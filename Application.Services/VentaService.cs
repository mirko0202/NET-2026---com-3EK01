using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository ventaRepository;
        private readonly IProductoRepository productoRepository;
        private readonly ISucursalRepository sucursalRepository;

        public VentaService(IVentaRepository ventaRepository,
                            IProductoRepository productoRepository,
                            ISucursalRepository sucursalRepository)
        {
            this.ventaRepository = ventaRepository;
            this.productoRepository = productoRepository;
            this.sucursalRepository = sucursalRepository;
        }

        public async Task<VentaDTO?> GetAsync(int id)
        {
            var venta = await ventaRepository.GetAsync(id);
            return venta == null ? null : ToDTO(venta);
        }

        public async Task<IEnumerable<VentaDTO>> GetAllAsync()
        {
            var ventas = await ventaRepository.GetAllAsync();
            return ventas.Select(ToDTO).ToList();
        }

        public async Task<VentaDTO> AddAsync(VentaDTO dto, int usuarioId)
        {
            await ValidarSucursalAsync(dto.SucursalId);

            var venta = new Venta(0, DateTime.Now, usuarioId, dto.SucursalId);

            foreach (var item in dto.Detalles)
                venta.AgregarDetalle(item.ProductoId, item.Cantidad, item.PrecioUnitario);

            venta.ValidarDetalles();

            foreach (var detalle in venta.Detalles)
            {
                var producto = await ObtenerProductoAsync(detalle.ProductoId);
                producto.DescontarStock(detalle.Cantidad);
            }

            await ventaRepository.AddAsync(venta);

            var registrada = await ventaRepository.GetAsync(venta.Id);
            return ToDTO(registrada!);
        }

        public async Task<bool> UpdateAsync(VentaDTO dto)
        {
            var venta = await ventaRepository.GetAsync(dto.Id);
            if (venta == null) return false;

            await ValidarSucursalAsync(dto.SucursalId);

            var cantidadesAnteriores = venta.Detalles.ToDictionary(d => d.ProductoId, d => d.Cantidad);

            if (dto.Detalles.GroupBy(d => d.ProductoId).Any(g => g.Count() > 1))
                throw new ArgumentException("Un producto no puede figurar dos veces en la misma venta.");

            venta.SetSucursalId(dto.SucursalId);

            var productosQuitados = cantidadesAnteriores.Keys
                .Except(dto.Detalles.Select(d => d.ProductoId))
                .ToList();

            foreach (var productoId in productosQuitados)
                venta.QuitarDetalle(productoId);

            foreach (var item in dto.Detalles)
            {
                if (venta.ObtenerDetalle(item.ProductoId) != null)
                    venta.ActualizarDetalle(item.ProductoId, item.Cantidad, item.PrecioUnitario);
                else
                    venta.AgregarDetalle(item.ProductoId, item.Cantidad, item.PrecioUnitario);
            }

            venta.ValidarDetalles();

            var cantidadesNuevas = venta.Detalles.ToDictionary(d => d.ProductoId, d => d.Cantidad);

            foreach (var productoId in cantidadesAnteriores.Keys.Union(cantidadesNuevas.Keys))
            {
                var diferencia = cantidadesNuevas.GetValueOrDefault(productoId)
                               - cantidadesAnteriores.GetValueOrDefault(productoId);

                if (diferencia == 0) continue;

                var producto = await ObtenerProductoAsync(productoId);

                if (diferencia > 0)
                    producto.DescontarStock(diferencia);
                else
                    producto.ReponerStock(-diferencia);
            }

            return await ventaRepository.UpdateAsync(venta);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var venta = await ventaRepository.GetAsync(id);
            if (venta == null) return false;

            foreach (var detalle in venta.Detalles)
            {
                var producto = await ObtenerProductoAsync(detalle.ProductoId);
                producto.ReponerStock(detalle.Cantidad);
            }

            return await ventaRepository.DeleteAsync(id);
        }

        private async Task ValidarSucursalAsync(int sucursalId)
        {
            var sucursal = await sucursalRepository.GetAsync(sucursalId)
                ?? throw new ArgumentException("La sucursal indicada no existe.");

            if (!sucursal.Activa)
                throw new InvalidOperationException($"La sucursal '{sucursal.Nombre}' no está activa.");
        }

        private async Task<Producto> ObtenerProductoAsync(int productoId) =>
            await productoRepository.GetAsync(productoId)
            ?? throw new ArgumentException($"El producto con Id {productoId} no existe.");

        private static VentaDTO ToDTO(Venta v) => new()
        {
            Id = v.Id,
            FechaHora = v.FechaHora,
            UsuarioId = v.UsuarioId,
            UsuarioNombre = v.Usuario?.NombreCompleto,
            SucursalId = v.SucursalId,
            SucursalNombre = v.Sucursal?.Nombre,
            Detalles = v.Detalles
                .OrderBy(d => d.Producto?.Nombre)
                .Select(d => new DetalleVentaDTO
                {
                    ProductoId = d.ProductoId,
                    ProductoNombre = d.Producto?.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                })
                .ToList()
        };
    }
}
