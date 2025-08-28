using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TechStore_db_ef;


namespace ConectaDbEf
{
    public class TechStoreDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetallesPedido> DetallesPedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=TechStore;Uid=root;Pwd=enoc9810;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("clientes")
                .HasKey(c => c.ClienteId)
                .HasName("clientes_id");
            modelBuilder.Entity<Cliente>().Property(c => c.ClienteId).HasColumnName("cliente_id");
            modelBuilder.Entity<Cliente>().Property(c => c.FechaRegistro).HasColumnName("fecha_registro");

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Productos)
                .WithMany(dp => dp.Pedidos)
                .UsingEntity<DetallesPedido>(
                    j => j
                        .HasOne(dp => dp.Producto)
                        .WithMany(p => p.DetallesPedidos)
                        .HasForeignKey(dp => dp.ProductoId),
                    j => j
                        .HasOne(dp => dp.Pedido)
                        .WithMany(p => p.DetallesPedidos)
                        .HasForeignKey(dp => dp.PedidoId),
                    j =>
                    {
                        j.ToTable("detalles_pedido");
                        j.HasKey(dp => new { dp.PedidoId, dp.ProductoId });
                        j.Property(dp => dp.PedidoId).HasColumnName("pedido_id");
                        j.Property(dp => dp.ProductoId).HasColumnName("producto_id");
                    });

            base.OnModelCreating(modelBuilder);

        }
    }
}