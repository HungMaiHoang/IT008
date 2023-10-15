
using Lab1_Bai3;
using System;
using System.Collections.Specialized;
using System.Reflection.Emit;

namespace Bai3
{
    class Program
    {

        static void Main(String[] args)
        {
            Console.WriteLine("Nhap day");
            string s = Console.ReadLine();
            MaxMin a = new MaxMin();
            a.Max(s);
            a.Min(s);
        }
       
    }
}