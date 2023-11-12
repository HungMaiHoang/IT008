namespace clock_down
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_hour = new System.Windows.Forms.TextBox();
            this.textBox_minute = new System.Windows.Forms.TextBox();
            this.textBox_second = new System.Windows.Forms.TextBox();
            this.comboBox_action = new System.Windows.Forms.ComboBox();
            this.button_start = new System.Windows.Forms.Button();
            this.label_hours = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_minutes = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_seconds = new System.Windows.Forms.Label();
            this.button_stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minutes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(508, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Seconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Actions";
            // 
            // textBox_hour
            // 
            this.textBox_hour.Location = new System.Drawing.Point(115, 35);
            this.textBox_hour.Name = "textBox_hour";
            this.textBox_hour.Size = new System.Drawing.Size(93, 26);
            this.textBox_hour.TabIndex = 4;
            // 
            // textBox_minute
            // 
            this.textBox_minute.Location = new System.Drawing.Point(355, 36);
            this.textBox_minute.Name = "textBox_minute";
            this.textBox_minute.Size = new System.Drawing.Size(96, 26);
            this.textBox_minute.TabIndex = 5;
            // 
            // textBox_second
            // 
            this.textBox_second.Location = new System.Drawing.Point(586, 36);
            this.textBox_second.Name = "textBox_second";
            this.textBox_second.Size = new System.Drawing.Size(100, 26);
            this.textBox_second.TabIndex = 6;
            // 
            // comboBox_action
            // 
            this.comboBox_action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_action.FormattingEnabled = true;
            this.comboBox_action.Items.AddRange(new object[] {
            "Shutdown",
            "Logout",
            "Restart"});
            this.comboBox_action.Location = new System.Drawing.Point(162, 138);
            this.comboBox_action.Name = "comboBox_action";
            this.comboBox_action.Size = new System.Drawing.Size(113, 28);
            this.comboBox_action.TabIndex = 7;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(555, 101);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(157, 65);
            this.button_start.TabIndex = 8;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_hours
            // 
            this.label_hours.AutoSize = true;
            this.label_hours.Location = new System.Drawing.Point(336, 324);
            this.label_hours.Name = "label_hours";
            this.label_hours.Size = new System.Drawing.Size(27, 20);
            this.label_hours.TabIndex = 9;
            this.label_hours.Text = "00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 324);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = ":";
            // 
            // label_minutes
            // 
            this.label_minutes.AutoSize = true;
            this.label_minutes.Location = new System.Drawing.Point(389, 324);
            this.label_minutes.Name = "label_minutes";
            this.label_minutes.Size = new System.Drawing.Size(27, 20);
            this.label_minutes.TabIndex = 11;
            this.label_minutes.Text = "00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(422, 324);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = ":";
            // 
            // label_seconds
            // 
            this.label_seconds.AutoSize = true;
            this.label_seconds.Location = new System.Drawing.Point(441, 324);
            this.label_seconds.Name = "label_seconds";
            this.label_seconds.Size = new System.Drawing.Size(27, 20);
            this.label_seconds.TabIndex = 13;
            this.label_seconds.Text = "00";
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(555, 199);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(157, 65);
            this.button_stop.TabIndex = 14;
            this.button_stop.Text = "Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.label_seconds);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label_minutes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_hours);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.comboBox_action);
            this.Controls.Add(this.textBox_second);
            this.Controls.Add(this.textBox_minute);
            this.Controls.Add(this.textBox_hour);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_hour;
        private System.Windows.Forms.TextBox textBox_minute;
        private System.Windows.Forms.TextBox textBox_second;
        private System.Windows.Forms.ComboBox comboBox_action;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label_hours;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_minutes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_seconds;
        private System.Windows.Forms.Button button_stop;
    }
}

