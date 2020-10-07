using System;
using System.IO;
using System.Drawing;

// Контейнер для данных.

namespace graph1
{
	partial class Graph
	{
		const int points_count = 150000;
		const int plots_count = 50;
		int CONTAINER_cur = 0;
		int CONTAINER_curdir = 1;

		int[] CONTAINER_graph = new int[points_count];
		int CONTAINER_x0 = 0;
		int CONTAINER_x1 = 100;
		int CONTAINER_range = 100;
		int CONTAINER_mps = 1;
		int filter = 1;

		int[][] saved_graph = new int[plots_count][];
		int[] saved_cur = new int[plots_count];
		int[] saved_range0 = new int[plots_count];
		int[] saved_range1 = new int[plots_count];
		int[] saved_mps = new int[plots_count];
		int[] saved_filter = new int[plots_count];

		/// <summary>
		/// Добавление элемента таблицы.
		/// </summary>
		/// <param name="bt"></param>
		void CONTAINER_Add(int bt)
		{
			if (CONTAINER_cur < 0)
				CONTAINER_cur = 0;
			if (CONTAINER_cur > points_count)
				CONTAINER_cur = points_count;
			CONTAINER_graph[CONTAINER_cur] = bt;
		}

		/// <summary>
		/// Сохранение спектра в файл в виде таблицы.
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
				for (int r = 0; r < CONTAINER_cur; r++)
					outputFile.WriteLine("{0}\t{1}", r + 1, CONTAINER_graph[r]);
				LOG($"Сохранено, {time}.txt");
			}
		}

		/// <summary>
		/// Сохраниение спектра в буфер.
		/// </summary>
		/// <param name="c"></param>
		void CONTAINER_Save_on_RAM(int c)
		{
			saved_graph[c] = CONTAINER_graph;
			saved_cur[c] = CONTAINER_cur;
			saved_range0[c] = CONTAINER_x0;
			saved_range1[c] = CONTAINER_x1;
			saved_mps[c] = CONTAINER_mps;
			LOG_Debug($"Спектр{c + 1} сохранен");
		}

		/// <summary>
		/// Загрузка спектра из буфера.
		/// </summary>
		/// <param name="c"></param>
		void CONTAINER_Load_from_RAM(int c)
		{
			if (saved_graph[c] != null)
			{
				CONTAINER_graph = saved_graph[c];
				CONTAINER_cur = saved_cur[c];
				CONTAINER_x0 = saved_range0[c];
				CONTAINER_x1 = saved_range1[c];
				CONTAINER_mps = saved_mps[c];
				LOG_Debug($"Спектр{c + 1} загружен");
			}
			else
			{
				CONTAINER_graph = new int[points_count];
				LOG_Debug($"Создан Спектр{c + 1}");
				CONTAINER_Save_on_RAM(c);
			}
		}

		/// <summary>
		/// Удаление спектра из буфера.
		/// </summary>
		/// <param name="c"></param>
		/// <param name="max"></param>
		void CONTAINER_Delete_from_RAM(int c, int max)
		{
			saved_graph[c] = null;
			if (max > c)
			{
				while (c < max)
				{
					saved_graph[c] = saved_graph[c + 1];
					c++;
				}
			}
		}

		void CONTAINER_Clear()
		{
			for (int i = 0; i < CONTAINER_graph.Length; i++)
			{
				CONTAINER_graph[i] = 0;
			}
			CONTAINER_cur = 0;
		}

		void CONTAINER_Reset()
		{
			CONTAINER_cur = 0;
		}
	}
}
