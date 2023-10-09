using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Bai4
{
    internal class Screen
    {
        public void Display(EventHandling eventHandling)
        {
            Temperature temperature = new Temperature();
            Console.WriteLine("Temperature: " + temperature.Degrees + " C");
            Console.WriteLine("Press Space to change, other key to exit");
            eventHandling.OnButtonPressed(this);
        }        
    }
}
