using System.Drawing;
using System.Windows.Forms;

namespace graph1
{
	partial class Graph
	{
		Rectangle DRAW_canvas;
		Rectangle DRAW_background;
		Pen pen = new Pen(SystemColors.HighlightText);
		BufferedGraphicsContext context = BufferedGraphicsManager.Current;
		BufferedGraphics grafx;
		Graphics tabgrfx;
		int DRAW_resolution = 1;
		float DRAW_scale;
		float DRAW_height_scale;
		int DRAW_range = 100;
		int DRAW_range_scale = 10;

		int DRAW_startcur = 0;
		int DRAW_endcur = 0;

		/// <summary>
		/// Вызывается при запросе "перерисовать" (Ivalidate).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DRAW(object sender, PaintEventArgs e)
		{
			grafx.Render(tabgrfx);
		}

		void DRAW_line(Graphics g, Pen p, Rectangle rect, float x1, float y1, float x2, float y2)
		{
			g.DrawLine(p, rect.X + x1, rect.Y + y1, rect.X + x2, rect.Y + y2);
		}
		
		void DRAW_point(Graphics g, Pen p, Rectangle rect, float x1, float y1)
		{
			g.DrawRectangle(p, rect.X + x1, rect.Y + y1, 1, 1);
		}

		void DRAW_grid(Graphics g)
		{
			pen.Color = SystemColors.HighlightText;
			g.FillRectangle(SystemBrushes.Highlight, DRAW_background);
			g.FillRectangle(SystemBrushes.Highlight, DRAW_canvas);
			pen.Color = SystemColors.GrayText;
			if (DRAW_range < 10)
				DRAW_range_scale = DRAW_range;
			else
				DRAW_range_scale = 10;

			//Вертикальные линии
			for (int i = 0; i <= DRAW_range; i += DRAW_range / DRAW_range_scale)
			{
				DRAW_line(g, pen, DRAW_canvas, 
					i * DRAW_scale, 
					0, 
					i * DRAW_scale,
					DRAW_canvas.Height
				);
			}

			//Горизонтальные линии
			for (float i = 0; i <= 1024; i += (float)1024 / (float)5)
			{
				DRAW_line(g, pen, DRAW_canvas, 
					0, 
					i * DRAW_height_scale,
					DRAW_canvas.Width,
					i * DRAW_height_scale
				);
			}
		}

		void DRAW_curs(Graphics g)
		{
			pen.Color = Color.GreenYellow;
			DRAW_line(g, pen, DRAW_canvas,
				DRAW_startcur * DRAW_scale,
				0,
				DRAW_startcur * DRAW_scale,
				DRAW_canvas.Height
			);

			pen.Color = Color.Red;
			DRAW_line(g, pen, DRAW_canvas,
				DRAW_endcur * DRAW_scale,
				0,
				DRAW_endcur * DRAW_scale,
				DRAW_canvas.Height
			);
		}

		void DRAW_spectrum_line(Graphics g, plot p)
		{
			// Отрисовка спектра
			pen.Color = SystemColors.HighlightText;
			for (int i = 0; i <= DRAW_range; i += DRAW_resolution)
			{
				if (((i - DRAW_resolution) >= 0) && (i <= p.end))
				{
					DRAW_line(g, pen, DRAW_canvas,
						i * DRAW_scale,
						DRAW_canvas.Height - (p.graph[i] * DRAW_height_scale),
						(i - DRAW_resolution) * DRAW_scale,
						DRAW_canvas.Height - (p.graph[i - DRAW_resolution] * DRAW_height_scale)
					);
				}
			}
		}

		void DRAW_spectrum_dot(Graphics g, plot p)
		{
			// Отрисовка спектра
			pen.Color = SystemColors.HighlightText;
			for (int i = 0; i <= DRAW_range; i += DRAW_resolution)
			{
				if (((i - DRAW_resolution) >= 0) && (i <= p.end))
				{
					DRAW_point(g, pen, DRAW_canvas,
						i * DRAW_scale,
						DRAW_canvas.Height - (p.graph[i] * DRAW_height_scale)
					);
				}
			}
		}
	}
}