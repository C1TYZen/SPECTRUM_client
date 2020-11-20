namespace graph1
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
			this.Begin_button = new System.Windows.Forms.Button();
			this.Save_button = new System.Windows.Forms.Button();
			this.Close_button = new System.Windows.Forms.Button();
			this.tab_control1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.MPosition_status_label = new System.Windows.Forms.Label();
			this.Text_console = new System.Windows.Forms.TextBox();
			this.resolution_set = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.RangeSet0 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.FilterSet = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.MesuresCountSet = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.RangeSet1 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menustrip_File = new System.Windows.Forms.ToolStripMenuItem();
			this.menustrip_Tools = new System.Windows.Forms.ToolStripMenuItem();
			this.menustrip_COM = new System.Windows.Forms.ToolStripMenuItem();
			this.menustrip_BaudRate = new System.Windows.Forms.ToolStripMenuItem();
			this.New_button = new System.Windows.Forms.Button();
			this.Delete_button = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Goto_button = new System.Windows.Forms.Button();
			this.DividerSet = new System.Windows.Forms.TextBox();
			this.Callibrate_button = new System.Windows.Forms.Button();
			this.Stop_button = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.tab_control1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// Begin_button
			// 
			this.Begin_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Begin_button.Location = new System.Drawing.Point(130, 16);
			this.Begin_button.Name = "Begin_button";
			this.Begin_button.Size = new System.Drawing.Size(75, 23);
			this.Begin_button.TabIndex = 1;
			this.Begin_button.Text = "Начать";
			this.Begin_button.UseVisualStyleBackColor = true;
			this.Begin_button.Click += new System.EventHandler(this.BUTTON_Begin_click);
			// 
			// Save_button
			// 
			this.Save_button.Location = new System.Drawing.Point(87, 19);
			this.Save_button.Name = "Save_button";
			this.Save_button.Size = new System.Drawing.Size(75, 23);
			this.Save_button.TabIndex = 2;
			this.Save_button.Text = "Сохранить";
			this.Save_button.UseVisualStyleBackColor = true;
			this.Save_button.Click += new System.EventHandler(this.BUTTON_Save_click);
			// 
			// Close_button
			// 
			this.Close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Close_button.Location = new System.Drawing.Point(130, 356);
			this.Close_button.Name = "Close_button";
			this.Close_button.Size = new System.Drawing.Size(75, 23);
			this.Close_button.TabIndex = 3;
			this.Close_button.Text = "Закрыть";
			this.Close_button.UseVisualStyleBackColor = true;
			this.Close_button.Click += new System.EventHandler(this.BUTTON_Close_click);
			// 
			// tab_control1
			// 
			this.tab_control1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tab_control1.Controls.Add(this.tabPage1);
			this.tab_control1.Cursor = System.Windows.Forms.Cursors.Cross;
			this.tab_control1.Location = new System.Drawing.Point(3, 3);
			this.tab_control1.Name = "tab_control1";
			this.tab_control1.SelectedIndex = 0;
			this.tab_control1.Size = new System.Drawing.Size(669, 385);
			this.tab_control1.TabIndex = 8;
			this.tab_control1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tab_control1_selecting);
			// 
			// tabPage1
			// 
			this.tabPage1.Cursor = System.Windows.Forms.Cursors.Cross;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(661, 359);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Спектр1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// MPosition_status_label
			// 
			this.MPosition_status_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MPosition_status_label.AutoSize = true;
			this.MPosition_status_label.Location = new System.Drawing.Point(6, 125);
			this.MPosition_status_label.Name = "MPosition_status_label";
			this.MPosition_status_label.Size = new System.Drawing.Size(35, 13);
			this.MPosition_status_label.TabIndex = 0;
			this.MPosition_status_label.Text = "label2";
			// 
			// Text_console
			// 
			this.Text_console.AcceptsReturn = true;
			this.Text_console.AcceptsTab = true;
			this.Text_console.AllowDrop = true;
			this.Text_console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Text_console.BackColor = System.Drawing.Color.Black;
			this.Text_console.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Text_console.ForeColor = System.Drawing.SystemColors.Window;
			this.Text_console.Location = new System.Drawing.Point(678, 394);
			this.Text_console.Multiline = true;
			this.Text_console.Name = "Text_console";
			this.Text_console.ReadOnly = true;
			this.Text_console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Text_console.Size = new System.Drawing.Size(211, 147);
			this.Text_console.TabIndex = 11;
			// 
			// resolution_set
			// 
			this.resolution_set.Location = new System.Drawing.Point(9, 35);
			this.resolution_set.Name = "resolution_set";
			this.resolution_set.Size = new System.Drawing.Size(80, 20);
			this.resolution_set.TabIndex = 13;
			this.resolution_set.TextChanged += new System.EventHandler(this.resolution_set_text_changed);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(70, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Разрешение";
			// 
			// RangeSet0
			// 
			this.RangeSet0.Location = new System.Drawing.Point(7, 32);
			this.RangeSet0.Name = "RangeSet0";
			this.RangeSet0.Size = new System.Drawing.Size(80, 20);
			this.RangeSet0.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "x0";
			// 
			// FilterSet
			// 
			this.FilterSet.Location = new System.Drawing.Point(7, 149);
			this.FilterSet.Name = "FilterSet";
			this.FilterSet.Size = new System.Drawing.Size(80, 20);
			this.FilterSet.TabIndex = 17;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 133);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(87, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Номер фильтра";
			// 
			// MesuresCountSet
			// 
			this.MesuresCountSet.Location = new System.Drawing.Point(7, 110);
			this.MesuresCountSet.Name = "MesuresCountSet";
			this.MesuresCountSet.Size = new System.Drawing.Size(80, 20);
			this.MesuresCountSet.TabIndex = 19;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 94);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Измерение/шаг";
			// 
			// RangeSet1
			// 
			this.RangeSet1.Location = new System.Drawing.Point(7, 71);
			this.RangeSet1.Name = "RangeSet1";
			this.RangeSet1.Size = new System.Drawing.Size(80, 20);
			this.RangeSet1.TabIndex = 21;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(6, 55);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(18, 13);
			this.label8.TabIndex = 20;
			this.label8.Text = "x1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menustrip_File,
            this.menustrip_Tools});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(892, 24);
			this.menuStrip1.TabIndex = 24;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menustrip_File
			// 
			this.menustrip_File.Name = "menustrip_File";
			this.menustrip_File.Size = new System.Drawing.Size(48, 20);
			this.menustrip_File.Text = "Файл";
			// 
			// menustrip_Tools
			// 
			this.menustrip_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menustrip_COM,
            this.menustrip_BaudRate});
			this.menustrip_Tools.Name = "menustrip_Tools";
			this.menustrip_Tools.Size = new System.Drawing.Size(95, 20);
			this.menustrip_Tools.Text = "Инструменты";
			// 
			// menustrip_COM
			// 
			this.menustrip_COM.Name = "menustrip_COM";
			this.menustrip_COM.Size = new System.Drawing.Size(124, 22);
			this.menustrip_COM.Text = "COM";
			// 
			// menustrip_BaudRate
			// 
			this.menustrip_BaudRate.Name = "menustrip_BaudRate";
			this.menustrip_BaudRate.Size = new System.Drawing.Size(124, 22);
			this.menustrip_BaudRate.Text = "BaudRate";
			// 
			// New_button
			// 
			this.New_button.Location = new System.Drawing.Point(6, 19);
			this.New_button.Name = "New_button";
			this.New_button.Size = new System.Drawing.Size(75, 23);
			this.New_button.TabIndex = 26;
			this.New_button.Text = "Новый";
			this.New_button.UseVisualStyleBackColor = true;
			this.New_button.Click += new System.EventHandler(this.BUTTON_New_click);
			// 
			// Delete_button
			// 
			this.Delete_button.Location = new System.Drawing.Point(6, 48);
			this.Delete_button.Name = "Delete_button";
			this.Delete_button.Size = new System.Drawing.Size(75, 23);
			this.Delete_button.TabIndex = 27;
			this.Delete_button.Text = "Удалить";
			this.Delete_button.UseVisualStyleBackColor = true;
			this.Delete_button.Click += new System.EventHandler(this.BUTTON_Delete_click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.Goto_button);
			this.groupBox1.Controls.Add(this.DividerSet);
			this.groupBox1.Controls.Add(this.Callibrate_button);
			this.groupBox1.Controls.Add(this.Stop_button);
			this.groupBox1.Controls.Add(this.Begin_button);
			this.groupBox1.Controls.Add(this.RangeSet1);
			this.groupBox1.Controls.Add(this.Close_button);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.MesuresCountSet);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.FilterSet);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.RangeSet0);
			this.groupBox1.Location = new System.Drawing.Point(678, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 385);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Параметры";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 172);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "Делитель";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 191);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "1/";
			// 
			// Goto_button
			// 
			this.Goto_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Goto_button.Location = new System.Drawing.Point(130, 75);
			this.Goto_button.Name = "Goto_button";
			this.Goto_button.Size = new System.Drawing.Size(75, 23);
			this.Goto_button.TabIndex = 25;
			this.Goto_button.Text = "К началу";
			this.Goto_button.UseVisualStyleBackColor = true;
			this.Goto_button.Click += new System.EventHandler(this.BUTTON_Goto_click);
			// 
			// DividerSet
			// 
			this.DividerSet.Location = new System.Drawing.Point(33, 188);
			this.DividerSet.Name = "DividerSet";
			this.DividerSet.Size = new System.Drawing.Size(20, 20);
			this.DividerSet.TabIndex = 3;
			// 
			// Callibrate_button
			// 
			this.Callibrate_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Callibrate_button.Location = new System.Drawing.Point(7, 356);
			this.Callibrate_button.Name = "Callibrate_button";
			this.Callibrate_button.Size = new System.Drawing.Size(89, 23);
			this.Callibrate_button.TabIndex = 24;
			this.Callibrate_button.Text = "Калибровка";
			this.Callibrate_button.UseVisualStyleBackColor = true;
			this.Callibrate_button.Click += new System.EventHandler(this.BUTTON_Callibrate_click);
			// 
			// Stop_button
			// 
			this.Stop_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Stop_button.Location = new System.Drawing.Point(130, 45);
			this.Stop_button.Name = "Stop_button";
			this.Stop_button.Size = new System.Drawing.Size(75, 23);
			this.Stop_button.TabIndex = 23;
			this.Stop_button.Text = "Стоп";
			this.Stop_button.UseVisualStyleBackColor = true;
			this.Stop_button.Click += new System.EventHandler(this.BUTTON_Stop_click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.68534F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.31466F));
			this.tableLayoutPanel1.Controls.Add(this.tab_control1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.Text_console, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.94861F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.05139F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(892, 544);
			this.tableLayoutPanel1.TabIndex = 28;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.92526F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.07474F));
			this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox3, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 394);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.16666F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(669, 147);
			this.tableLayoutPanel2.TabIndex = 26;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.MPosition_status_label);
			this.groupBox2.Controls.Add(this.New_button);
			this.groupBox2.Controls.Add(this.Save_button);
			this.groupBox2.Controls.Add(this.Delete_button);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(327, 141);
			this.groupBox2.TabIndex = 29;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Спектр";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.AutoSize = true;
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.resolution_set);
			this.groupBox3.Location = new System.Drawing.Point(336, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(330, 141);
			this.groupBox3.TabIndex = 30;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Просмотр";
			// 
			// Graph
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(892, 573);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Graph";
			this.Text = "Graph_v0.6a";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Graph_form_closing);
			this.Load += new System.EventHandler(this.Graph_load);
			this.SizeChanged += new System.EventHandler(this.Graph_size_changed);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DRAW);
			this.tab_control1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Begin_button;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Button Close_button;
		private System.Windows.Forms.TabControl tab_control1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox Text_console;
		private System.Windows.Forms.TextBox resolution_set;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox RangeSet0;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox FilterSet;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox MesuresCountSet;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox RangeSet1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menustrip_File;
		private System.Windows.Forms.ToolStripMenuItem menustrip_Tools;
		private System.Windows.Forms.ToolStripMenuItem menustrip_COM;
		private System.Windows.Forms.ToolStripMenuItem menustrip_BaudRate;
		private System.Windows.Forms.Button New_button;
		private System.Windows.Forms.Button Delete_button;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button Stop_button;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox DividerSet;
		private System.Windows.Forms.Button Callibrate_button;
		private System.Windows.Forms.Button Goto_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label MPosition_status_label;
		private System.Windows.Forms.Label label2;
	}
}

