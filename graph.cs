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

		const int DRIVER_FWD = 1;
		const int DRIVER_BCK = -1;

		//Использовал таймер из форм, потому что другой не работает...почему то...
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		//Настройка графики
		Rectangle canvas;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;
		int resolution = 1;
		float scale;
		float height_scale;

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
			StepsSet.Text = "1";
			DividerSet.Text = "1";

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
			int cur = 0;
			g.DrawLine(
				pen, 
				CONTAINER_cur * scale, 
				0, 
				CONTAINER_cur * scale, 
				canvas.Height
			);
			for (int i = 0; i <= CONTAINER_range; i += resolution)
			{
				g.DrawLine(
					pen,
					(int)(i * scale),
					canvas.Height - (CONTAINER_graph[i] >> 2),
					(int)((i + resolution) * scale),
					canvas.Height - (CONTAINER_graph[i + resolution] >> 2)
				);
				cur += (int)scale;
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
		/// Подготовка интерфейса к измерению
		/// </summary>
		void get_armed()
		{
			//отключение кнопок, не нужных на момент измерения
			Save_Button.Enabled = false;
			Begin_Button.Enabled = false;
			Begin_Button.Text = "Измерение";
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Stop_Button.Enabled = true;
			Forward_button.Enabled = false;
			Back_button.Enabled = false;
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
			Forward_button.Enabled = true;
			Back_button.Enabled = true;
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

		void set_canvas_scale()
		{
			//вычисление масштаба горизонтальной шкалы
			CONTAINER_range = CONTAINER_x1 - CONTAINER_x0;
			scale = canvas.Width / (float)CONTAINER_range;
			//height_scale = canvas.Height / 1024;
			LOG_Debug($"Scale: {scale}");
		}

		void search_setup(int dir)
		{
			int steps;
			int div;
			get_armed();

			if ((steps = check_value(
					StepsSet.Text,
					"**ОШИБКА** Incorrect steps!!!")) == -1)
				steps = 1;
			if ((div = check_value(
					DividerSet.Text,
					"**ОШИБКА** Incorrect div!!!")) == -1)
				div = 1;

			//Установка количества шагов
			Thread.Sleep(1);
			TALKER_send2bytes(CMD_ST);
			TALKER_send2bytes(steps);
			TALKER_read_line(); //подтверждение

			//Установка делителя
			Thread.Sleep(1);
			TALKER_send2bytes(CMD_DV);
			TALKER_send2bytes(div);
			TALKER_read_line(); //подтверждение

			//Установка направления
			if (dir == 1)
			{
				TALKER_send2bytes(CMD_DF);
				TALKER_read_line(); //подтверждение
			}

			if (dir == -1)
			{
				TALKER_send2bytes(CMD_DB);
				TALKER_read_line();
			}

			set_canvas_scale();
			CONTAINER_curdir = dir;

			//очистка и отправка команды начать
			Thread.Sleep(1);
			TALKER_FlushReadBuf();
			LOG($"ПОИСК");
			Receive = true;
			TALKER_send2bytes(CMD_MB);
		}

		void mesure_setup()
		{
			get_armed();

			//Проверка значений и запись в память
			if ((CONTAINER_x0 = check_value(
					RangeSet0.Text,
					"**ОШИБКА** Incorrect RANGE_0!!!")) == -1)
				CONTAINER_x0 = 0;
			if ((CONTAINER_x1 = check_value(
					RangeSet1.Text,
					"**ОШИБКА** Incorrect RANGE_1!!!")) == -1)
				CONTAINER_x1 = 100;
			if ((CONTAINER_mps = check_value(
					MesuresCountSet.Text,
					"**ОШИБКА** Incorrect MesuresCount!!!")) == -1)
				CONTAINER_mps = 1;
			if ((resolution = check_value(
					ResolutionSet.Text,
					"**ОШИБКА** Incorrect Resolution!!!")) == -1)
				resolution = 1;

			//Диапазон измерений/////////////////////////////////////
			Thread.Sleep(1);
			TALKER_send2bytes(CMD_MR);
			TALKER_send3bytes(CONTAINER_x0); //первое значение
			TALKER_send3bytes(CONTAINER_x1); //второе значение
			LOG_Debug($"range0 = {CONTAINER_x0}");
			LOG_Debug($"range1 = {CONTAINER_x1}");
			TALKER_read_line(); //подтверждение

			//Измерений за шаг///////////////////////////////////////
			Thread.Sleep(1);
			TALKER_send2bytes(CMD_MC);
			TALKER_send2bytes(CONTAINER_mps);
			LOG_Debug($"mps = {CONTAINER_mps}");
			TALKER_read_line(); //подтверждение

			//Усновка направления
			TALKER_send2bytes(CMD_DF);
			TALKER_read_line();

			set_canvas_scale();
			CONTAINER_curdir = 1;

			//очистка и отправка команды начать
			Thread.Sleep(1);
			CONTAINER_Clear();
			TALKER_FlushReadBuf();
			LOG($"ИЗМЕРЕНИЕ!");
			Receive = true;
			TALKER_send2bytes(CMD_MB);
		}

		#region События   

		void Begin_Click(object sender, EventArgs e)
		{
			mesure_setup();
		}

		void Forward_button_Click(object sender, EventArgs e)
		{
			search_setup(DRIVER_FWD);
		}

		void Back_button_Click(object sender, EventArgs e)
		{
			search_setup(DRIVER_BCK);
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
			CONTAINER_Load_from_RAM(e.TabPageIndex);
			
			canvas = new Rectangle(0, 0, e.TabPage.Width, e.TabPage.Height);
			context.MaximumBuffer = new Size(canvas.Width + 1, canvas.Height + 1);
			grafx = context.Allocate(CreateGraphics(), canvas);
			tabgrfx = e.TabPage.CreateGraphics();
			set_canvas_scale();

			RangeSet0.Text = CONTAINER_x0.ToString();
			RangeSet1.Text = CONTAINER_x1.ToString();
			MesuresCountSet.Text = CONTAINER_mps.ToString();

			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;
		}

		void Stop_button_Click(object sender, EventArgs e)
		{
			TALKER_send2bytes(CMD_DI);
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
			//Изменение размеров и масштаба холста
			canvas = new Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			//Здесь нужен только пересчет масштаба
			scale = canvas.Width / (float)CONTAINER_range;
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