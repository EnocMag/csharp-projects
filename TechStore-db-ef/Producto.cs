using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore_db_ef
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        [Column("categoria_id")]
        [ForeignKey("categoria_id")]
        public int CategoriaId { get; set; }
        public string Sku { get; set; }
        [Column("stock_disponible")]
        public int StockDisponible { get; set; }
        [Column("stock_minimo")]
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<DetallesPedido> DetallesPedidos { get; set; }

        public override string ToString()
        {
            return $"Id: {ProductoId}, Nombre: {Nombre, -25}|| Precio: {Precio:C}|| Stock: {StockDisponible,-15}";
        }
    }
}