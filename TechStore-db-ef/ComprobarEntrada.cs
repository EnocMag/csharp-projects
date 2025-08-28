using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore_db_ef
{
    public class ComprobarEntrada
    {
        static public int Comprobar(string Entrada) 
        {
            if (Entrada.ToLower() == "x")
            {
                Console.WriteLine("Saliendo del programa..");
                Environment.Exit(0);
            }
            else
            {
                return int.TryParse(Entrada, out int numero) ? numero : 0;
            }
            return 0;
        }
    }
}
