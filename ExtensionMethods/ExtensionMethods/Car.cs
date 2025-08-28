using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public class Car
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }

        public virtual void PrintDetails(string text)
        {
            Console.WriteLine($"Brand: {Brand}, Color: {Color}, Year: {Year}");
        }
    }
}
