using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace graph1
{
	partial class Graph : Form
	{
		//Команды сервера
		const int CMD_MB = 25197;
		const int CMD_MC = 25453;
		const int CMD_MR = 29293;
		const int CMD_MS = 29549;

		const int CMD_DD = 25700;
		const int CMD_DS = 29540;
		const int CMD_DV = 30308;
		const int CMD_DF = 26212;
		const int CMD_DB = 25188;
		const int CMD_DI = 26980;

		const int CMD_CC = 25443;
		const int CMD_ST = 29811;
		const int CMD_TP = 28788;

		//Использовал таймер из форм, потому что другой не работает...почему то...
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		//Настройка графики
		Rectangle canvas;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;
		int resolution = 1;

		// Констркуктор
		public Graph()
		{
			InitializeComponent();

			//Настройка связи
			Receive = false;
			_baudrate = 76800;

			//Настройка интерфейса
			Begin_Button.Enabled = false;
			Stop_Button.Enabled = false;
			Save_Button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			StartPosition = FormStartPosition.CenterScreen;

			//Настройка таймера
			timer.Enabled = true;
			timer.Interval = 1;
			timer.Tick += new EventHandler(timer_update);
			
			//Установка дефолтных значений
			RangeSet0.Text = "0";
			RangeSet1.Text = "100";
			MesuresCountSet.Text = "1";
			ResolutionSet.Text = "1";

			//Подключение к серверу
			LOG_Debug("Talker here!\nAvailable Ports:");
			TALKER_connect();
		}

		/// <summary>
		/// Тикалка
		/// Это событие вызывается каждый тик таймера.
		/// Каждый вызов вызывает функцию приема данных
		/// 1000 раз для того, что бы не забивался буфер.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timer_update(object sender, EventArgs e)
		{
			if (Receive)
			{
				for(int i = 0; i < 1000; i++)
					TALKER_receiver();
			}
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
			for (int i = 0; i <= CONTAINER_cur; i += resolution)
			{
				if (dot[i + resolution].X != 0)
				{
					//сложные формулы
					g.DrawLine(
						pen,
						dot[i].X,
						canvas.Height - (dot[i].Y >> 2)-1,
						dot[i + resolution].X,
						canvas.Height - (dot[i + resolution].Y >> 2)-1
					);
				}
			}
		}

		/// <summary>
		/// Вызывается при запросе "перерисовать" (Ivalidate) в функции таймера.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void draw(object sender, PaintEventArgs e)
		{
			grafx.Render(tabgrfx);
		}

		/// <summary>
		/// Возвращение интерфейса в активное состояние
		/// </summary>
		void get_ready()
		{
			Receive = false;
			Begin_Button.Text = "Начать";

			Begin_Button.Enabled = true;
			Stop_Button.Enabled = false;
			Save_Button.Enabled = true;
			New_button.Enabled = true;
			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;

			CONTAINER_Save_on_RAM(TabControl1.SelectedIndex);
			LOG("Готов");
			LOG_Debug("*****");
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
				LOG(errmsg);
				return -1;
			}

			if (temp < 0)
				return 0;

			return temp;
		}

		#region События   
		
		void Begin_Click(object sender, EventArgs e)
		{
			//отключение кнопок, не нужных на момент измерения
			Save_Button.Enabled = false;
			Begin_Button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Begin_Button.Text = "Измерение";

			//проверка значений и запись в память
			if ((range0 = check_value(
					RangeSet0.Text, 
					"**ОШИБКА** Incorrect RANGE_0!!!")) == -1)
				range0 = 0;
			if ((range1 = check_value(
					RangeSet1.Text,
					"**ОШИБКА** Incorrect RANGE_1!!!")) == -1)
				range1 = 100;
			if ((mps = check_value(
					MesuresCountSet.Text,
					"**ОШИБКА** Incorrect MesuresCount!!!")) == -1)
				mps = 1;
			if ((resolution = check_value(
					ResolutionSet.Text,
					"**ОШИБКА** Incorrect Resolution!!!")) == -1)
				resolution = 1;


			//mr SET Диапазон измерений/////////////////////////////////////
			Thread.Sleep(50);
			TALKER_send2bytes(CMD_MR);  //mr
			TALKER_send3bytes(range0); //первое значение
			TALKER_send3bytes(range1); //второе значение
			LOG_Debug($"range0 = {range0}");
			LOG_Debug($"range1 = {range1}");
			TALKER_read_line(); //подтверждение

			//mc SET Измерений за шаг///////////////////////////////////////
			Thread.Sleep(50);
			TALKER_send2bytes(CMD_MC); //mc
			TALKER_send2bytes(mps);
			LOG_Debug($"mps = {mps}");
			TALKER_read_line(); //подтверждение

			//вычисление масштаба горизонтальной шкалы
			scale = canvas.Width / (float)(range1 - range0);
			LOG_Debug($"Scale: {scale}");

			Stop_Button.Enabled = true;

			//очистка и отправка команды начать
			Thread.Sleep(200);
			CONTAINER_Clear();
			TALKER_FlushReadBuf();
			LOG($"ИЗМЕРЕНИЕ!");
			Receive = true;
			TALKER_send2bytes(CMD_MB);
		}

		void New_button_Click(object sender, EventArgs e)
		{
			TabPage newTabPage = new TabPage("Спектр " + (TabControl1.TabCount + 1).ToString());
			TabControl1.TabPages.Add(newTabPage);
			TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(newTabPage);
			LOG($"Спектр создан");
		}

		void Delete_button_Click(object sender, EventArgs e)
		{
			CONTAINER_Delete_from_RAM(TabControl1.SelectedIndex, TabControl1.TabCount);
			TabControl1.TabPages.Remove(TabControl1.SelectedTab);
			LOG($"Спектр удален");
		}

		void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
		{
			tabgrfx = e.TabPage.CreateGraphics();
			CONTAINER_Load_from_RAM(e.TabPageIndex);
			
			canvas = new Rectangle(0, 0, e.TabPage.Width, e.TabPage.Height);
			context.MaximumBuffer = new Size(canvas.Width + 1, canvas.Height + 1);
			grafx = context.Allocate(CreateGraphics(), canvas);
			tabgrfx = e.TabPage.CreateGraphics();

			RangeSet0.Text = range0.ToString();
			RangeSet1.Text = range1.ToString();
			MesuresCountSet.Text = mps.ToString();

			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;
		}

		void Stop_button_Click(object sender, EventArgs e)
		{
			TALKER_send2bytes(CMD_DI);  // di
		}

		void Save_Click(object sender, EventArgs e)
		{
			CONTAINER_Save_on_disk();
		}

		void Graph_Load(object sender, EventArgs e)
		{
			//Настройка графики
			canvas = new Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			context.MaximumBuffer = new Size(canvas.Width + 1, canvas.Height + 1);
			grafx = context.Allocate(CreateGraphics(), canvas);
			tabgrfx = tabPage1.CreateGraphics();
		}

		void Graph_SizeChanged(object sender, EventArgs e)
		{
			canvas = new Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			context.MaximumBuffer = new Size(canvas.Width + 1, canvas.Height + 1);
			grafx = context.Allocate(CreateGraphics(), canvas);
			tabgrfx = tabPage1.CreateGraphics();
		}

		void Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		void Graph_Form_Closing(object sender, FormClosingEventArgs e)
		{
			Dispose();
		}

		#endregion
	}
}
