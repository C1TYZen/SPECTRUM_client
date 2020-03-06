using System;
using System.Drawing;
using System.Windows.Forms;

namespace graph1
{
	public partial class Form1 : Form
	{
		Point moveStart;

		SP_talker talker = new SP_talker();

		Timer timer = new Timer();
		int FPS = 60;

		Rectangle canvas = new Rectangle(0, 0, 800, 300);
		Pen pen = new Pen(SystemColors.HighlightText);
		Pen sysPen = new Pen(SystemColors.Highlight);
		BufferedGraphicsContext context;
		BufferedGraphics grafx;

		public Form1()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterScreen;

			timer.Enabled = true;
			timer.Interval = 1000 / FPS;
			timer.Tick += new EventHandler(timerUpdate);

			//graphics = CreateGraphics();
			context = BufferedGraphicsManager.Current;
			context.MaximumBuffer = new Size(Width + 1, Height + 1);
			grafx = context.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));

			talker.open();
		}

		void timerUpdate(object sender, EventArgs e)
		{
			DrawToBuffer(grafx.Graphics);
			Invalidate();
		}

		private void draw(object sender, PaintEventArgs e)
		{
			grafx.Render(e.Graphics);
		}

		private void DrawToBuffer(Graphics g)
		{
			grafx.Graphics.FillRectangle(SystemBrushes.Highlight, 0, 0, Width, Height);
			g.DrawLine(pen, 0, 250, 800, 250);
			for (int i = 0; i <= SP_contaner.cur; i++)
			{
				g.DrawEllipse(pen, SP_contaner.points[i].X * 2, 250 - SP_contaner.points[i].Y/5, 1, 1);
			}
		}

		private void Send_Click(object sender, EventArgs e)
		{
			SP_contaner.reset();
			byte[] msg = new byte[2];
			msg[0] = 98;	//b
			msg[1] = 109;	//m
			talker.send(msg, 0, 2);
		}

		private void Save_Click(object sender, EventArgs e)
		{
			SP_contaner.save();
		}

		private void Close_Click(object sender, EventArgs e)
		{
			talker.Dispose();
			Close();
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				moveStart = new Point(e.X, e.Y);
			}
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != 0)
			{
				Point deltaPos = new Point(e.X - moveStart.X, e.Y - moveStart.Y);
				Location = new Point(Location.X + deltaPos.X, Location.Y + deltaPos.Y);
			}
		}
	}
}
