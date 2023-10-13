using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Bai4
{
    internal class Temperature
    {
        private float degrees;

        public float Degrees { get => degrees; set => degrees = value; }
        public Temperature() 
        {
            Random random = new Random();
            degrees = random.Next(0, 101);
        }
        
    }
}
