using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Bai4
{
    public partial class FormPaint : Form
    {
        public FormPaint()
        {
            InitializeComponent();
        }
        private void EventPaint (object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random random = new Random();
            int x = random.Next(0,Width);
            int y = random.Next(0,Height);
            g.DrawString("Paint Event ", new Font("Arial", 12), Brushes.Black, x, y);
        }
     
    }
}
