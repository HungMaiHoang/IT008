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
            try
            {
                float res = tu / mau;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            this.tu = tu;
            this.mau = mau;
        }
        public static Fraction operator +(Fraction a, Fraction b)
            => new Fraction(a.Tu* b.Mau + b.Tu* a.Mau, a.Mau* b.Mau);
        public static Fraction operator -(Fraction a, Fraction b)
            => new Fraction(a.Tu * b.Mau - b.Tu * a.Mau, a.Mau * b.Mau);
        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a.Tu * b.Tu, a.Mau * b.Mau);
        public static Fraction operator /(Fraction a, Fraction b)
            => new Fraction(a.Tu * b.Mau, a.Mau * b.Tu);
        public static bool operator ==(Fraction a, Fraction b)
        {
        }
    }
}
