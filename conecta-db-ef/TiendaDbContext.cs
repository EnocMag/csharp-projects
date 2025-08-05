using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConectaDbEf
{
    public class TiendaDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=tienda_ventas;Uid=root;Pwd=enoc9810;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
