using System;
using System.Windows.Forms;

namespace graph1
{
	partial class Graph : Form
	{
		// Команды сервера
		//set
		const int CMD_MC = 25453; // установка количества измерений за шаг
		//do
		const int CMD_MB = 25197; // начать измерение
		const int CMD_MS = 29549; // остановить измерение

		//set
		const int CMD_DM = 28004; // установка начала измерения
		const int CMD_DV = 30308; // установка делителя шага
		//do
		const int CMD_DI = 26980; // остановка двигателя
		const int CMD_DC = 25444; // калибровка

		//set
		const int CMD_ST = 29811; // установка количества шагов
		//do
		const int CMD_CC = 25443; // проверить соединение

		//Разное
		Timer timer = new Timer();
		bool Receive;

		public Graph()
		{
			InitializeComponent();

			//Настройка связи
			Receive = false;
			_baudrate = 76800;

			//Настройка интерфейса
			StartPosition = FormStartPosition.CenterScreen;
			Begin_button.Enabled = false;
			Stop_button.Enabled = false;
			Save_button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Callibrate_button.Enabled = false;

			//Настройка таймера
			timer.Enabled = true;
			timer.Interval = 1;
			timer.Tick += new EventHandler(timer_update);

			//Установка дефолтных значений
			RangeSet0.Text = "0";
			RangeSet1.Text = "100";
			MesuresCountSet.Text = "1";
			resolution_set.Text = "1";
			DividerSet.Text = "1";

			spectrum.graph = new int[points_count];
			spectrum.end = 0;
			spectrum.x0 = 0;
			spectrum.x1 = 100;
			spectrum.pos = 0;
			spectrum.mps = 1;
			spectrum.filter = 1;
			spectrum.div = 1;

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
				for (int i = 0; i < 1000; i++)
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
			if (_serialPort.BytesToRead >= 2)
			{
				TALKER_read(bmsg, 0, 2);
				imsg = bmsg[0] + (bmsg[1] << 8);
				LOG_Debug($"{imsg}");
				if (imsg != CMD_MS)
				{
					spectrum.end += 1;
					spectrum.pos += (float)1/ (float)spectrum.div;
					CONTAINER_Add(imsg, spectrum);
				}
				else
				{
					LOG_Debug("");
					get_ready();
				}
			}
		}

		#region Setup

		/// <summary>
		/// Подготовка интерфейса к измерению
		/// </summary>
		void get_armed()
		{
			Save_button.Enabled = false;
			Begin_button.Enabled = false;
			Begin_button.Text = "Измерение";
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Stop_button.Enabled = true;
			Callibrate_button.Enabled = false;
		}

		/// <summary>
		/// Возвращение интерфейса в активное состояние
		/// </summary>
		void get_ready()
		{
			Receive = false;
			Begin_button.Text = "Начать";
			Begin_button.Enabled = true;
			Stop_button.Enabled = false;
			Save_button.Enabled = true;
			New_button.Enabled = true;
			Callibrate_button.Enabled = true;
			if (tab_control1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;

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
				new System.Drawing.Rectangle(10, 10, tabPage1.Width - 20, tabPage1.Height - 20);
			context.MaximumBuffer =
				new System.Drawing.Size(tabPage1.Width + 1, tabPage1.Height + 1);
		}

		/// <summary>
		/// Вычисление масштаба холста
		/// </summary>
		void setup_canvas_scale()
		{
			DRAW_range = (spectrum.x1 - spectrum.x0) * spectrum.div;
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
			if (TALKER_FlushReadBuf() == -1) return;
			get_armed();

			//Проверка значений и запись в память
			if ((spectrum.x0 = check_value(RangeSet0.Text, "RANGE_0")) == -1)
				spectrum.x0 = 0;
			if ((spectrum.x1 = check_value(RangeSet1.Text,"RANGE_1")) == -1)
				spectrum.x1 = 100;
			if (spectrum.x0 > spectrum.x1)
			{
				int buf = spectrum.x0;
				spectrum.x0 = spectrum.x1;
				spectrum.x1 = buf;
			}
			if ((spectrum.mps = check_value(MesuresCountSet.Text, "Mesures Count")) == -1)
				spectrum.mps = 1;
			if ((spectrum.div = check_value(DividerSet.Text, "Divider")) == -1)
				spectrum.div = 1;

			//Начало измерения
			TALKER_set(CMD_DM, spectrum.x0);
			//Делитель шага
			TALKER_set(CMD_DV, spectrum.div);
			//Количество шагов
			TALKER_set(CMD_ST, spectrum.x1 - spectrum.x0);
			//Число измерений за один шаг двигателя
			TALKER_set(CMD_MC, spectrum.mps);

			//Настройка отрисовки
			setup_canvas_scale();
			CONTAINER_Clear();
			LOG_Debug($"Scale: {DRAW_scale}");
			LOG_Debug($"Height Scale: {DRAW_height_scale}");
			//Это нужно для того, что бы отрисовывался весь диапазон от х0 до х1,
			//включая крайние точки
			spectrum.end = -1;
			spectrum.pos = (float)spectrum.x0 - ((float)1 / (float)spectrum.div);

			//Очистка буфера и отправка команды начать
			TALKER_FlushReadBuf();
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
			TALKER_send(CMD_DC);
		}

		void BUTTON_Goto_click(object sender, EventArgs e)
		{
			if ((spectrum.x0 = check_value(
					RangeSet0.Text,
					"**ОШИБКА** Incorrect RANGE_0!!!")) == -1)
				spectrum.x0 = 0;

			TALKER_set(CMD_DM, spectrum.x0);
		}

		void BUTTON_New_click(object sender, EventArgs e)
		{
			TabPage newTabPage = new TabPage("Спектр " + (tab_control1.TabCount + 1).ToString());
			tab_control1.TabPages.Add(newTabPage);
			tab_control1.SelectedIndex = tab_control1.TabPages.IndexOf(newTabPage);

			newTabPage.MouseClick += new System.Windows.Forms.MouseEventHandler(tab_page_mouse_click);
			newTabPage.MouseMove += new System.Windows.Forms.MouseEventHandler(tab_page_mouse_move);
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
			TALKER_send(CMD_DI);
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

			tabPage1.MouseClick += new System.Windows.Forms.MouseEventHandler(tab_page_mouse_click);
			tabPage1.MouseMove += new System.Windows.Forms.MouseEventHandler(tab_page_mouse_move);
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

			RangeSet0.Text = spectrum.x0.ToString();
			RangeSet1.Text = spectrum.x1.ToString();
			MesuresCountSet.Text = spectrum.mps.ToString();

			if (tab_control1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;
		}

		void tab_page_mouse_move(object sender, MouseEventArgs e)
		{
			int temp_index = (int)((e.Location.X - DRAW_canvas.X) / DRAW_scale);
			int temp_x = spectrum.x0 + temp_index / spectrum.div;

			if ((temp_index < 0) || (temp_index > points_count))
				temp_index = 0;
			int temp_value = spectrum.graph[temp_index];

			MPosition_status_label.Text = $"X: {temp_x}  Знач: {temp_value}";
		}

		void tab_page_mouse_click(object sender, MouseEventArgs e)
		{
			int temp_index = (int)((e.Location.X - DRAW_canvas.X) / DRAW_scale);
			int temp_x = spectrum.x0 + temp_index / spectrum.div;
			/*if (temp_x <= spectrum.x0)
				temp_x = 0;
			if (temp_x > spectrum.x1)
				temp_x = spectrum.x1;*/

			if ((temp_index < 0) || (temp_index > points_count))
				temp_x = 0;

			if (e.Button == MouseButtons.Left)
			{
				DRAW_startcur = (int)Math.Round((e.Location.X - DRAW_canvas.X) / DRAW_scale);
				RangeSet0.Text = temp_x.ToString();
				LOG_Status(
					String.Format(
						$"{DRAW_startcur + spectrum.x0}    {spectrum.graph[DRAW_startcur]}"
					)
				);
			}

			if (e.Button == MouseButtons.Right)
			{
				DRAW_endcur = (int)Math.Round((e.Location.X - DRAW_canvas.X) / DRAW_scale);
				RangeSet1.Text = temp_x.ToString();
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