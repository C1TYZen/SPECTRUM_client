using System;
using System.Windows.Forms;

namespace graph1
{
	partial class Graph : Form
	{
		//Функции
		const int CMD_MB = 0x626d; // начать измерение
		const int CMD_DZ = 0x7a64; // двигатель на ноль
		const int CMD_CC = 0x6363; // проверить соединение
		const int CMD_CS = 0x7363; // установка значения переменной
		const int CMD_TP = 0x7074; // тестирование пинов
		const int CMD_TF = 0x6674; // тестовая функция

		//Сообщения
		const int CMD_MS = 0x736d; // остановить измерение
		const int CMD_MI = 0x696d; // прервать измерение

		//Переменные
		const int CVAR_MA = 0x616d; // начало измерения
		const int CVAR_MZ = 0x7a6d; // конец измерения
		const int CVAR_MC = 0x636d; // количество измерений
		const int CVAR_DS = 0x7364; // скорость двигателя
		const int CVAR_FN = 0x6e66; // номер фильтра
		const int CVAR_FS = 0x7366; // шаг установки фильтра

		//Разное
		Timer timer = new Timer();
		bool Receive;

		// Переменные для хранения сообщений
		byte[] bmsg = new byte[4];
		int imsg;

		public Graph()
		{
			InitializeComponent();

			CFG_get_config(cfg);

			//Настройка связи
			Receive = false;

			//Настройка интерфейса
			StartPosition = FormStartPosition.CenterScreen;
			begin_button.Enabled		= false;
			stop_button.Enabled			= false;
			save_button.Enabled			= false;
			new_button.Enabled			= false;
			delete_button.Enabled		= false;
			callibrate_button.Enabled	= false;

			//Настройка таймера
			timer.Enabled	= true;
			timer.Interval	= 1;
			timer.Tick		+= new EventHandler(timer_update);

			//Установка дефолтных значений
			mesure_start_set.Text	= CFG_get_value(cfg, "mesure_start").ToString();
			mesure_end_set.Text		= CFG_get_value(cfg, "mesure_end").ToString();
			mesure_count_set.Text	= CFG_get_value(cfg, "mesure_count").ToString();
			resolution_set.Text		= CFG_get_value(cfg, "draw_resolution").ToString();
			filter_num_set.Text		= CFG_get_value(cfg, "filter_num").ToString();
			filter_step_set.Text	= CFG_get_value(cfg, "filter_step").ToString();
			speed_set.Text			= CFG_get_value(cfg, "driver_speed").ToString();
			amp_coef_set.Text		= CFG_get_value(cfg, "amp").ToString();

			spectrum.graph			= new int[points_count];
			spectrum.end			= 0;
			spectrum.x0				= 0;
			spectrum.x1				= 100;
			spectrum.mps			= 1;
			spectrum.filter_num		= 1;
			spectrum.filter_step	= 0;
			spectrum.speed			= 700;
			spectrum.amp			= 1;

			//Подключение к серверу
			LOG_Debug("Talker here!\nAvailable Ports:");
			if (TALKER_connect() == 0)
				get_ready();
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
				//for (int i = 0; i < 5; i++)
					receiver();
			}
			DRAW_grid(grafx.Graphics);
			DRAW_spectrum_line(grafx.Graphics, spectrum);
			DRAW_curs(grafx.Graphics);
			Invalidate();
		}

		/// <summary>
		/// Читает по 2 байта из
		/// буфера и записывает в контейнер.
		/// При получении команды стоп - опускается флаг.
		/// </summary>

		/// <remarks>
		/// Цикл приема данных:
		/// 1. Прочитать 2 байта из буфера;
		///	2. Соеденить 2 байта и сохранить в переменную;
		///	3. Если данные равны значению выхода - закончить прием измерений;
		///	4. Вывод статуса измерения в строку в интерфейсе;
		///	5. Добавить значение в контейнер.
		/// </remarks>
		void receiver()
		{
			com_read(bmsg, 2);
			imsg = bmsg[0] + (bmsg[1] << 8);
			LOG_Debug($"{imsg}");

			if (imsg == CMD_MS)
			{
				LOG_Debug("");
				get_ready();
			}
			spectrum.end += 1;
			CONTAINER_Add(imsg, spectrum);
		}

		#region Setup

		/// <summary>
		/// Подготовка интерфейса к измерению
		/// </summary>
		void get_armed()
		{
			save_button.Enabled			= false;
			begin_button.Enabled		= false;
			begin_button.Text			= "Измерение";
			new_button.Enabled			= false;
			delete_button.Enabled		= false;
			stop_button.Enabled			= true;
			callibrate_button.Enabled	= false;
		}

		/// <summary>
		/// Возвращение интерфейса в активное состояние
		/// </summary>
		void get_ready()
		{
			imsg = 0;
			bmsg[0] = 0;
			bmsg[1] = 0;
			Receive						= false;
			begin_button.Text			= "Начать";
			begin_button.Enabled		= true;
			stop_button.Enabled			= false;
			save_button.Enabled			= true;
			new_button.Enabled			= true;
			callibrate_button.Enabled	= true;

			if (tab_control1.TabCount == 1)
				delete_button.Enabled = false;
			else
				delete_button.Enabled = true;

			CONTAINER_Save_on_RAM(tab_control1.SelectedIndex);
			LOG("Готов");
			LOG_Debug("*****");

			LOG_Debug($"шаг: {spectrum.end}");
		}

		/// <summary>
		/// Вычисление размеров элементов окна и самого окна
		/// </summary>
		void setup_sizes()
		{
			DRAW_background =
				new System.Drawing.Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			DRAW_canvas =
				new System.Drawing.Rectangle(40, 10, tabPage1.Width - 50, tabPage1.Height - 40);
			context.MaximumBuffer =
				new System.Drawing.Size(tabPage1.Width + 1, tabPage1.Height + 1);
		}

		/// <summary>
		/// Вычисление масштаба холста
		/// </summary>
		void setup_canvas_scale()
		{
			DRAW_range = (spectrum.x1 - spectrum.x0) * 8;
			DRAW_scale = DRAW_canvas.Width / (float)DRAW_range;
			DRAW_height_scale = DRAW_canvas.Height / (float)1024;
		}

		void refresh_gfx(TabPage tab)
		{
			setup_sizes();
			setup_canvas_scale();

			grafx = context.Allocate(CreateGraphics(), DRAW_background);
			tabgrfx = tab.CreateGraphics();
		}

		/// <summary>
		/// Подготовка к измерению
		/// </summary>
		void mesure_setup()
		{
			if (TALKER_flush_read_buf() == -1) return;
			get_armed();

			//Проверка значений и запись в память
			if ((spectrum.x0 = check_value(mesure_start_set.Text, "RANGE_0")) == -1)
				spectrum.x0 = 0;
			if ((spectrum.x1 = check_value(mesure_end_set.Text,"RANGE_1")) == -1)
				spectrum.x1 = 100;
			if (spectrum.x0 > spectrum.x1)
			{
				int buf = spectrum.x0;
				spectrum.x0 = spectrum.x1;
				spectrum.x1 = buf;
			}
			if ((spectrum.mps = check_value(mesure_count_set.Text, "Mesures Count")) == -1)
				spectrum.mps = 1;
			if ((spectrum.filter_num = check_value(filter_num_set.Text, "Filter Number")) == -1)
				spectrum.filter_num = 1;
			if ((spectrum.filter_step = check_value(filter_step_set.Text, "Filter Step")) == -1)
				spectrum.mps = 0;
			if ((spectrum.speed = check_value(speed_set.Text, "Speed")) == -1)
				spectrum.speed = 1;
			if ((spectrum.amp = check_value(amp_coef_set.Text, "Amp")) == -1)
				spectrum.amp = 1;

			//Начало диапазона
			TALKER_set(CVAR_MA, spectrum.x0);
			//Конец диапазона
			TALKER_set(CVAR_MZ, spectrum.x1);
			//Измерений/шаг
			TALKER_set(CVAR_MC, spectrum.mps);
			//Номер фильтра
			//TALKER_set(CVAR_FN, spectrum.filter_num);
			//Шаг выставления фильтра
			//TALKER_set(CVAR_FS, spectrum.filter_step);
			//Скорость
			TALKER_set(CVAR_DS, spectrum.speed);

			//Настройка отрисовки
			setup_canvas_scale();
			CONTAINER_Clear();
			LOG_Debug($"Scale: {DRAW_scale}");
			LOG_Debug($"Height Scale: {DRAW_height_scale}");
			//Это нужно для того, что бы отрисовывался весь диапазон от х0 до х1,
			//включая крайние точки
			spectrum.end = -1;

			//Очистка буфера и отправка команды начать
			TALKER_flush_read_buf();
			LOG($"ИЗМЕРЕНИЕ!");
			Receive = true;
			TALKER_send(CMD_MB);
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
				LOG("**ОШИБКА** Incorrect " + errmsg + " !!!");
				return -1;
			}

			if (temp < 0)
				return 0;

			return temp;
		}

		#endregion

		#region Evenst

		void BUTTON_Begin_click(object sender, EventArgs e)
		{
			mesure_setup();
		}

		void BUTTON_Callibrate_click(object sender, EventArgs e)
		{
			TALKER_send(CMD_DZ);
		}

		void BUTTON_check(object sender, EventArgs e)
		{
			TALKER_send(CMD_CC);
			TALKER_read_line();
		}

		void BUTTON_New_click(object sender, EventArgs e)
		{
			TabPage newTabPage = 
				new TabPage("Спектр " + (tab_control1.TabCount + 1).ToString());
			tab_control1.TabPages.Add(newTabPage);
			tab_control1.SelectedIndex = tab_control1.TabPages.IndexOf(newTabPage);

			newTabPage.MouseClick += 
				new System.Windows.Forms.MouseEventHandler(tab_page_mouse_click);
			newTabPage.MouseMove += 
				new System.Windows.Forms.MouseEventHandler(tab_page_mouse_move);
			LOG($"Спектр создан");
		}

		void BUTTON_Delete_click(object sender, EventArgs e)
		{
			CONTAINER_Delete_from_RAM(tab_control1.SelectedIndex, tab_control1.TabCount);
			tab_control1.TabPages.Remove(tab_control1.SelectedTab);
			LOG($"Спектр удален");
		}

		void BUTTON_Stop_click(object sender, EventArgs e)
		{
			TALKER_send(CMD_MI);
		}

		void BUTTON_Save_click(object sender, EventArgs e)
		{
			CONTAINER_Save_on_disk();
		}

		void BUTTON_Close_click(object sender, EventArgs e)
		{
			Close();
		}

		void Graph_load(object sender, EventArgs e)
		{
			refresh_gfx(tabPage1);

			tabPage1.MouseClick += 
				new System.Windows.Forms.MouseEventHandler(tab_page_mouse_click);
			tabPage1.MouseMove += 
				new System.Windows.Forms.MouseEventHandler(tab_page_mouse_move);
		}

		void Graph_size_changed(object sender, EventArgs e)
		{
			refresh_gfx(tab_control1.SelectedTab);
		}

		void Graph_form_closing(object sender, FormClosingEventArgs e)
		{
			Dispose();
		}

		void tab_control1_selecting(object sender, TabControlCancelEventArgs e)
		{
			CONTAINER_Load_from_RAM(e.TabPageIndex);

			refresh_gfx(e.TabPage);

			mesure_start_set.Text = spectrum.x0.ToString();
			mesure_end_set.Text = spectrum.x1.ToString();
			mesure_count_set.Text = spectrum.mps.ToString();

			if (tab_control1.TabCount == 1)
				delete_button.Enabled = false;
			else
				delete_button.Enabled = true;
		}

		void tab_page_mouse_move(object sender, MouseEventArgs e)
		{
			int temp_index = (int)((e.Location.X - DRAW_canvas.X) / DRAW_scale);
			int temp_x = spectrum.x0 + temp_index / 8;

			if ((temp_index < 0) || (temp_index > points_count))
				temp_index = 0;
			int temp_value = spectrum.graph[temp_index];

			MPosition_status_label.Text = $"X: {temp_x}  Знач: {temp_value}";
		}

		void tab_page_mouse_click(object sender, MouseEventArgs e)
		{
			int temp_index = (int)((e.Location.X - DRAW_canvas.X) / DRAW_scale);
			int temp_x = spectrum.x0 + temp_index / 8;

			if ((temp_index < 0) || (temp_index > points_count))
				temp_x = 0;

			if (e.Button == MouseButtons.Left)
			{
				DRAW_startcur = (int)Math.Round((e.Location.X - DRAW_canvas.X) / DRAW_scale);
				if (DRAW_startcur < 0)
					DRAW_startcur = 0;
				mesure_start_set.Text = temp_x.ToString();
				LOG_Status(
					String.Format(
						$"{DRAW_startcur / 8 + spectrum.x0}    {spectrum.graph[DRAW_startcur]}"
					)
				);
			}

			if (e.Button == MouseButtons.Right)
			{
				DRAW_endcur = (int)Math.Round((e.Location.X - DRAW_canvas.X) / DRAW_scale);
				mesure_end_set.Text = temp_x.ToString();
			}
		}

		void resolution_set_text_changed(object sender, EventArgs e)
		{
			if ((DRAW_resolution = check_value(resolution_set.Text, "Resolution")) == -1)
				DRAW_resolution = 1;
			if (DRAW_resolution == 0)
				DRAW_resolution = 1;
		}

		#endregion
	}
}