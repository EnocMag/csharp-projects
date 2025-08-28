using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectaDbEf;

namespace TechStore_db_ef
{
    public class Pedido
    {
        [Key]
        [Column("pedido_id")]
        public int PedidoId { get; set; }
        [Column("cliente_id")]
        [ForeignKey("cliente_id")]
        public int ClienteId { get; set; }
        [Column("fecha_pedido")]
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        public float Subtotal { get; set; }
        public float Impuestos { get; set; }
        public float Descuento { get; set; }
        public float Total { get; set; }
        public string? Notas { get; set; }
        public Cliente Cliente { get; set; }
        public List<DetallesPedido> DetallesPedidos { get; set; }
        public List<Producto> Productos { get; set; }
        public override string ToString()
        {
            return $"Pedido Id: {PedidoId}, Fecha: {FechaPedido}, Total: {Total:C}, Estado: {Estado}";
        }
    }
}
