﻿namespace graph1
{
    partial class Graph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.Begin_Button = new System.Windows.Forms.Button();
			this.Save_Button = new System.Windows.Forms.Button();
			this.Close_Button = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.NumOfSteps = new System.Windows.Forms.TextBox();
			this.Text_console = new System.Windows.Forms.TextBox();
			this.ResolutionSet = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.RangeSet0 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.FilterSet = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.MesuresCountSet = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.RangeSet1 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.Mesure_stat_label = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menustrip_File = new System.Windows.Forms.ToolStripMenuItem();
			this.menustrip_Tools = new System.Windows.Forms.ToolStripMenuItem();
			this.menustrip_COM = new System.Windows.Forms.ToolStripMenuItem();
			this.menustrip_BaudRate = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tabControl1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Begin_Button
			// 
			this.Begin_Button.Location = new System.Drawing.Point(167, 19);
			this.Begin_Button.Name = "Begin_Button";
			this.Begin_Button.Size = new System.Drawing.Size(75, 23);
			this.Begin_Button.TabIndex = 1;
			this.Begin_Button.Text = "Begin";
			this.Begin_Button.UseVisualStyleBackColor = true;
			this.Begin_Button.Click += new System.EventHandler(this.Begin_Click);
			// 
			// Save_Button
			// 
			this.Save_Button.Location = new System.Drawing.Point(167, 48);
			this.Save_Button.Name = "Save_Button";
			this.Save_Button.Size = new System.Drawing.Size(75, 23);
			this.Save_Button.TabIndex = 2;
			this.Save_Button.Text = "Save";
			this.Save_Button.UseVisualStyleBackColor = true;
			this.Save_Button.Click += new System.EventHandler(this.Save_Click);
			// 
			// Close_Button
			// 
			this.Close_Button.Location = new System.Drawing.Point(167, 77);
			this.Close_Button.Name = "Close_Button";
			this.Close_Button.Size = new System.Drawing.Size(75, 23);
			this.Close_Button.TabIndex = 3;
			this.Close_Button.Text = "Close";
			this.Close_Button.UseVisualStyleBackColor = true;
			this.Close_Button.Click += new System.EventHandler(this.Close_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(12, 36);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(517, 295);
			this.tabControl1.TabIndex = 8;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(509, 269);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Спектр1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Шагов";
			// 
			// NumOfSteps
			// 
			this.NumOfSteps.Location = new System.Drawing.Point(9, 32);
			this.NumOfSteps.Name = "NumOfSteps";
			this.NumOfSteps.Size = new System.Drawing.Size(121, 20);
			this.NumOfSteps.TabIndex = 10;
			// 
			// Text_console
			// 
			this.Text_console.AcceptsReturn = true;
			this.Text_console.AcceptsTab = true;
			this.Text_console.AllowDrop = true;
			this.Text_console.BackColor = System.Drawing.Color.Black;
			this.Text_console.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Text_console.ForeColor = System.Drawing.SystemColors.Window;
			this.Text_console.Location = new System.Drawing.Point(12, 337);
			this.Text_console.Multiline = true;
			this.Text_console.Name = "Text_console";
			this.Text_console.ReadOnly = true;
			this.Text_console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Text_console.Size = new System.Drawing.Size(517, 124);
			this.Text_console.TabIndex = 11;
			// 
			// ResolutionSet
			// 
			this.ResolutionSet.Location = new System.Drawing.Point(9, 71);
			this.ResolutionSet.Name = "ResolutionSet";
			this.ResolutionSet.Size = new System.Drawing.Size(121, 20);
			this.ResolutionSet.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Разрешение";
			// 
			// RangeSet0
			// 
			this.RangeSet0.Location = new System.Drawing.Point(9, 110);
			this.RangeSet0.Name = "RangeSet0";
			this.RangeSet0.Size = new System.Drawing.Size(121, 20);
			this.RangeSet0.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 94);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "x0";
			// 
			// FilterSet
			// 
			this.FilterSet.Location = new System.Drawing.Point(9, 227);
			this.FilterSet.Name = "FilterSet";
			this.FilterSet.Size = new System.Drawing.Size(121, 20);
			this.FilterSet.TabIndex = 17;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 211);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Фильтр";
			// 
			// MesuresCountSet
			// 
			this.MesuresCountSet.Location = new System.Drawing.Point(9, 188);
			this.MesuresCountSet.Name = "MesuresCountSet";
			this.MesuresCountSet.Size = new System.Drawing.Size(121, 20);
			this.MesuresCountSet.TabIndex = 19;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 172);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Измерение/шаг";
			// 
			// RangeSet1
			// 
			this.RangeSet1.Location = new System.Drawing.Point(9, 149);
			this.RangeSet1.Name = "RangeSet1";
			this.RangeSet1.Size = new System.Drawing.Size(121, 20);
			this.RangeSet1.TabIndex = 21;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 133);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(18, 13);
			this.label8.TabIndex = 20;
			this.label8.Text = "x1";
			// 
			// Mesure_stat_label
			// 
			this.Mesure_stat_label.AutoSize = true;
			this.Mesure_stat_label.Location = new System.Drawing.Point(535, 448);
			this.Mesure_stat_label.Name = "Mesure_stat_label";
			this.Mesure_stat_label.Size = new System.Drawing.Size(75, 13);
			this.Mesure_stat_label.TabIndex = 22;
			this.Mesure_stat_label.Text = "Mesure Status";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(535, 430);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 13);
			this.label9.TabIndex = 23;
			this.label9.Text = "Mesure Status";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menustrip_File,
            this.menustrip_Tools});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 24;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menustrip_File
			// 
			this.menustrip_File.Name = "menustrip_File";
			this.menustrip_File.Size = new System.Drawing.Size(45, 20);
			this.menustrip_File.Text = "Файл";
			// 
			// menustrip_Tools
			// 
			this.menustrip_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menustrip_COM,
            this.menustrip_BaudRate});
			this.menustrip_Tools.Name = "menustrip_Tools";
			this.menustrip_Tools.Size = new System.Drawing.Size(87, 20);
			this.menustrip_Tools.Text = "Инструменты";
			// 
			// menustrip_COM
			// 
			this.menustrip_COM.Name = "menustrip_COM";
			this.menustrip_COM.Size = new System.Drawing.Size(121, 22);
			this.menustrip_COM.Text = "COM";
			// 
			// menustrip_BaudRate
			// 
			this.menustrip_BaudRate.Name = "menustrip_BaudRate";
			this.menustrip_BaudRate.Size = new System.Drawing.Size(121, 22);
			this.menustrip_BaudRate.Text = "BaudRate";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.Begin_Button);
			this.groupBox1.Controls.Add(this.Save_Button);
			this.groupBox1.Controls.Add(this.RangeSet1);
			this.groupBox1.Controls.Add(this.Close_Button);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.NumOfSteps);
			this.groupBox1.Controls.Add(this.MesuresCountSet);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.ResolutionSet);
			this.groupBox1.Controls.Add(this.FilterSet);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.RangeSet0);
			this.groupBox1.Location = new System.Drawing.Point(538, 58);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(254, 269);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			// 
			// Graph
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(792, 473);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.Mesure_stat_label);
			this.Controls.Add(this.Text_console);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Graph";
			this.Text = "Graph_v0.4a";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Draw);
			this.tabControl1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Begin_Button;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Close_Button;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox NumOfSteps;
		private System.Windows.Forms.TextBox Text_console;
		private System.Windows.Forms.TextBox ResolutionSet;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox RangeSet0;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox FilterSet;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox MesuresCountSet;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox RangeSet1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label Mesure_stat_label;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menustrip_File;
		private System.Windows.Forms.ToolStripMenuItem menustrip_Tools;
		private System.Windows.Forms.ToolStripMenuItem menustrip_COM;
		private System.Windows.Forms.ToolStripMenuItem menustrip_BaudRate;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
