using System;

namespace Lab1_Bai2;
class Program
{
    static void Main(string[] args)
    {

        List<Fraction> list = new List<Fraction>();
        list.Add(new Fraction(1, 2));
        list.Add(new Fraction(1, 5));
        list.Add(new Fraction(1, 4));
        list.Add(new Fraction(1, 7));
        list.Add(new Fraction(1, 10));
        list.Add(new Fraction(1, 9));
        list.Add(new Fraction(1, 8));

        list.Sort();

        foreach (Fraction fraction in list)
        {
            Console.WriteLine(fraction.Tu + "/" + fraction.Mau);
        }
    }
}
