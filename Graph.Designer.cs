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
			this.begin_button = new System.Windows.Forms.Button();
			this.save_button = new System.Windows.Forms.Button();
			this.close_button = new System.Windows.Forms.Button();
			this.tab_control1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.MPosition_status_label = new System.Windows.Forms.Label();
			this.Text_console = new System.Windows.Forms.TextBox();
			this.resolution_set = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.mesure_start_set = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.filter_num_set = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.mesure_count_set = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.mesure_end_set = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menustrip_File = new System.Windows.Forms.ToolStripMenuItem();
			this.new_button = new System.Windows.Forms.Button();
			this.delete_button = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.amp_coef_set = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.filter_step_set = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cc_button = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.speed_set = new System.Windows.Forms.TextBox();
			this.callibrate_button = new System.Windows.Forms.Button();
			this.stop_button = new System.Windows.Forms.Button();
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
			// begin_button
			// 
			this.begin_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.begin_button.Location = new System.Drawing.Point(112, 16);
			this.begin_button.Name = "begin_button";
			this.begin_button.Size = new System.Drawing.Size(75, 23);
			this.begin_button.TabIndex = 1;
			this.begin_button.Text = "Начать";
			this.begin_button.UseVisualStyleBackColor = true;
			this.begin_button.Click += new System.EventHandler(this.BUTTON_Begin_click);
			// 
			// save_button
			// 
			this.save_button.Location = new System.Drawing.Point(87, 19);
			this.save_button.Name = "save_button";
			this.save_button.Size = new System.Drawing.Size(75, 23);
			this.save_button.TabIndex = 2;
			this.save_button.Text = "Сохранить";
			this.save_button.UseVisualStyleBackColor = true;
			this.save_button.Click += new System.EventHandler(this.BUTTON_Save_click);
			// 
			// close_button
			// 
			this.close_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.close_button.Location = new System.Drawing.Point(112, 356);
			this.close_button.Name = "close_button";
			this.close_button.Size = new System.Drawing.Size(75, 23);
			this.close_button.TabIndex = 3;
			this.close_button.Text = "Закрыть";
			this.close_button.UseVisualStyleBackColor = true;
			this.close_button.Click += new System.EventHandler(this.BUTTON_Close_click);
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
			this.tab_control1.Size = new System.Drawing.Size(687, 385);
			this.tab_control1.TabIndex = 8;
			this.tab_control1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tab_control1_selecting);
			// 
			// tabPage1
			// 
			this.tabPage1.Cursor = System.Windows.Forms.Cursors.Cross;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(679, 359);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Спектр1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// MPosition_status_label
			// 
			this.MPosition_status_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.MPosition_status_label.AutoSize = true;
			this.MPosition_status_label.Location = new System.Drawing.Point(6, 147);
			this.MPosition_status_label.Name = "MPosition_status_label";
			this.MPosition_status_label.Size = new System.Drawing.Size(0, 13);
			this.MPosition_status_label.TabIndex = 0;
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
			this.Text_console.Location = new System.Drawing.Point(696, 394);
			this.Text_console.Multiline = true;
			this.Text_console.Name = "Text_console";
			this.Text_console.ReadOnly = true;
			this.Text_console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Text_console.Size = new System.Drawing.Size(193, 147);
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
			// mesure_start_set
			// 
			this.mesure_start_set.Location = new System.Drawing.Point(7, 32);
			this.mesure_start_set.Name = "mesure_start_set";
			this.mesure_start_set.Size = new System.Drawing.Size(80, 20);
			this.mesure_start_set.TabIndex = 15;
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
			// filter_num_set
			// 
			this.filter_num_set.Location = new System.Drawing.Point(7, 149);
			this.filter_num_set.Name = "filter_num_set";
			this.filter_num_set.Size = new System.Drawing.Size(80, 20);
			this.filter_num_set.TabIndex = 17;
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
			// mesure_count_set
			// 
			this.mesure_count_set.Location = new System.Drawing.Point(7, 110);
			this.mesure_count_set.Name = "mesure_count_set";
			this.mesure_count_set.Size = new System.Drawing.Size(80, 20);
			this.mesure_count_set.TabIndex = 19;
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
			// mesure_end_set
			// 
			this.mesure_end_set.Location = new System.Drawing.Point(7, 71);
			this.mesure_end_set.Name = "mesure_end_set";
			this.mesure_end_set.Size = new System.Drawing.Size(80, 20);
			this.mesure_end_set.TabIndex = 21;
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
            this.menustrip_File});
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
			// new_button
			// 
			this.new_button.Location = new System.Drawing.Point(6, 19);
			this.new_button.Name = "new_button";
			this.new_button.Size = new System.Drawing.Size(75, 23);
			this.new_button.TabIndex = 26;
			this.new_button.Text = "Новый";
			this.new_button.UseVisualStyleBackColor = true;
			this.new_button.Click += new System.EventHandler(this.BUTTON_New_click);
			// 
			// delete_button
			// 
			this.delete_button.Location = new System.Drawing.Point(6, 48);
			this.delete_button.Name = "delete_button";
			this.delete_button.Size = new System.Drawing.Size(75, 23);
			this.delete_button.TabIndex = 27;
			this.delete_button.Text = "Удалить";
			this.delete_button.UseVisualStyleBackColor = true;
			this.delete_button.Click += new System.EventHandler(this.BUTTON_Delete_click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.AutoSize = true;
			this.groupBox1.Controls.Add(this.amp_coef_set);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.filter_step_set);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cc_button);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.speed_set);
			this.groupBox1.Controls.Add(this.callibrate_button);
			this.groupBox1.Controls.Add(this.stop_button);
			this.groupBox1.Controls.Add(this.begin_button);
			this.groupBox1.Controls.Add(this.mesure_end_set);
			this.groupBox1.Controls.Add(this.close_button);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.mesure_count_set);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.filter_num_set);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.mesure_start_set);
			this.groupBox1.Location = new System.Drawing.Point(696, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(193, 385);
			this.groupBox1.TabIndex = 25;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Параметры";
			// 
			// amp_coef_set
			// 
			this.amp_coef_set.Location = new System.Drawing.Point(9, 266);
			this.amp_coef_set.Name = "amp_coef_set";
			this.amp_coef_set.Size = new System.Drawing.Size(80, 20);
			this.amp_coef_set.TabIndex = 31;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 250);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(57, 13);
			this.label9.TabIndex = 30;
			this.label9.Text = "Усиление";
			// 
			// filter_step_set
			// 
			this.filter_step_set.Location = new System.Drawing.Point(9, 188);
			this.filter_step_set.Name = "filter_step_set";
			this.filter_step_set.Size = new System.Drawing.Size(80, 20);
			this.filter_step_set.TabIndex = 29;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 172);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 13);
			this.label3.TabIndex = 28;
			this.label3.Text = "Шаг выставления";
			// 
			// cc_button
			// 
			this.cc_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cc_button.Location = new System.Drawing.Point(112, 74);
			this.cc_button.Name = "cc_button";
			this.cc_button.Size = new System.Drawing.Size(75, 23);
			this.cc_button.TabIndex = 27;
			this.cc_button.Text = "_проверка";
			this.cc_button.UseVisualStyleBackColor = true;
			this.cc_button.Click += new System.EventHandler(this.BUTTON_check);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 211);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "Скорость";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(67, 230);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "ш/с";
			// 
			// speed_set
			// 
			this.speed_set.Location = new System.Drawing.Point(9, 227);
			this.speed_set.Name = "speed_set";
			this.speed_set.Size = new System.Drawing.Size(52, 20);
			this.speed_set.TabIndex = 3;
			// 
			// callibrate_button
			// 
			this.callibrate_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.callibrate_button.Location = new System.Drawing.Point(112, 103);
			this.callibrate_button.Name = "callibrate_button";
			this.callibrate_button.Size = new System.Drawing.Size(75, 23);
			this.callibrate_button.TabIndex = 24;
			this.callibrate_button.Text = "_на ноль";
			this.callibrate_button.UseVisualStyleBackColor = true;
			this.callibrate_button.Click += new System.EventHandler(this.BUTTON_Callibrate_click);
			// 
			// stop_button
			// 
			this.stop_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.stop_button.Location = new System.Drawing.Point(112, 45);
			this.stop_button.Name = "stop_button";
			this.stop_button.Size = new System.Drawing.Size(75, 23);
			this.stop_button.TabIndex = 23;
			this.stop_button.Text = "Стоп";
			this.stop_button.UseVisualStyleBackColor = true;
			this.stop_button.Click += new System.EventHandler(this.BUTTON_Stop_click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.80269F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.19731F));
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
			this.tableLayoutPanel2.Size = new System.Drawing.Size(687, 147);
			this.tableLayoutPanel2.TabIndex = 26;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.MPosition_status_label);
			this.groupBox2.Controls.Add(this.new_button);
			this.groupBox2.Controls.Add(this.save_button);
			this.groupBox2.Controls.Add(this.delete_button);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(336, 141);
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
			this.groupBox3.Location = new System.Drawing.Point(345, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(339, 141);
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
        private System.Windows.Forms.Button begin_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button close_button;
		private System.Windows.Forms.TabControl tab_control1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox Text_console;
		private System.Windows.Forms.TextBox resolution_set;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox mesure_start_set;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox filter_num_set;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox mesure_count_set;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox mesure_end_set;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menustrip_File;
		private System.Windows.Forms.Button new_button;
		private System.Windows.Forms.Button delete_button;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button stop_button;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox speed_set;
		private System.Windows.Forms.Button callibrate_button;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label MPosition_status_label;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cc_button;
		private System.Windows.Forms.TextBox filter_step_set;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox amp_coef_set;
		private System.Windows.Forms.Label label9;
	}
}

