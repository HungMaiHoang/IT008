using System;
using System.IO;
using System.Text;

namespace Lab1_Bai5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Nhập đường dẫn thư mục: ");
            string folderpath = Console.ReadLine();
            if (Directory.Exists(folderpath))
            {
                Console.WriteLine("Các tệp trong thư mục '{0}': ", folderpath);
                string[] files = Directory.GetFiles(folderpath);
                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        Console.WriteLine(Path.GetFileName(file));
                    }
                }
                else { Console.WriteLine("Thư mục trống"); }
            }
            else Console.WriteLine("Đường dẫn thư mục không tồn tại");
        }
    }
}