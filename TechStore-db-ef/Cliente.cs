using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore_db_ef;

namespace ConectaDbEf
{
	[Table("clientes")]
    public class Cliente
	{
		[Key]
		[Column("cliente_id")]
        public int ClienteId { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Email { get; set; }
		public string? Telefono { get; set; }
		[Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }
		public bool Activo { get; set; }
        public List<Pedido> Pedidos { get; set; }

		public override string ToString()
		{
			return $"Id: {ClienteId}, Nombre: {Nombre} {Apellido}, Email: {Email}";
        }
    }
}