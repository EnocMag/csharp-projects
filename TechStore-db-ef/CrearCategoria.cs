using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectaDbEf;

namespace TechStore_db_ef
{
    public class CrearCategoria
    {
        static public Categoria Crear(TechStoreDbContext context)
        {
            Console.WriteLine("Favor de introducir el nombre de la nueva categoria");
            var nombreCategoria = Console.ReadLine();
            // Comprobar si la categoria ya existe
            var categoriasExistente = context.Categorias
                .Where(x => x.Nombre.Contains(nombreCategoria))
                    .ToList();
            if (categoriasExistente.Any())
            {
                foreach (var categoria in categoriasExistente)
                {
                    Console.WriteLine($"La categoria '{nombreCategoria}' ya existe con el ID: {categoria.CategoriaId}");
                    Environment.Exit(0);
                }
            }
            
            Console.WriteLine("Favor de introducir la descripcion de la nueva categoria");
            var descripcionCategoria = Console.ReadLine();
            Console.WriteLine("Favor de introducir el id de la categoria padre (opcional, presione Enter si no hay)");
            var categoriaPadreIdInput = Console.ReadLine();
            int? categoriaPadreId = null;
            if (!string.IsNullOrEmpty(categoriaPadreIdInput) && int.TryParse(categoriaPadreIdInput, out int padreId))
            {
                categoriaPadreId = padreId;
            }
            var nuevaCategoria = new Categoria
            {
                Nombre = nombreCategoria,
                Descripcion = descripcionCategoria,
                CategoriaPadreId = categoriaPadreId,
                Activa = true
            };
            return nuevaCategoria;
            
        }
    }
}
