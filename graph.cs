using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace graph1
{
	public partial class Graph : Form
	{
		//настройка связи
		SP_talker talker = new SP_talker();
		Thread Receiver;
		string portName;
		int portSpeed;

		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		int FPS = 30;

		//graphics
		Rectangle canvas;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;

		int resolution = 1;

		public Graph()
		{
			InitializeComponent();

			//настройка интерфейса
			Begin_Button.Enabled = false;
			Save_Button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			StartPosition = FormStartPosition.CenterScreen;

			//настройка таймера
			timer.Enabled = true;
			timer.Interval = 1000 / FPS;
			timer.Tick += new EventHandler(TimerUpdate);

			//настройка графики
			canvas = new Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			context.MaximumBuffer = new Size(canvas.Width + 1, canvas.Height + 1);
			grafx = context.Allocate(CreateGraphics(), canvas);
			tabgrfx = tabPage1.CreateGraphics();

			//установка дефолтных значений
			NumOfSteps.Text = "100";
			ResolutionSet.Text = "1";
			RangeSet0.Text = "0";
			RangeSet1.Text = "100";
			MesuresCountSet.Text = "1";

			/* Установка флагов в опущенное положение
			 * Установка объекта консоль для вывода сообщений */
			SP_Flags.get_ready_flag = false;
			SP_Flags.external_message_flag = false;
			SP_Flags.mesure_status_flag = false;
			SP_Log.console = Text_console;

			//установка имени и скорости порта
			Console.WriteLine("Talker here!\nAvailable Ports:");
			foreach (string s in talker.GetPortNames())
			{
				Console.WriteLine($"    {s}");
				portName = s;
				menustrip_COM.DropDownItems.Add(s).Click += on_port_select;
			}
			portSpeed = talker.GetBaudRate();
			menustrip_BaudRate.DropDownItems.Add(portSpeed.ToString()).Click += on_speed_select;

			//запуск параллельного потока
			Receiver = new Thread(new ThreadStart(connect));
			Receiver.Start();
		}

		// Запускается в паралелльном потоке
		void connect()
		{
			Thread.Sleep(1000);
			int attempt = 1;
			SP_Log.External_message("Соединение");

			//попытка открыть порт
			if (talker.open(portName, portSpeed) == -1)
			{
				SP_Log.External_message("**ERROR** Plug in and restart!");
				return;
			}

			while (attempt <= 3)
			{
				//прочитать строку проверки связи
				Thread.Sleep(1000);
				talker.send2bytes(25443);   //cc
				if (talker.read_line() == 0)
				{
					Console.WriteLine("Connected with {0} attempts", attempt);
					break;
				}
				attempt++;
			}

			if (attempt > 3)
			{
				SP_Log.External_message("**ERROR** Can't connect");
				return;
			}

			Console.WriteLine("************");
			Console.WriteLine("CONNECTED");
			Console.WriteLine($"PORT: {portName}");
			Console.WriteLine($"SPEED: {portSpeed}");
			Console.WriteLine("************");

			SP_Flags.get_ready_flag = true;

			talker.go_online();
		}

		//отрисовка графика в буфер
		void DrawToBuffer(Graphics g)
		{
			g.FillRectangle(SystemBrushes.Highlight, canvas);
			for (int i = 0; i <= SP_contaner.cur; i += resolution)
			{
				if (SP_contaner.points[i + resolution].X  != 0)
				{
					g.DrawLine(pen, SP_contaner.points[i].X, 
						canvas.Height - (SP_contaner.points[i].Y >> 2),
						SP_contaner.points[i + resolution].X, 
						canvas.Height - (SP_contaner.points[i + resolution].Y >> 2));
				}
			}
		}

		//постоянно тикающий таймер
		void TimerUpdate(object sender, EventArgs e)
		{
			if (SP_Flags.get_ready_flag)
				get_ready();

			Mesure_stat_label.Text = SP_Log.status;
			if (SP_Flags.external_message_flag)
			{
				SP_Log.Log(SP_Log.external);
				SP_Flags.external_message_flag = false;
			}


			DrawToBuffer(grafx.Graphics);
			Invalidate(canvas);
		}

		/* вызывается при запросе "перерисовать" (Ivalidate)
		   в функции таймера*/
		void Draw(object sender, PaintEventArgs e)
		{
			grafx.Render(tabgrfx);
		}

		//Вызывается при нажатии кнопки "Начать"
		void Begin_Click(object sender, EventArgs e)
		{
			//буфер для передаваемых параметров параметров
			int buf;

			//отключение кнопок, не нужных на момент измерения
			Save_Button.Enabled = false;
			Begin_Button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Begin_Button.Text = "Измерение";

			//mr SET////////////////////////////////////////////////////
			//отправка команды, прием подтверждающей строки
			Thread.Sleep(50);
			talker.send2bytes(29548);   //mr

			if ((buf = check_value(RangeSet0.Text)) == -1)
			{
				SP_Log.Log("**ERROR** Incorrect RANGE_0!!!");
				buf = 0;
			}

			talker.send2bytes(buf);

			if ((buf = check_value(RangeSet1.Text)) == -1)
			{
				SP_Log.Log("**ERROR** Incorrect RANGE_1!!!");
				buf = 100;
			}

			talker.send2bytes(buf);
			talker.read_line();

			//mc SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(25453);   //mc

			if ((buf = check_value(MesuresCountSet.Text)) == -1)
			{
				SP_Log.Log("**ERROR** Incorrect MesuresCount!!!");
				buf = 100;
			}

			talker.send2bytes(buf);
			talker.read_line();

			//st SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(29811);   //st

			if ((buf = check_value(NumOfSteps.Text)) == -1)
			{
				SP_Log.Log("**ERROR** Incorrect Num of Steps!!!");
				buf = 100;
			}
				
			SP_contaner.scale = (float)canvas.Width / (float)buf;
			talker.send2bytes(buf);
			talker.read_line();

			//установка разрешения графика
			Console.WriteLine($"Scale: {SP_contaner.scale}");
			try { resolution = Convert.ToInt32(ResolutionSet.Text); }
			catch
			{
				resolution = 1;
				SP_Log.Log($"**ERROR** Incorrect resolution!!!");
			}
			
			//очистка и отправка команды начать
			Thread.Sleep(200);
			SP_contaner.Clear();
			talker.FlushReadBuf();
			SP_Log.Debug($"Receiver STATUS: {Receiver.ThreadState}");
			SP_Log.Log($"ИЗМЕРЕНИЕ!");
			talker.Receive = true; // поднятие флага рессивера
			talker.send2bytes(28002);	//bm
		}

		void Save_Click(object sender, EventArgs e)
		{
			SP_contaner.Save_on_disk();
		}

		void Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		void Graph_FormClosing(object sender, FormClosingEventArgs e)
		{
			Receiver.Abort();
			talker.Dispose();
		}

		// Вызывается для подготовки интерфейса и программы к вводу пользователя
		void get_ready()
		{
			talker.Receive = false;
			Begin_Button.Text = "Начать";

			Begin_Button.Enabled = true;
			Save_Button.Enabled = true;
			New_button.Enabled = true;
			if (tabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;

			SP_contaner.Save_on_RAM(tabControl1.SelectedIndex);
			SP_Flags.get_ready_flag = false;
			SP_Log.Log("Готов");
			SP_Log.Debug("*****");
		}

		//проверяет значение
		int check_value(string str)
		{
			try { Convert.ToInt32(str); }
			catch
			{
				return -1;
			}

			return Convert.ToInt32(str);
		}

		void on_port_select(object sender, EventArgs e)
		{
			portName = sender.ToString();
		}

		void on_speed_select(object sender, EventArgs e)
		{
			portSpeed = Convert.ToInt32(sender);
		}

		void New_button_Click(object sender, EventArgs e)
		{
			TabPage newTabPage = new TabPage("Спектр " + (tabControl1.TabCount + 1).ToString());
			tabControl1.TabPages.Add(newTabPage);
			tabControl1.SelectedIndex = tabControl1.TabPages.IndexOf(newTabPage);
			SP_Log.Log($"Спектр{tabControl1.TabPages.IndexOf(newTabPage) + 1} создан");
		}

		void Delete_button_Click(object sender, EventArgs e)
		{

			SP_contaner.Delete_from_RAM(tabControl1.SelectedIndex);
			tabControl1.TabPages.Remove(tabControl1.SelectedTab);
			SP_Log.Log($"Спектр{tabControl1.SelectedIndex + 1} удален");
		}

		void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
		{
			tabgrfx = e.TabPage.CreateGraphics();
			SP_contaner.Load_from_RAM(e.TabPageIndex);

			if (tabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;
		}
	}
}
