using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class ESantiagoPreExamenBBVAContext : DbContext
    {
        public ESantiagoPreExamenBBVAContext()
        {
        }

        public ESantiagoPreExamenBBVAContext(DbContextOptions<ESantiagoPreExamenBBVAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cambio> Cambios { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database= ESantiagoPreExamenBBVA; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cambio>(entity =>
            {
                entity.HasKey(e => e.IdMaquina)
                    .HasName("PK__Cambio__08E38C8347B2C7A0");

                entity.ToTable("Cambio");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compra__0A5CDB5C3F7D84C4");

                entity.ToTable("Compra");

                entity.Property(e => e.Cambio).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Ingreso).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(7, 2)")
                    .HasComputedColumnSql("([Ingreso]-[Cambio])", false);

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compra__IdProduc__145C0A3F");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__09889210DEDDE79B");

                entity.ToTable("Producto");

                entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
