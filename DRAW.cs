using System.Drawing;
using System.Windows.Forms;

namespace graph1
{
	partial class Graph
	{
		//Настройка графики
		Rectangle canvas;
		Rectangle background;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;
		int resolution = 1;
		float scale;
		float height_scale;
		int range = 100;

		void DRAW_setup_sizes()
		{
			background = new Rectangle(0, 0, tabPage1.Width, tabPage1.Height);
			canvas = new Rectangle(10, 10, tabPage1.Width - 20, tabPage1.Height - 20);
			context.MaximumBuffer = new Size(tabPage1.Width + 1, tabPage1.Height + 1);
		}

		/// <summary>
		/// Вычисление масштаба холста
		/// </summary>
		void DRAW_setup_canvas_scale()
		{
			//вычисление масштаба горизонтальной шкалы
			range = spectrum.x1 - spectrum.x0;
			scale = canvas.Width / (float)range;
			height_scale = canvas.Height / (float)1024;
			LOG_Debug($"Scale: {scale}");
			LOG_Debug($"Height Scale: {height_scale}");
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

		void DRAW_grid(Graphics g)
		{
			pen.Color = SystemColors.HighlightText;
			g.FillRectangle(SystemBrushes.Highlight, background);
			g.FillRectangle(SystemBrushes.Highlight, canvas);
			pen.Color = SystemColors.GrayText;

			for (int i = 0; i <= range; i += range / 10)
			{
				DRAW_line(g, pen, canvas, i * scale, 0, i * scale, canvas.Height);
			}

			for (int i = 0; i <= canvas.Height; i += canvas.Height / 10)
			{
				DRAW_line(g, pen, canvas, 0, canvas.Height - i, canvas.Width, canvas.Height - i);
			}
		}

		/// <summary>
		/// Отрисовка спектра в буфер
		/// </summary>
		/// <param name="g"></param>
		void DRAW_to_buffer(Graphics g)
		{
			pen.Color = Color.Red;
			DRAW_line(g, pen, canvas,
				spectrum.cur * scale,
				0,
				spectrum.cur * scale,
				canvas.Height
			);
			pen.Color = SystemColors.HighlightText;
			for (int i = 0; i <= range; i += resolution)
			{
				if ((i - resolution) >= 0)
				{
					DRAW_line(g, pen, canvas,
						i * scale,
						canvas.Height - (spectrum.graph[i] * height_scale),
						(i - resolution) * scale,
						canvas.Height - (spectrum.graph[i - resolution] * height_scale)
					);
				}
			}
		}

		void DRAW_line(Graphics g, Pen p, Rectangle rect, float x1, float y1, float x2, float y2)
		{
			g.DrawLine(p, rect.X + x1, rect.Y + y1, rect.X + x2, rect.Y + y2);
		}
	}
}
