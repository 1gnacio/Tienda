using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Tienda.Models
{
    public partial class TiendaCTX : DbContext
    {
        public TiendaCTX()
        {
        }

        public TiendaCTX(DbContextOptions<TiendaCTX> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=.;database=Tienda;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__A3C02A105BCE998A");

                entity.Property(e => e.IdCategoria).ValueGeneratedNever();
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__D594664206D10636");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__0A5CDB5CD78BD792");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compras__IdProve__4AB81AF0");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCompra)
                    .HasName("PK__DetalleC__E046CCBB82B30852");

                entity.Property(e => e.Total).HasComputedColumnSql("([Precio]*[Cantidad])", false);

                entity.HasOne(d => d.Compra)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__DetalleCo__IdCom__45F365D3");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleCo__IdPro__46E78A0C");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVenta)
                    .HasName("PK__DetalleV__AAA5CEC2B013B96A");

                entity.Property(e => e.Total).HasComputedColumnSql("([Precio]*[Cantidad])", false);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DetalleVe__IdPro__48CFD27E");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__IdVen__47DBAE45");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__098892100C593BC5");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Productos__IdCat__44FF419A");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__E8B631AF6B39022F");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Ventas__BC1240BD0D417E78");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ventas__IdClient__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
