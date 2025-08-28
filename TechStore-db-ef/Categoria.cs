using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore_db_ef
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Column("categoria_padre_id")]
        [ForeignKey("categoria_padre_id")]
        public int? CategoriaPadreId { get; set; }
        public bool Activa { get; set; }

        public List<Producto> Productos { get; set; }
        public Categoria CategoriaPadre { get; set; }
        public List<Categoria> Subcategorias { get; set; }

        public override string ToString()
        {
            return $"Id: {CategoriaId}, Nombre: {Nombre, -25}|| Descripcion: {Descripcion, -50}|| Activa: {Activa}";
        }
    }
}