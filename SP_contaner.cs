using System;
using System.IO;
using System.Drawing;

namespace graph1
{
	/// <summary>
	/// Класс - контейнер для данных.
	/// </summary>
	public static class SP_contaner
	{
		public static int cur = 0;
		public static Point[] points = new Point[50000];
		public static float scale;

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
		/// Сохранение графика в файл в виде таблицы.
		/// </summary>
		public static void Save()
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
