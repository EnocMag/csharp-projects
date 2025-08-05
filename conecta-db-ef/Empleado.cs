using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaDbEf
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Puesto { get; set; }
        public string Email { get; set; }
    }
}
