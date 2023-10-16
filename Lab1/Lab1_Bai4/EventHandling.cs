using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Bai4
{
    internal class EventHandling
    {
        public event EventHandler ButtonPressed;

        public void OnButtonPressed(object other)
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key.ToString() == "Spacebar")
                ButtonPressed += OnSpaceDown;
            else
                ButtonPressed = null;
            ButtonPressed?.Invoke(other, new EventArgs());
        }
        private void OnSpaceDown(object sender, EventArgs e)
        {
            ButtonPressed = null;
            Console.Clear();
            Screen screen = new Screen();
            screen.Display(this);
        }
    }
}
