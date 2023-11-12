using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clock_down
{
    public partial class Form1 : Form
    {
        Timer timer;        
        bool isStart;        
        int hours;
        int minutes;
        int seconds;
        public Form1()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(time_Down);
            isStart = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!isTimeFormat(textBox_hour.Text, textBox_minute.Text, textBox_second.Text))
            {
                MessageBox.Show("Not time format!");
            }
            else
            {
                updateTime();
                isStart = true;
                timer.Start();
            }            
        }
        private void time_Down(object sender, EventArgs e)
        {
            if (isStart)
            {
                seconds--;
                if (seconds < 0)
                {
                    seconds = 59;
                    minutes--;
                    if (minutes < 0)
                    {
                        minutes = 59;
                        hours--;
                        if (hours < 0)
                        {
                            isStart = false;
                            seconds = 0;
                            minutes = 0;
                            hours = 0;
                            switch (comboBox_action.Text)
                            {
                                case "Shutdown":
                                    Process.Start("shutdown", "/s");
                                    break;
                                case "Restart":
                                    Process.Start("shutdown", "/r");
                                    break;
                                case "Logout":
                                    Process.Start("shutdown", "/l");
                                    break;
                                default:
                                    MessageBox.Show("Oops! Nothing happen");
                                    break;
                            }
                        }
                    }
                }
                updateTime();
            }            
        }
        private bool isTimeFormat(string label_hours, string label_minutes, string label_seconds)
        {
            bool isTempt = true;
            isTempt = int.TryParse(label_hours, out hours);
            if (!isTempt)
                return false;
            isTempt = int.TryParse(label_minutes, out minutes);
            if (!isTempt)
                return false;
            isTempt = int.TryParse(label_seconds, out seconds);
            if (!isTempt)
                return false;
            return true;
        }
        private void updateTime()
        {
            if (hours < 10)
                label_hours.Text = "0" + hours.ToString();
            else
                label_hours.Text = hours.ToString();
            if (minutes < 10)
                label_minutes.Text = "0" + minutes.ToString();
            else
                label_minutes.Text = minutes.ToString();
            if (seconds < 10)
                label_seconds.Text = "0" + seconds.ToString();
            else
                label_seconds.Text = seconds.ToString();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            hours = 0;
            minutes = 0;
            seconds = 0;
            updateTime();
        }
    }
}
