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

		//буфер для передаваемых параметров параметров
		int buf = 0;

		public Graph()
		{
			InitializeComponent();

			SP_Flags.buttons_enable_flag = false;
			SP_Flags.external_message_flag = false;
			SP_Flags.mesure_status_flag = false;
			SP_Log.console = Text_console;

			NumOfSteps.Text = "100";
			ResolutionSet.Text = "1";
			RangeSet0.Text = "0";
			RangeSet1.Text = "100";
			MesuresCountSet.Text = "1";


			/*Receiver = new Thread(new ThreadStart(talker.go_online));
			Receiver.Start();*/

			Receiver = new Thread(new ThreadStart(connect));
			Receiver.Start();

			//настройка интерфейса
			Begin_Button.Enabled = false;
			Save_Button.Enabled = false;
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

			//ПОДКЛЮЧЕНИЕ
			Console.WriteLine("Talker here!\nAvailable Ports:");
			foreach (string s in talker.GetPortNames())
			{
				Console.WriteLine($"    {s}");
				portName = s;
				menustrip_COM.DropDownItems.Add(s).Click += on_port_select;
			}

			portSpeed = talker.GetBaudRate();
			menustrip_BaudRate.DropDownItems.Add(portSpeed.ToString()).Click += on_speed_select;
		}

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

		void TimerUpdate(object sender, EventArgs e)
		{
			if (SP_Flags.buttons_enable_flag)
				enable_buttons();

			Mesure_stat_label.Text = SP_Log.status;
			if (SP_Flags.external_message_flag)
			{
				SP_Log.Log(SP_Log.external);
				SP_Flags.external_message_flag = false;
			}


			DrawToBuffer(grafx.Graphics);
			Invalidate(canvas);
		}

		void Draw(object sender, PaintEventArgs e)
		{
			grafx.Render(tabgrfx);
		}

		void Begin_Click(object sender, EventArgs e)
		{
			Save_Button.Enabled = false;
			Begin_Button.Enabled = false;
			Begin_Button.Text = "Mesuring";

			//mr SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(29548);   //mr

			if (check_value(RangeSet0.Text) == -1)
				SP_Log.Log("**ERROR** Incorrect RANGE_0!!!");

			talker.send2bytes(buf);

			if (check_value(RangeSet1.Text) == -1)
				SP_Log.Log("**ERROR** Incorrect RANGE_1!!!");

			talker.send2bytes(buf);
			talker.read_line();

			//mc SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(25453);   //mc

			if (check_value(MesuresCountSet.Text) == -1)
				SP_Log.Log("**ERROR** Incorrect MesuresCount!!!");

			talker.send2bytes(buf);
			talker.read_line();

			//st SET////////////////////////////////////////////////////
			Thread.Sleep(50);
			talker.send2bytes(29811);   //st

			if (check_value(NumOfSteps.Text) == -1)
				SP_Log.Log("**ERROR** Incorrect Num of Steps!!!");

			SP_contaner.scale = (float)canvas.Width / (float)buf;
			talker.send2bytes(buf);
			talker.read_line();

			//информэйшен
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
			SP_Log.Log($"MESURING!");
			talker.Receive = true;
			talker.send2bytes(28002);	//bm
		}

		void Save_Click(object sender, EventArgs e)
		{
			SP_contaner.Save();
		}

		void Close_Click(object sender, EventArgs e)
		{
			Receiver.Abort();
			talker.Dispose();
			Close();
		}

		void enable_buttons()
		{
			talker.Receive = false;
			Begin_Button.Text = "Begin";
			Begin_Button.Enabled = true;
			Save_Button.Enabled = true;
			SP_Flags.buttons_enable_flag = false;
			SP_Log.Log("Ready");
			SP_Log.Debug("*****");
		}

		/* ИСПОЛЬЗОВАТЬ ТОЛЬКО ПРИ УСТАНОВКЕ ЗНАЧЕНИЙ!!!
		 * ФУНКЦИЯ ИСПОЛЬЗУЕТ ГЛОБАЛЬНУЮ ПЕРЕМЕННУЮ БУФЕРА
		 * ДЛЯ ЗАПИСИ ЗНАЧЕНИЯ ПЕРЕДАННОЙ СТРОКИ
		 */
		int check_value(string str)
		{
			try { buf = Convert.ToInt32(str); }
			catch
			{
				buf = 100;
				return -1;
			}

			return 0;
		}

		void connect()
		{
			Thread.Sleep(1000);
			int attempt = 1;
			SP_Log.External_message("Connecting...");

			if (talker.open(portName, portSpeed) == -1)
			{
				SP_Log.External_message("**ERROR** Plug in and restart!");
				return;
			}

			while (attempt <= 3)
			{
				//read hello
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

			SP_Flags.buttons_enable_flag = true;

			talker.go_online();
		}

		void on_port_select(object sender, EventArgs e)
		{
			portName = sender.ToString();
		}

		void on_speed_select(object sender, EventArgs e)
		{
			portSpeed = Convert.ToInt32(sender);
		}
	}
}
