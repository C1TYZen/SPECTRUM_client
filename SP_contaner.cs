using System;
using System.IO;
using System.Drawing;

namespace graph1
{
	/// <summary>
	/// Класс - контейнер для данных.
	/// </summary>
	static class SP_contaner
	{
		const int points_count = 150000;
		const int plots_count = 50;

		public static int cur = 0;
		public static float scale;

		public static Point[] points = new Point[points_count];
		public static int range0;
		public static int range1;
		public static int mps;
		public static int filter;

		static Point[][] saved_points = new Point[plots_count][];
		static int[] saved_cur = new int[plots_count];
		static int[] saved_range0 = new int[plots_count];
		static int[] saved_range1 = new int[plots_count];
		static int[] saved_mps = new int[plots_count];
		static int[] saved_filter = new int[plots_count];

		/// <summary>
		/// Добавление элемента таблицы.
		/// </summary>
		/// <param name="bt"></param>
		public static void Add(int bt)
		{
			points[cur].X = (int)(scale * cur);
			points[cur].Y = bt;
			cur++;
		}

		/// <summary>
		/// Сохранение спектра в файл в виде таблицы.
		/// </summary>
		public static void Save_on_disk()
		{
			string time = String.Format("{0}_{1}_{2} {3}_{4}_{5}",
				DateTime.Now.Day, DateTime.Now.Month,
				DateTime.Now.Year, DateTime.Now.Hour,
				DateTime.Now.Minute, DateTime.Now.Second);
			string path = String.Format("plots/{0}.txt", time);

			if(!Directory.Exists("plots"))
				Directory.CreateDirectory("plots");

			using (StreamWriter outputFile = new StreamWriter(path, true))
			{
				for(int r = 0; r < cur; r++)
				{
					outputFile.WriteLine("{0}\t{1}", points[r].X, points[r].Y);
				}
				SP_Log.Log($"Saved, {time}.txt");
			}
		}

		/// <summary>
		/// Сохраниение спектра в буфер.
		/// </summary>
		/// <param name="c"></param>
		public static void Save_on_RAM(int c)
		{
			saved_points[c] = points;
			saved_cur[c] = cur;
			saved_range0[c] = range0;
			saved_range1[c] = range1;
			saved_mps[c] = mps;
			Console.WriteLine($"Спектр{c + 1} сохранен");
		}

		/// <summary>
		/// Загрузка спектра из буфера.
		/// </summary>
		/// <param name="c"></param>
		public static void Load_from_RAM(int c)
		{
			if(saved_points[c] != null)
			{
				points = saved_points[c];
				cur = saved_cur[c];
				range0 = saved_range0[c];
				range1 = saved_range1[c];
				mps = saved_mps[c];
				Console.WriteLine($"Спектр{c + 1} загружен");
			}
			else
			{
				points = new Point[points_count];
				Console.WriteLine($"Создан Спектр{c + 1}");
				Save_on_RAM(c);
			}
		}

		/// <summary>
		/// Удаление спектра из буфера.
		/// </summary>
		/// <param name="c"></param>
		/// <param name="max"></param>
		public static void Delete_from_RAM(int c, int max)
		{
			saved_points[c] = null;
			if(max > c)
			{
				while(c < max)
				{
					saved_points[c] = saved_points[c + 1];
					c++;
				}
			}
		}

		public static void Reset()
		{
			cur = 0;
		}

		public static void Clear()
		{
			for(int i = 0; i < points.Length; i++)
			{
				points[i].X = 0;
				points[i].Y = 0;
			}
			cur = 0;
		}
	}
}
