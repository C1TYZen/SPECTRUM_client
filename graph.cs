using System;
using System.Windows.Forms;
using System.Threading;

namespace graph1
{
	partial class Graph : Form
	{
		// Команды сервера
		//set
		const int CMD_MC = 25453; // установка количества измерений
		//do
		const int CMD_MB = 25197; // начать измерение
		const int CMD_MR = 29293; // установка диапазона измерений
		const int CMD_MS = 29549; // остановить измерение
		const int CMD_MF = 26221; // поиск

		//set
		const int CMD_DD = 25700; // установка направления
		const int CMD_DM = 28004; // передвинуть двигатель к указаной точке
		const int CMD_DV = 30308; // установка делителя шага
		//do
		const int CMD_DS = 29540; // шаг двигателя
		const int CMD_DI = 26980; // остановка двигателя
		const int CMD_DB = 25188; // направление двигателя назад
		const int CMD_DF = 26212; // направление двигателя вперед
		const int CMD_DC = 25444; // калибровка
		const int CMD_DP = 28772; // вывод информации о положении двигателя

		//set
		const int CMD_ST = 29811; // установка количества шагов
		//do
		const int CMD_CC = 25443; // проверить соединение
		const int CMD_TP = 28788; // тестирование пинов

		//Направления шагового двигателя
		const int DRIVER_BCK = 1;
		const int DRIVER_FWD = -1;

		//Использовал таймер из форм, потому что другой не работает...почему то...
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

		//Флаги
		bool Receive;

		public Graph()
		{
			InitializeComponent();

			//Настройка связи
			Receive = false;
			_baudrate = 76800;

			//Настройка интерфейса
			StartPosition = FormStartPosition.CenterScreen;
			Begin_Button.Enabled = false;
			Stop_Button.Enabled = false;
			Save_Button.Enabled = false;
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Forward_button.Enabled = false;
			Back_button.Enabled = false;
			Callibrate_button.Enabled = false;

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

			spectrum.graph = new int[points_count];
			spectrum.cur = 0;
			spectrum.dir = 1;
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
			DRAW_spectrum(grafx.Graphics);
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
					//spectrum.cur здесь для того, что бы визуально показания 
					//на курсоре совпадали с действительностью
					spectrum.cur += spectrum.dir;
					if((DRAW_cur <= DRAW_range) && (DRAW_cur <= spectrum.cur))
						DRAW_cur += 1;
					spectrum.pos += (float)spectrum.dir / (float)spectrum.div;

					//LOG_Debug($"{spectrum.pos}    {spectrum.cur}");
					LOG_Status(
						String.Format(
							$"{(spectrum.x0 + spectrum.cur)}    {imsg}"
						)
					);
					CONTAINER_Add(imsg);
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
			Save_Button.Enabled = false;
			Begin_Button.Enabled = false;
			Begin_Button.Text = "Измерение";
			New_button.Enabled = false;
			Delete_button.Enabled = false;
			Stop_Button.Enabled = true;
			Forward_button.Enabled = false;
			Back_button.Enabled = false;
			Callibrate_button.Enabled = false;
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
			Callibrate_button.Enabled = true;
			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;

			CONTAINER_Save_on_RAM(TabControl1.SelectedIndex);
			LOG("Готов");
			LOG_Debug("*****");
		}

		/// <summary>
		/// Подготовка к поиску
		/// </summary>
		/// <param name="dir"></param>
		void search_setup(int dir)
		{
			if(TALKER_FlushReadBuf() == -1) return;
			get_armed();
			int steps;
			spectrum.dir = dir;

			if ((steps = check_value(StepsSet.Text, "steps")) == -1)
				steps = 1;
			if ((spectrum.div = check_value(DividerSet.Text, "divider")) == -1)
				spectrum.div = 1;

			/*if((spectrum.cur + dir * steps) > ((spectrum.x1 - spectrum.x0) / spectrum.div))
				steps = ((spectrum.x1 - spectrum.x0) / spectrum.div) - spectrum.cur;
			else if ((spectrum.cur + dir * steps) < 0)
				steps = spectrum.cur;
			if (steps == 0)
			{
				get_ready();
				return;
			}*/

			//Количество шагов
			TALKER_set(CMD_ST, steps);
			//Делитель шага
			TALKER_set(CMD_DV, spectrum.div);
			//Направление
			TALKER_set(CMD_DD, dir);

			DRAW_setup_canvas_scale();
			LOG_Debug($"Scale: {DRAW_scale}");
			LOG_Debug($"Height Scale: {DRAW_height_scale}");

			TALKER_FlushReadBuf();
			Receive = true;
			TALKER_send(CMD_MF, 2);
			TALKER_read_line();
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

			//TALKER_set(CMD_DM, spectrum.x0);
			//Делитель шага
			TALKER_set(CMD_DV, spectrum.div);
			//Количество шагов
			TALKER_set(CMD_ST, (spectrum.x1 - spectrum.x0) * spectrum.div);
			//Число измерений за один шаг двигателя
			TALKER_set(CMD_MC, spectrum.mps);

			//Настройка отрисовки
			DRAW_setup_canvas_scale();
			CONTAINER_Clear();
			DRAW_cur = 0;
			spectrum.dir = 1;
			LOG_Debug($"Scale: {DRAW_scale}");
			LOG_Debug($"Height Scale: {DRAW_height_scale}");
			//Это нужно для того, что бы отрисовывался весь диапазон от х0 до х1,
			//включая крайние точки
			spectrum.cur = -1;
			spectrum.pos = (float)spectrum.x0 - ((float)1 / (float)spectrum.div);

			//Очистка буфера и отправка команды начать
			TALKER_FlushReadBuf();
			LOG($"ИЗМЕРЕНИЕ!");
			Receive = true;
			TALKER_send(CMD_MB, 2);
			TALKER_read_line();
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

			//LOG_Debug($"{temp}");

			if (temp < 0)
				return 0;

			return temp;
		}

		#endregion

		#region Evenst   

		void Begin_Click(object sender, EventArgs e)
		{
			mesure_setup();
		}

		void Forward_button_Click(object sender, EventArgs e)
		{
			search_setup(DRIVER_BCK);
		}

		void Back_button_Click(object sender, EventArgs e)
		{
			search_setup(DRIVER_FWD);
		}

		void button1_Click(object sender, EventArgs e)
		{
			TALKER_send(CMD_DC, 2);
		}

		void Goto_button_Click(object sender, EventArgs e)
		{
			if ((spectrum.x0 = check_value(
					RangeSet0.Text,
					"**ОШИБКА** Incorrect RANGE_0!!!")) == -1)
				spectrum.x0 = 0;

			TALKER_set(CMD_DM, spectrum.x0);
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

			DRAW_setup_sizes();
			grafx = context.Allocate(CreateGraphics(), DRAW_canvas);
			tabgrfx = e.TabPage.CreateGraphics();
			DRAW_setup_canvas_scale();

			RangeSet0.Text = spectrum.x0.ToString();
			RangeSet1.Text = spectrum.x1.ToString();
			MesuresCountSet.Text = spectrum.mps.ToString();

			if (TabControl1.TabCount == 1)
				Delete_button.Enabled = false;
			else
				Delete_button.Enabled = true;
		}

		void Stop_button_Click(object sender, EventArgs e)
		{
			TALKER_send(CMD_DI, 2);
		}

		void Save_Click(object sender, EventArgs e)
		{
			CONTAINER_Save_on_disk();
		}

		void Graph_Load(object sender, EventArgs e)
		{
			DRAW_setup_sizes();
			DRAW_setup_canvas_scale();
			grafx = context.Allocate(CreateGraphics(), DRAW_background);
			tabgrfx = tabPage1.CreateGraphics();
		}

		void Graph_SizeChanged(object sender, EventArgs e)
		{
			//Изменение размеров и масштаба холста
			DRAW_setup_sizes();
			DRAW_setup_canvas_scale();
		}

		void ResolutionSet_TextChanged(object sender, EventArgs e)
		{
			if ((DRAW_resolution = check_value(ResolutionSet.Text, "Resolution")) == -1)
				DRAW_resolution = 1;
			if (DRAW_resolution == 0)
				DRAW_resolution = 1;
		}

		void tabPage1_MouseMove(object sender, MouseEventArgs e)
		{
			MPosition_status_label.Text =
				$"{(e.Location.X - DRAW_canvas.X) / DRAW_scale}    {((DRAW_canvas.Height - (e.Location.Y - DRAW_canvas.Y)) / DRAW_height_scale) / 204.8f}";
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