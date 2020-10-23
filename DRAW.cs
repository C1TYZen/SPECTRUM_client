using System.Drawing;
using System.Windows.Forms;

namespace graph1
{
	partial class Graph
	{
		Rectangle canvas;
		Rectangle background;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;
		int resolution = 1;
		float DRAW_scale;
		float DRAW_height_scale;
		int DRAW_range = 100;
		int DRAW_range_scale = 10;
		int DRAW_cur = 0;

		void DRAW_line(Graphics g, Pen p, Rectangle rect, float x1, float y1, float x2, float y2)
		{
			g.DrawLine(p, rect.X + x1, rect.Y + y1, rect.X + x2, rect.Y + y2);
		}

		/// <summary>
		/// Вычисление размеров элементов окна и самого окна
		/// </summary>
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
			//вычисление масштаба горизонтальной и вертикальной шкал
			DRAW_range = spectrum.x1 - spectrum.x0;
			DRAW_scale = canvas.Width / (float)DRAW_range;
			DRAW_height_scale = canvas.Height / (float)1021;
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
			if (DRAW_range < 10)
				DRAW_range_scale = DRAW_range;
			else
				DRAW_range_scale = 10;

			for (int i = 0; i <= DRAW_range; i += DRAW_range / DRAW_range_scale)
			{
				DRAW_line(g, pen, canvas, 
					i * DRAW_scale, 
					0, 
					i * DRAW_scale, 
					canvas.Height
				);
			}

			for (int i = 0; i <= 1024; i += 1024 / 5)
			{
				DRAW_line(g, pen, canvas, 
					0, 
					i * DRAW_height_scale, 
					canvas.Width,
					i * DRAW_height_scale
				);
			}
		}

		void DRAW_spectrum(Graphics g)
		{
			// Курсор
			pen.Color = Color.Red;
			DRAW_line(g, pen, canvas,
				spectrum.pos * DRAW_scale,
				0,
				spectrum.pos * DRAW_scale,
				canvas.Height
			);

			// Отрисовка спектра
			pen.Color = SystemColors.HighlightText;
			for (int i = 0; i <= DRAW_range; i += resolution)
			{
				if (((i - resolution) >= 0) && (i <= DRAW_cur))
				{
					DRAW_line(g, pen, canvas,
						i * DRAW_scale,
						canvas.Height - (spectrum.graph[i] * DRAW_height_scale),
						(i - resolution) * DRAW_scale,
						canvas.Height - (spectrum.graph[i - resolution] * DRAW_height_scale)
					);
				}
			}
		}
	}
}
