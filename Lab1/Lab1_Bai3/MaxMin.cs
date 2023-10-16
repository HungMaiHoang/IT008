using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Bai3
{
   internal class MaxMin
    {
        public void Max(string input)
        {
            string[] ql = input.Split(" ");
            bool i = int.TryParse(ql[0], out int c);
            if (i)
            {
                int[] nguyen = new int[ql.Length];
                for (int m = 0; m < ql.Length; m++)
                {
                    try
                    {
                        nguyen[m] = int.Parse(ql[m]);
                    }
                    catch (Exception)
                    {
                        goto db;
                    }
                }
                int maxnguyen = nguyen[0];
                foreach (int m in nguyen)
                {
                    if (m > maxnguyen)
                        maxnguyen = m;
                }
                Console.WriteLine("so nguyen lon nhat: {0}", maxnguyen);
                goto e;
            }
        db:;
            bool db = double.TryParse(ql[0], out double d);
            if (db)
            {
                double[] thuc = new double[ql.Length];
                for (int m = 0; m < ql.Length; m++)
                {
                    try
                    {
                        thuc[m] = double.Parse(ql[m]);
                    }
                    catch (Exception)
                    {
                        goto str;
                    }
                }
                double maxthuc = thuc[0];
                foreach (double m in thuc)
                {
                    if (m > maxthuc)
                        maxthuc = m;
                }
                Console.WriteLine("so thuc lon nhat: {0}", maxthuc);
                goto e;
            }
        str:;
            string chuoidainhat = ql[0];
            int maxchuoi = ql[0].Length;
            for (int m = 0; m < ql.Length; m++)
            {
                if (ql[m].Length > maxchuoi)
                {
                    maxchuoi = ql[m].Length;
                    chuoidainhat = ql[m];
                }
            }
            Console.WriteLine("Chuoi dai nhat: {0} co do dai {1}", chuoidainhat, maxchuoi);
        e:;
        }
        public void Min(string input)
        {
            string[] ql = input.Split(" ");
            bool i = int.TryParse(ql[0], out int c);
            if (i)
            {
                int[] nguyen = new int[ql.Length];
                for (int m = 0; m < ql.Length; m++)
                {
                    try
                    {
                        nguyen[m] = int.Parse(ql[m]);
                    }
                    catch (Exception)
                    {
                        goto db;
                    }
                }
                int minnguyen = nguyen[0];
                foreach (int m in nguyen)
                {
                    if (m < minnguyen)
                        minnguyen = m;
                }
                Console.WriteLine("so nguyen nho nhat: {0}", minnguyen);
                goto e;
            }
        db:;
            bool db = double.TryParse(ql[0], out double d);
            if (db)
            {
                double[] thuc = new double[ql.Length];
                for (int m = 0; m < ql.Length; m++)
                {
                    try
                    {
                        thuc[m] = double.Parse(ql[m]);
                    }
                    catch (Exception)
                    {
                        goto str;
                    }
                }
                double minthuc = thuc[0];
                foreach (double m in thuc)
                {
                    if (m < minthuc)
                        minthuc = m;
                }
                Console.WriteLine("so thuc nho nhat: {0}", minthuc);
                goto e;
            }
        str:;
            string chuoingannhat = ql[0];
            int minchuoi = ql[0].Length;
            for (int m = 0; m < ql.Length; m++)
            {
                if (ql[m].Length < minchuoi)
                {
                    minchuoi = ql[m].Length;
                    chuoingannhat = ql[m];
                }
            }
            Console.WriteLine("Chuoi ngan nhat: {0} co do dai {1}", chuoingannhat, minchuoi);
        e:;
        }
    }
}
