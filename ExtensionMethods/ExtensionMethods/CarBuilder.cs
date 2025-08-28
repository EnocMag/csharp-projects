using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    //Un metodo de extension debe ser un metodo
    //estatico dentro de una clase estatica
    public static class CarBuilder
    {
        // El primer parametro del metodo de extension debe ser eltipo a extender
        // precedido por la palabra this
        public static Car SetBrand(this Car car, string brand) { 
            car.Brand = brand;
            return car;
        }

        public static Car SetColor(this Car car, string color)
        {
            car.Color = color;
            return car;
        }

        public static Car SetYear(this Car car, int year)
        {
            car.Year = year;
            return car;
        }

        public static string GenerateRandomText(this string text, int length = 10)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
