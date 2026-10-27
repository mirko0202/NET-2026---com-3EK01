using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<IngresoProducto> IngresosProducto { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options) { }

        internal TPIContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Unidad).HasMaxLength(20);
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.StockMin).IsRequired();
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Categoria).IsRequired().HasConversion<int>();

                entity.Ignore(e => e.BajoStock);

                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.HasData(DatosDePrueba.Productos());
            });

            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.ToTable("Ingredientes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Unidad).HasMaxLength(20);
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.StockMin).IsRequired();

                entity.Ignore(e => e.BajoStock);

                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.HasData(DatosDePrueba.Ingredientes());
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.ToTable("Sucursales");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(150);
                entity.Property(e => e.Telefono).HasMaxLength(30);
                entity.Property(e => e.Activa).IsRequired();

                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.HasData(DatosDePrueba.Sucursales());
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Salt).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Rol).IsRequired().HasConversion<int>();
                entity.Property(e => e.Activo).IsRequired();

                entity.HasOne(e => e.Sucursal)
                    .WithMany()
                    .HasForeignKey(e => e.SucursalId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Ignore(e => e.EsAdministrador);

                entity.HasIndex(e => e.Username).IsUnique();

                entity.HasData(DatosDePrueba.Usuarios());
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FechaHora).IsRequired();

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Sucursal)
                    .WithMany()
                    .HasForeignKey(e => e.SucursalId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Detalles)
                    .WithOne()
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Navigation(e => e.Detalles)
                    .HasField("_detalles")
                    .UsePropertyAccessMode(PropertyAccessMode.Field);

                entity.Ignore(e => e.Total);

                entity.HasIndex(e => e.FechaHora);

                entity.HasData(DatosDePrueba.Ventas());
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.ToTable("DetallesVenta");
                entity.HasKey(e => new { e.VentaId, e.ProductoId });

                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.PrecioUnitario).IsRequired().HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Producto)
                    .WithMany()
                    .HasForeignKey(e => e.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Ignore(e => e.Subtotal);

                entity.HasData(DatosDePrueba.DetallesVenta());
            });

            modelBuilder.Entity<IngresoProducto>(entity =>
            {
                entity.ToTable("IngresosProducto");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FechaHora).IsRequired();
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.Proveedor).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Comprobante).HasMaxLength(50);
                entity.Property(e => e.Observacion).HasMaxLength(255);

                entity.HasOne(e => e.Producto)
                    .WithMany()
                    .HasForeignKey(e => e.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Sucursal)
                    .WithMany()
                    .HasForeignKey(e => e.SucursalId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.FechaHora);

                entity.HasData(DatosDePrueba.IngresosProducto());
            });
        }
    }
}
