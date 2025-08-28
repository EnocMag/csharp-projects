using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore_db_ef
{
    public class DetallesPedido
    {
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
    }
}
