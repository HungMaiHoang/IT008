using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Bai2
{
    internal class Fraction
    {
        private int tu;
        private int mau;

        public int Tu { get => tu; set => tu = value; }
        public int Mau { get => mau; set => mau = value; }

        public Fraction() { }
        public Fraction(int tu, int mau)
        {
            this.tu = tu;
            this.mau = mau;
        }
    }
}
