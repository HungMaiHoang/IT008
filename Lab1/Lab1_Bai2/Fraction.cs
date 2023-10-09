using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Bai2
{
    internal class Fraction : IComparable<Fraction>
    {
        private float tu;
        private float mau;

        public float Tu { get => tu; set => tu = value; }
        public float Mau { get => mau; set => mau = value; }

        public Fraction() { }
        public Fraction(float tu, float mau)
        {
            if (mau == 0)
                throw new ArgumentException("Divide by Zero?");
            else
            {
                this.tu = tu;
                this.mau = mau;
            }
        }
        public Fraction (float other)
        {
            this.tu = other;
            this.mau = 1;
        }
        public static implicit operator Fraction (int other)
            => new Fraction(other,1);
        public static explicit operator float(Fraction other)
        {
            return other.Tu / other.Mau;
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
            if ((float)a - (float)b == 0)
                return true;
            return false;
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);           
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Fraction))
                return false;
            return this == (Fraction)obj;
        }
        public static bool operator >(Fraction a, Fraction b)
        {
            if ((float)a - (float)b > 0)
                return true;
            return false;
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            if ((float)a - (float)b < 0)
                return true;
            return false;
        }
        //public int CompareTo(object other)
        //{
        //    if (other is null) return 0;
        //    float f1 = (float)this;
        //    float f2 = (float)other;
        //    return f1.CompareTo(f2);
        //}
        public int CompareTo(Fraction other)
        {
            if (other is null) return 0;
            float f1 = (float)this;
            float f2 = (float)other;
            return f1.CompareTo(f2);
        }
    }
}
