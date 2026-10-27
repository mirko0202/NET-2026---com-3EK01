using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class IngresoProductoService : IIngresoProductoService
    {
        private readonly IIngresoProductoRepository ingresoRepository;
        private readonly IProductoRepository productoRepository;
        private readonly ISucursalRepository sucursalRepository;

        public IngresoProductoService(IIngresoProductoRepository ingresoRepository,
                                      IProductoRepository productoRepository,
                                      ISucursalRepository sucursalRepository)
        {
            this.ingresoRepository = ingresoRepository;
            this.productoRepository = productoRepository;
            this.sucursalRepository = sucursalRepository;
        }

        public async Task<IngresoProductoDTO?> GetAsync(int id)
        {
            var ingreso = await ingresoRepository.GetAsync(id);
            return ingreso == null ? null : ToDTO(ingreso);
        }

        public async Task<IEnumerable<IngresoProductoDTO>> GetAllAsync(int? productoId, DateTime? desde, DateTime? hasta)
        {
            if (desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.");

            var ingresos = await ingresoRepository.GetAllAsync(productoId, desde, hasta);
            return ingresos.Select(ToDTO).ToList();
        }

        public async Task<IngresoProductoDTO> AddAsync(IngresoProductoDTO dto, int usuarioId)
        {
            await ValidarSucursalAsync(dto.SucursalId);

            var fecha = dto.FechaHora == default ? DateTime.Now : dto.FechaHora;
            var ingreso = new IngresoProducto(0, fecha, dto.ProductoId, dto.SucursalId, usuarioId,
                                              dto.Cantidad, dto.Proveedor, dto.Comprobante, dto.Observacion);

            var producto = await ObtenerProductoAsync(ingreso.ProductoId);
            producto.ReponerStock(ingreso.Cantidad);

            await ingresoRepository.AddAsync(ingreso);

            var registrado = await ingresoRepository.GetAsync(ingreso.Id);
            return ToDTO(registrado!);
        }

        public async Task<bool> UpdateAsync(IngresoProductoDTO dto)
        {
            var ingreso = await ingresoRepository.GetAsync(dto.Id);
            if (ingreso == null) return false;

            await ValidarSucursalAsync(dto.SucursalId, ingreso.SucursalId);

            var productoAnteriorId = ingreso.ProductoId;
            var cantidadAnterior = ingreso.Cantidad;

            ingreso.SetFechaHora(dto.FechaHora == default ? ingreso.FechaHora : dto.FechaHora);
            ingreso.SetProductoId(dto.ProductoId);
            ingreso.SetSucursalId(dto.SucursalId);
            ingreso.SetCantidad(dto.Cantidad);
            ingreso.SetProveedor(dto.Proveedor);
            ingreso.SetComprobante(dto.Comprobante);
            ingreso.SetObservacion(dto.Observacion);

            if (ingreso.ProductoId == productoAnteriorId)
            {
                var producto = await ObtenerProductoAsync(ingreso.ProductoId);
                var diferencia = ingreso.Cantidad - cantidadAnterior;

                if (diferencia > 0)
                    producto.ReponerStock(diferencia);
                else if (diferencia < 0)
                    RetirarStock(producto, -diferencia, "modificar");
            }
            else
            {
                var productoAnterior = await ObtenerProductoAsync(productoAnteriorId);
                RetirarStock(productoAnterior, cantidadAnterior, "modificar");

                var productoNuevo = await ObtenerProductoAsync(ingreso.ProductoId);
                productoNuevo.ReponerStock(ingreso.Cantidad);
            }

            return await ingresoRepository.UpdateAsync(ingreso);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ingreso = await ingresoRepository.GetAsync(id);
            if (ingreso == null) return false;

            var producto = await ObtenerProductoAsync(ingreso.ProductoId);
            RetirarStock(producto, ingreso.Cantidad, "eliminar");

            return await ingresoRepository.DeleteAsync(id);
        }

        private static void RetirarStock(Producto producto, int cantidad, string accion)
        {
            if (producto.Stock < cantidad)
                throw new InvalidOperationException(
                    $"No se puede {accion} el ingreso: el stock actual de '{producto.Nombre}' ({producto.Stock}) " +
                    $"es menor que la cantidad a retirar ({cantidad}), porque parte de la mercadería ya se vendió.");

            producto.DescontarStock(cantidad);
        }

        private async Task ValidarSucursalAsync(int sucursalId, int? sucursalActualId = null)
        {
            var sucursal = await sucursalRepository.GetAsync(sucursalId)
                ?? throw new ArgumentException("La sucursal indicada no existe.");

            if (!sucursal.Activa && sucursal.Id != sucursalActualId)
                throw new InvalidOperationException($"La sucursal '{sucursal.Nombre}' no está activa.");
        }

        private async Task<Producto> ObtenerProductoAsync(int productoId) =>
            await productoRepository.GetAsync(productoId)
            ?? throw new ArgumentException($"El producto con Id {productoId} no existe.");

        private static IngresoProductoDTO ToDTO(IngresoProducto i) => new()
        {
            Id = i.Id,
            FechaHora = i.FechaHora,
            ProductoId = i.ProductoId,
            ProductoNombre = i.Producto?.Nombre,
            Unidad = i.Producto?.Unidad,
            SucursalId = i.SucursalId,
            SucursalNombre = i.Sucursal?.Nombre,
            UsuarioId = i.UsuarioId,
            UsuarioNombre = i.Usuario?.NombreCompleto,
            Cantidad = i.Cantidad,
            Proveedor = i.Proveedor,
            Comprobante = i.Comprobante,
            Observacion = i.Observacion
        };
    }
}
