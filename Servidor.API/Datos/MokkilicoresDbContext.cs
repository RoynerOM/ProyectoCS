using Microsoft.EntityFrameworkCore;
using Proyecto2.API.Models;

namespace Proyecto2.API.Datos;

public partial class MokkilicoresDbContext : DbContext
{
    public MokkilicoresDbContext()
    {
    }

    public MokkilicoresDbContext(DbContextOptions<MokkilicoresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC072DAE445C");

            entity.ToTable("Cliente");

            entity.Property(e => e.CantidadDineroTotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CantidadDineroUltimoAno).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CantidadDineroUltimos6Meses).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Canton)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Distrito)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Identificacion)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto).IsUnicode(false);
            entity.Property(e => e.Provincia)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasMany(c => c.Pedidos)
            .WithOne(p => p.Cliente) 
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.Sesions)
            .WithOne(s => s.Cliente) 
            .HasForeignKey(s => s.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventar__3214EC07036095FD");

            entity.ToTable("Inventario");

            entity.Property(e => e.BodegaId).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.Nombre).IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Stock).HasDefaultValueSql("((0))");
            entity.Property(e => e.TipoLicor)
                .HasConversion(tipo => tipo.ToString(), tipo => (TipoLicor)Enum.Parse(typeof(TipoLicor), tipo))
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedido__3214EC073A23A5F8");

            entity.ToTable("Pedido");

            entity.Property(e => e.Cantidad).HasDefaultValueSql("((1))");
            entity.Property(e => e.CostoSinIva)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("CostoSinIVA");
            entity.Property(e => e.CostoTotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Estado).HasConversion(e => e.ToString(), e => (EstadoPedido)Enum.Parse(typeof(EstadoPedido), e))
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__ClienteI__4222D4EF");

            entity.HasOne(d => d.Producto).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido__Producto__4316F928");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sesion__3214EC071FAEF4E1");

            entity.ToTable("Sesion");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesion__ClienteI__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
