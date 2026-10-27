using Domain.Model;

namespace Data
{
    public static class DatosDePrueba
    {
        private record ProductoSemilla(int Id, string Nombre, string Descripcion, string Unidad, int Stock,
                                       int StockMin, decimal Precio, CategoriaProducto Categoria,
                                       int Peso, int CantidadMin, int CantidadMax);

        private static readonly ProductoSemilla[] productos =
        {
            new(1, "Medialuna", "Medialuna de manteca", "unidad", 120, 50, 900m, CategoriaProducto.Facturas, 9, 6, 24),
            new(2, "Pan francés", "Pan tipo baguette", "kg", 30, 10, 3500m, CategoriaProducto.Panificados, 7, 1, 4),
            new(3, "Torta selva negra", "Porción individual", "porción", 8, 10, 6500m, CategoriaProducto.Pasteleria, 3, 1, 3),
            new(4, "Café", "Café en grano premium", "kg", 5, 8, 28000m, CategoriaProducto.Cafeteria, 2, 1, 1),
            new(5, "Factura de dulce de leche", "Factura rellena", "unidad", 90, 40, 950m, CategoriaProducto.Facturas, 8, 6, 18),
            new(6, "Budín de limón", "Budín casero con glaseado", "unidad", 12, 15, 7500m, CategoriaProducto.Pasteleria, 3, 1, 2),
            new(7, "Alfajor de maicena", "Alfajor artesanal", "unidad", 60, 25, 1800m, CategoriaProducto.Pasteleria, 6, 2, 12),
            new(8, "Sándwich de miga", "Docena surtida", "docena", 4, 6, 15000m, CategoriaProducto.Salados, 3, 1, 2),
            new(9, "Pan de campo", "Hogaza de masa madre", "unidad", 18, 8, 4200m, CategoriaProducto.Panificados, 4, 1, 3),
            new(10, "Chipá", "Chipá de queso", "docena", 14, 10, 6000m, CategoriaProducto.Salados, 4, 1, 2),
            new(11, "Tarta de frutillas", "Tarta entera de 8 porciones", "unidad", 3, 4, 24000m, CategoriaProducto.Pasteleria, 1, 1, 1),
            new(12, "Jugo de naranja", "Jugo exprimido de 500 ml", "unidad", 25, 12, 2500m, CategoriaProducto.Bebidas, 5, 1, 4)
        };

        public static object[] Productos() => productos
            .Select(p => (object)new
            {
                p.Id,
                p.Nombre,
                Descripcion = (string?)p.Descripcion,
                Unidad = (string?)p.Unidad,
                p.Stock,
                p.StockMin,
                p.Precio,
                p.Categoria
            })
            .ToArray();

        public static object[] Ingredientes() => new object[]
        {
            new { Id = 1, Nombre = "Harina 000", Descripcion = (string?)"Harina de trigo", Unidad = (string?)"kg", Stock = 200, StockMin = 50 },
            new { Id = 2, Nombre = "Manteca", Descripcion = (string?)"Manteca sin sal", Unidad = (string?)"kg", Stock = 40, StockMin = 20 },
            new { Id = 3, Nombre = "Azúcar", Descripcion = (string?)"Azúcar refinada", Unidad = (string?)"kg", Stock = 15, StockMin = 30 },
            new { Id = 4, Nombre = "Huevos", Descripcion = (string?)"Huevos de gallina", Unidad = (string?)"docena", Stock = 25, StockMin = 10 },
            new { Id = 5, Nombre = "Levadura fresca", Descripcion = (string?)"Levadura prensada", Unidad = (string?)"kg", Stock = 6, StockMin = 8 },
            new { Id = 6, Nombre = "Dulce de leche", Descripcion = (string?)"Dulce de leche repostero", Unidad = (string?)"kg", Stock = 35, StockMin = 15 },
            new { Id = 7, Nombre = "Chocolate cobertura", Descripcion = (string?)"Chocolate semiamargo", Unidad = (string?)"kg", Stock = 9, StockMin = 12 },
            new { Id = 8, Nombre = "Leche entera", Descripcion = (string?)"Leche de vaca", Unidad = (string?)"litro", Stock = 80, StockMin = 30 }
        };

        public static object[] Sucursales() => new object[]
        {
            new { Id = 1, Nombre = "Casa Central", Direccion = (string?)"Av. San Martín 1452", Telefono = (string?)"0341 425-1100", Activa = true },
            new { Id = 2, Nombre = "Sucursal Centro", Direccion = (string?)"Belgrano 860", Telefono = (string?)"0341 437-2290", Activa = true },
            new { Id = 3, Nombre = "Sucursal Norte", Direccion = (string?)"Av. Libertad 2315", Telefono = (string?)"0341 451-3375", Activa = true }
        };

        public static object[] Usuarios() => new[]
        {
            new Usuario(1, "admin", "Mirko Felicevich", "admin123", RolUsuario.Administrador),
            new Usuario(2, "empleado", "Lautaro Coria", "empleado123", RolUsuario.Empleado, true, 1),
            new Usuario(3, "lgomez", "Lucía Gómez", "lucia123", RolUsuario.Empleado, true, 2),
            new Usuario(4, "mperez", "Martín Pérez", "martin123", RolUsuario.Empleado, true, 3),
            new Usuario(5, "vrios", "Valeria Ríos", "valeria123", RolUsuario.Administrador, true, 1),
            new Usuario(6, "jsosa", "Julián Sosa", "julian123", RolUsuario.Empleado, false, 3)
        }
        .Select(u => (object)new
        {
            u.Id,
            u.Username,
            u.NombreCompleto,
            u.PasswordHash,
            u.Salt,
            u.Rol,
            u.Activo,
            u.SucursalId
        })
        .ToArray();

        public static object[] IngresosProducto() => new object[]
        {
            Ingreso(1, 2026, 8, 3, 7, 30, 1, 1, 1, 300, "Producción propia", "RI-0001-00000101", null),
            Ingreso(2, 2026, 8, 3, 7, 45, 5, 1, 1, 240, "Producción propia", "RI-0001-00000102", null),
            Ingreso(3, 2026, 8, 5, 9, 0, 4, 1, 1, 10, "Tostadero Don Pedro", "FC-A-0003-00004521", "Grano de tueste medio"),
            Ingreso(4, 2026, 8, 10, 8, 15, 2, 2, 5, 40, "Panificadora del Litoral", "FC-B-0002-00011873", null),
            Ingreso(5, 2026, 8, 12, 10, 30, 7, 3, 5, 120, "Alfajores Sol de Mayo", "FC-A-0001-00007812", null),
            Ingreso(6, 2026, 8, 18, 7, 30, 8, 2, 1, 20, "Producción propia", "RI-0002-00000045", null),
            Ingreso(7, 2026, 8, 24, 11, 0, 12, 1, 5, 60, "Distribuidora La Esquina", "FC-A-0005-00023310", null),
            Ingreso(8, 2026, 9, 1, 7, 30, 1, 2, 1, 280, "Producción propia", "RI-0002-00000058", null),
            Ingreso(9, 2026, 9, 2, 9, 20, 6, 1, 5, 30, "Producción propia", "RI-0001-00000134", null),
            Ingreso(10, 2026, 9, 7, 8, 0, 3, 1, 1, 24, "Producción propia", "RI-0001-00000140", "Porciones individuales"),
            Ingreso(11, 2026, 9, 15, 10, 10, 10, 3, 5, 30, "Panificadora del Litoral", "FC-B-0002-00012440", null),
            Ingreso(12, 2026, 9, 20, 9, 30, 4, 2, 1, 8, "Tostadero Don Pedro", "FC-A-0003-00004788", null),
            Ingreso(13, 2026, 9, 28, 7, 30, 9, 3, 5, 36, "Producción propia", "RI-0003-00000021", null),
            Ingreso(14, 2026, 10, 2, 8, 40, 11, 1, 1, 10, "Producción propia", "RI-0001-00000171", null),
            Ingreso(15, 2026, 10, 6, 7, 30, 5, 3, 5, 200, "Producción propia", "RI-0003-00000029", null),
            Ingreso(16, 2026, 10, 12, 11, 15, 12, 2, 1, 48, "Distribuidora La Esquina", "FC-A-0005-00024102", null),
            Ingreso(17, 2026, 10, 19, 9, 0, 7, 1, 5, 96, "Alfajores Sol de Mayo", "FC-A-0001-00008190", "Caja de 24 unidades"),
            Ingreso(18, 2026, 10, 23, 7, 30, 1, 3, 1, 260, "Producción propia", "RI-0003-00000036", null)
        };

        private static object Ingreso(int id, int anio, int mes, int dia, int hora, int minuto, int productoId,
                                      int sucursalId, int usuarioId, int cantidad, string proveedor,
                                      string? comprobante, string? observacion) => new
        {
            Id = id,
            FechaHora = new DateTime(anio, mes, dia, hora, minuto, 0),
            ProductoId = productoId,
            SucursalId = sucursalId,
            UsuarioId = usuarioId,
            Cantidad = cantidad,
            Proveedor = proveedor,
            Comprobante = comprobante,
            Observacion = observacion
        };

        private static readonly Lazy<(List<object> Ventas, List<object> Detalles)> ventas = new(GenerarVentas);

        public static object[] Ventas() => ventas.Value.Ventas.ToArray();

        public static object[] DetallesVenta() => ventas.Value.Detalles.ToArray();

        private static (List<object>, List<object>) GenerarVentas()
        {
            var aleatorio = new Random(2026);
            var ventasGeneradas = new List<object>();
            var detallesGenerados = new List<object>();

            var probabilidadPorSucursal = new Dictionary<int, double> { [1] = 0.45, [2] = 0.32, [3] = 0.22 };
            var vendedoresPorSucursal = new Dictionary<int, int[]> { [1] = new[] { 2, 2, 5 }, [2] = new[] { 3, 3, 1 }, [3] = new[] { 4, 4, 4 } };
            var pesoTotal = productos.Sum(p => p.Peso);
            var id = 1;

            for (var dia = new DateTime(2026, 8, 1); dia <= new DateTime(2026, 10, 25); dia = dia.AddDays(1))
            {
                foreach (var (sucursalId, probabilidad) in probabilidadPorSucursal)
                {
                    if (aleatorio.NextDouble() > probabilidad)
                        continue;

                    var vendedores = vendedoresPorSucursal[sucursalId];
                    var fechaHora = dia.AddHours(8 + aleatorio.Next(11)).AddMinutes(aleatorio.Next(60));

                    ventasGeneradas.Add(new
                    {
                        Id = id,
                        FechaHora = fechaHora,
                        UsuarioId = vendedores[aleatorio.Next(vendedores.Length)],
                        SucursalId = sucursalId
                    });

                    var elegidos = new HashSet<int>();
                    var cantidadItems = 1 + aleatorio.Next(3);

                    while (elegidos.Count < cantidadItems)
                    {
                        var producto = ElegirProducto(aleatorio.Next(pesoTotal));
                        if (!elegidos.Add(producto.Id))
                            continue;

                        detallesGenerados.Add(new
                        {
                            VentaId = id,
                            ProductoId = producto.Id,
                            Cantidad = aleatorio.Next(producto.CantidadMin, producto.CantidadMax + 1),
                            PrecioUnitario = PrecioDelMes(producto.Precio, dia.Month)
                        });
                    }

                    id++;
                }
            }

            return (ventasGeneradas, detallesGenerados);
        }

        private static ProductoSemilla ElegirProducto(int valor)
        {
            foreach (var producto in productos)
            {
                if (valor < producto.Peso)
                    return producto;
                valor -= producto.Peso;
            }

            return productos[^1];
        }

        private static decimal PrecioDelMes(decimal precioActual, int mes)
        {
            var factor = mes switch
            {
                8 => 0.88m,
                9 => 0.94m,
                _ => 1m
            };

            return decimal.Round(precioActual * factor / 50m) * 50m;
        }
    }
}
