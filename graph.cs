using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace graph1
{
	public partial class Graph : Form
	{
		//Настройка связи
		SP_talker talker = new SP_talker();

		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		int FPS = 30;

		//Настройка графики
		Rectangle canvas;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;
		int resolution = 1;

		/// <summary>
		/// Констркуктор
		/// </summary>
		public Graph()
		{
			InitializeComponent();

			//Настройка интерфейса
			Begin_Button.Enabled = false;
			Save_Button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			StartPosition = FormStartPosition.CenterScreen;

			//Настройка таймера
			timer.Enabled = true;
			timer.Interval = 1000 / FPS;
			timer.Tick += new EventHandler(timer_update);

			//Настройка графики
			canvas = new Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			context.MaximumBuffer = new Size(canvas.Width + 1, canvas.Height + 1);
			grafx = context.Allocate(CreateGraphics(), canvas);
			tabgrfx = tabPage1.CreateGraphics();

			//Установка дефолтных значений
			//NumOfSteps.Text = "100";
			ResolutionSet.Text = "1";
			RangeSet0.Text = "0";
			RangeSet1.Text = "100";
			MesuresCountSet.Text = "1";

			/* Установка флагов в опущенное положение
			 * Установка объекта консоль для вывода сообщений */
			SP_Flags.get_ready_flag = false;
			SP_Flags.external_message_flag = false;
			SP_Log.console = Text_console;

			//Установка имени и скорости порта
			Console.WriteLine("Talker here!\nAvailable Ports:");
			foreach (string s in talker.GetPortNames())
			{
				Console.WriteLine($"    {s}");
				talker._portname = s;
				menustrip_COM.DropDownItems.Add(s).Click += On_Port_Select;
			}
			menustrip_BaudRate.DropDownItems.Add(talker._baudrate.ToString()).Click += On_Speed_Select;

			talker.connect();
			get_ready();
		}

		/// <summary>
		/// Тикалка
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timer_update(object sender, EventArgs e)
		{
			Mesure_stat_label.Text = SP_Log.status;

			if (SP_Flags.get_ready_flag)
				get_ready();

			if (talker.Receive)
				talker.receiver();

			draw_to_buffer(grafx.Graphics);
			Invalidate(canvas);
		}

		/// <summary>
		/// Отрисовка спектра в буфер
		/// </summary>
		/// <param name="g"></param>
		void draw_to_buffer(Graphics g)
		{
			g.FillRectangle(SystemBrushes.Highlight, canvas);
			for (int i = 0; i <= SP_contaner.cur; i += resolution)
			{
				if (SP_contaner.points[i + resolution].X != 0)
				{
					g.DrawLine(pen, SP_contaner.points[i].X,
						canvas.Height - (SP_contaner.points[i].Y >> 2),
						SP_contaner.points[i + resolution].X,
						canvas.Height - (SP_contaner.points[i + resolution].Y >> 2));
				}
			}
		}

		/* вызывается при запросе "перерисовать" (Ivalidate)
		   в функции таймера*/
		void draw(object sender, PaintEventArgs e)
		{
			grafx.Render(tabgrfx);
		}

		/// <summary>
		/// Возвращение интерфейса в активное состояние
		/// </summary>
		void get_ready()
		{
			talker.Receive = false;
			Begin_Button.Text = "Начать";

			Begin_Button.Enabled = true;
			Save_Button.Enabled = true;
			New_button.Enabled = true;
			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;

			SP_contaner.Save_on_RAM(TabControl1.SelectedIndex);
			SP_Flags.get_ready_flag = false;
			SP_Log.Log("Готов");
			SP_Log.Debug("*****");
		}

		/// <summary>
		/// Проверка значения из строки
		/// </summary>
		/// <param name="str"></param>
		/// <param name="errmsg"></param>
		/// <returns></returns>
		int check_value(string str, string errmsg)
		{
			int temp;
			try { temp = Convert.ToInt32(str); }
			catch
			{
				SP_Log.Log(errmsg);
				return -1;
			}

			if (temp < 0)
				return 0;

			return temp;
		}

		#region События   
		
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

			if((buf = check_value(RangeSet0.Text, "**ERROR** Incorrect RANGE_0!!!")) == -1)
				buf = 0;

			SP_contaner.range0 = buf;
			talker.send2bytes(buf);

			if((buf = check_value(RangeSet1.Text, "**ERROR** Incorrect RANGE_1!!!")) == -1)
				buf = 100;
			
			SP_contaner.range1 = buf;
			talker.send2bytes(buf);
			talker.read_line();

			//mc SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(25453);   //mc

			if((buf = check_value(MesuresCountSet.Text, "**ERROR** Incorrect MesuresCount!!!")) == -1)
				buf = 100;

			SP_contaner.mps = buf;
			talker.send2bytes(buf);
			talker.read_line();

			//st SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(29811);   //st
			buf = SP_contaner.range1 - SP_contaner.range0;
			SP_contaner.scale = (float)canvas.Width / (float)buf;
			talker.send2bytes(buf);
			talker.read_line();

			//установка разрешения спектра
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
			SP_Log.Log($"ИЗМЕРЕНИЕ!");
			talker.Receive = true; // поднятие флага рессивера
			talker.send2bytes(28002);   //bm
		}

		void Save_Click(object sender, EventArgs e)
		{
			SP_contaner.Save_on_disk();
		}

		void Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		void New_button_Click(object sender, EventArgs e)
		{
			TabPage newTabPage = new TabPage("Спектр " + (TabControl1.TabCount + 1).ToString());
			TabControl1.TabPages.Add(newTabPage);
			TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(newTabPage);
			SP_Log.Log($"Спектр создан");
		}

		void Delete_button_Click(object sender, EventArgs e)
		{
			SP_contaner.Delete_from_RAM(TabControl1.SelectedIndex, TabControl1.TabCount);
			TabControl1.TabPages.Remove(TabControl1.SelectedTab);
			SP_Log.Log($"Спектр удален");
		}

		void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
		{
			tabgrfx = e.TabPage.CreateGraphics();
			SP_contaner.Load_from_RAM(e.TabPageIndex);

			RangeSet0.Text = SP_contaner.range0.ToString();
			RangeSet1.Text = SP_contaner.range1.ToString();
			MesuresCountSet.Text = SP_contaner.mps.ToString();

			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;
		}

		void On_Port_Select(object sender, EventArgs e)
		{
			talker._portname = sender.ToString();
		}

		void On_Speed_Select(object sender, EventArgs e)
		{
			talker._baudrate = Convert.ToInt32(sender);
		}

		void Graph_Form_Closing(object sender, FormClosingEventArgs e)
		{
			//Receiver.Abort();
			talker.Dispose();
		}

		#endregion
	}
}
