using Lab1_Bai4;
using System;

namespace Lab1_Bai2;
class Program
{   
    static void Main(string[] args)
    {
        EventHandling eventHandling = new EventHandling();
        Screen screen = new Screen();
        screen.Display(eventHandling);
        //ConsoleKeyInfo key = Console.ReadKey();
        //Console.WriteLine(key.Key.ToString());
    }
}
