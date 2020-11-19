using System;
using System.IO;

// Контейнер для данных.

namespace graph1
{
	partial class Graph
	{
		struct plot
		{
			public int[] graph; //Массив точек
			public int end;		//Конец графика
			public float pos;	//Положение двигателя
			public int x0;		//Начало диапазона
			public int x1;		//Конец диапазона
			public int mps;		//Измерений за шаг
			public int filter;	//Номер фильтра
			public int div;		//Делитель шага
		}

		const int points_count = 532480;
		const int plots_count = 64;

		plot spectrum = new plot();
		plot[] memory = new plot[plots_count];

		/// <summary>
		/// Добавление элемента таблицы
		/// </summary>
		/// <param name="element"></param>
		/// <param name="table"></param>
		void CONTAINER_Add(int element, plot table)
		{
			if (table.end < 0)
				table.end = 0;
			if (table.end > DRAW_range)
				table.end = DRAW_range;
			if (table.end > points_count)
				table.end = points_count;
			table.graph[table.end] = element;
		}

		/// <summary>
		/// Сохраниение спектра в буфер
		/// </summary>
		/// <param name="c"></param>
		void CONTAINER_Save_on_RAM(int c)
		{
			memory[c] = spectrum;
			LOG_Debug($"Спектр{c + 1} сохранен");
		}

		/// <summary>
		/// Загрузка спектра из буфера
		/// </summary>
		/// <param name="c"></param>
		void CONTAINER_Load_from_RAM(int c)
		{
			if (memory[c].graph != null)
			{
				spectrum = memory[c];
				LOG_Debug($"Спектр{c + 1} загружен");
			}
			else
			{
				spectrum.graph = new int[points_count];
				LOG_Debug($"Создан Спектр{c + 1}");
			}
		}

		/// <summary>
		/// Удаление спектра из буфера.
		/// </summary>
		/// <param name="c"></param>
		/// <param name="max"></param>
		void CONTAINER_Delete_from_RAM(int c, int max)
		{
			memory[c].graph = null;
			if (max > c)
			{
				while (c < max)
				{
					memory[c].graph = memory[c + 1].graph;
					c++;
				}
			}
		}

		/// <summary>
		/// Сохранение спектра в файл в виде таблицы
		/// </summary>
		void CONTAINER_Save_on_disk()
		{
			string time = String.Format("{0}-{1}-{2} {3}_{4}_{5}",
				DateTime.Now.Day, DateTime.Now.Month,
				DateTime.Now.Year, DateTime.Now.Hour,
				DateTime.Now.Minute, DateTime.Now.Second);
			string path = String.Format("plots/{0}.txt", time);

			if (!Directory.Exists("plots"))
				Directory.CreateDirectory("plots");

			using (StreamWriter outputFile = new StreamWriter(path, true))
			{
				for (int r = 0; r <= spectrum.end; r++)
					outputFile.WriteLine("{0}\t{1}", r + spectrum.x0, spectrum.graph[r]);
				LOG($"Сохранено, {time}.txt");
			}
		}

		/// <summary>
		/// Очистка текущего спектра
		/// </summary>
		void CONTAINER_Clear()
		{
			for (int i = 0; i < spectrum.graph.Length; i++)
			{
				spectrum.graph[i] = 0;
			}
			spectrum.end = 0;
		}

		/// <summary>
		/// Возвращение курсора в начальное положение
		/// </summary>
		void CONTAINER_Reset()
		{
			spectrum.end = 0;
		}
	}
}
