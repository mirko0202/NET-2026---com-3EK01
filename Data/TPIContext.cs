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

                entity.Ignore(e => e.BajoStock);

                entity.HasIndex(e => e.Nombre).IsUnique();

                entity.HasData(
                    new { Id = 1, Nombre = "Medialuna", Descripcion = (string?)"Medialuna de manteca", Unidad = (string?)"unidad", Stock = 120, StockMin = 50 },
                    new { Id = 2, Nombre = "Pan francés", Descripcion = (string?)"Pan tipo baguette", Unidad = (string?)"kg", Stock = 30, StockMin = 10 },
                    new { Id = 3, Nombre = "Torta selva negra", Descripcion = (string?)"Porción individual", Unidad = (string?)"porción", Stock = 8, StockMin = 10 },
                    new { Id = 4, Nombre = "Café", Descripcion = (string?)"Café en grano premium", Unidad = (string?)"kg", Stock = 5, StockMin = 8 },
                    new { Id = 5, Nombre = "Factura de dulce de leche", Descripcion = (string?)"Factura rellena", Unidad = (string?)"unidad", Stock = 90, StockMin = 40 },
                    new { Id = 6, Nombre = "Budín de limón", Descripcion = (string?)"Budín casero con glaseado", Unidad = (string?)"unidad", Stock = 12, StockMin = 15 },
                    new { Id = 7, Nombre = "Alfajor de maicena", Descripcion = (string?)"Alfajor artesanal", Unidad = (string?)"unidad", Stock = 60, StockMin = 25 },
                    new { Id = 8, Nombre = "Sándwich de miga", Descripcion = (string?)"Docena surtida", Unidad = (string?)"docena", Stock = 4, StockMin = 6 }
                );
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

                entity.HasData(
                    new { Id = 1, Nombre = "Harina 000", Descripcion = (string?)"Harina de trigo", Unidad = (string?)"kg", Stock = 200, StockMin = 50 },
                    new { Id = 2, Nombre = "Manteca", Descripcion = (string?)"Manteca sin sal", Unidad = (string?)"kg", Stock = 40, StockMin = 20 },
                    new { Id = 3, Nombre = "Azúcar", Descripcion = (string?)"Azúcar refinada", Unidad = (string?)"kg", Stock = 15, StockMin = 30 },
                    new { Id = 4, Nombre = "Huevos", Descripcion = (string?)"Huevos de gallina", Unidad = (string?)"docena", Stock = 25, StockMin = 10 },
                    new { Id = 5, Nombre = "Levadura fresca", Descripcion = (string?)"Levadura prensada", Unidad = (string?)"kg", Stock = 6, StockMin = 8 },
                    new { Id = 6, Nombre = "Dulce de leche", Descripcion = (string?)"Dulce de leche repostero", Unidad = (string?)"kg", Stock = 35, StockMin = 15 },
                    new { Id = 7, Nombre = "Chocolate cobertura", Descripcion = (string?)"Chocolate semiamargo", Unidad = (string?)"kg", Stock = 9, StockMin = 12 },
                    new { Id = 8, Nombre = "Leche entera", Descripcion = (string?)"Leche de vaca", Unidad = (string?)"litro", Stock = 80, StockMin = 30 }
                );
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

                entity.Ignore(e => e.EsAdministrador);

                entity.HasIndex(e => e.Username).IsUnique();

                var admin = new Usuario(1, "admin", "Mirko Felicevich", "admin123", RolUsuario.Administrador);
                var empleado = new Usuario(2, "empleado", "Lautaro Coria", "empleado123", RolUsuario.Empleado);

                entity.HasData(
                    new { admin.Id, admin.Username, admin.NombreCompleto, admin.PasswordHash, admin.Salt, admin.Rol, admin.Activo },
                    new { empleado.Id, empleado.Username, empleado.NombreCompleto, empleado.PasswordHash, empleado.Salt, empleado.Rol, empleado.Activo }
                );
            });
        }
    }
}
