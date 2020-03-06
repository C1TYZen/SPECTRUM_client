using System;
using System.IO;
using System.Drawing;

namespace graph1
{
	/// <summary>
	/// Класс - контейнер для данных
	/// </summary>
	public static class SP_contaner
	{
		public static int cur = 0;
		public static Point[] points = new Point[1000];

		public static void add(int bt)
		{
			points[cur].X = cur;
			points[cur].Y = bt;
			cur++;
		}

		public static void save()
		{
			using (StreamWriter outputFile = new StreamWriter("plot.txt", true))
			{
				for(int r = 0; r < cur; r++)
				{
					outputFile.WriteLine("{0}\t{1}", points[r].X, points[r].Y);
				}
				Console.WriteLine("Saved");
			}
		}

		public static void reset()
		{
			cur = 0;
		}
	}
}
