namespace Domain.Model
{
    public class Venta
    {
        private readonly List<DetalleVenta> _detalles = new();

        public int Id { get; private set; }
        public DateTime FechaHora { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }
        public int SucursalId { get; private set; }
        public Sucursal? Sucursal { get; private set; }

        public IReadOnlyCollection<DetalleVenta> Detalles => _detalles.AsReadOnly();

        public decimal Total => _detalles.Sum(d => d.Subtotal);

        public Venta(int id, DateTime fechaHora, int usuarioId, int sucursalId)
        {
            SetId(id);
            SetFechaHora(fechaHora);
            SetUsuarioId(usuarioId);
            SetSucursalId(sucursalId);
        }

        private Venta() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }

        public void SetFechaHora(DateTime fechaHora)
        {
            if (fechaHora == default)
                throw new ArgumentException("La fecha de la venta es requerida.", nameof(fechaHora));
            if (fechaHora > DateTime.Now.AddMinutes(5))
                throw new ArgumentException("La fecha de la venta no puede ser futura.", nameof(fechaHora));
            FechaHora = fechaHora;
        }

        public void SetUsuarioId(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Debe indicar el usuario que registra la venta.", nameof(usuarioId));
            UsuarioId = usuarioId;
        }

        public void SetSucursalId(int sucursalId)
        {
            if (sucursalId <= 0)
                throw new ArgumentException("Debe indicar la sucursal de la venta.", nameof(sucursalId));
            SucursalId = sucursalId;
        }

        public void AgregarDetalle(int productoId, int cantidad, decimal precioUnitario)
        {
            if (_detalles.Any(d => d.ProductoId == productoId))
                throw new ArgumentException("Un producto no puede figurar dos veces en la misma venta.", nameof(productoId));

            _detalles.Add(new DetalleVenta(Id, productoId, cantidad, precioUnitario));
        }

        public void ActualizarDetalle(int productoId, int cantidad, decimal precioUnitario)
        {
            var detalle = ObtenerDetalle(productoId)
                ?? throw new ArgumentException("El producto no figura en la venta.", nameof(productoId));

            detalle.SetCantidad(cantidad);
            detalle.SetPrecioUnitario(precioUnitario);
        }

        public void QuitarDetalle(int productoId)
        {
            var detalle = ObtenerDetalle(productoId)
                ?? throw new ArgumentException("El producto no figura en la venta.", nameof(productoId));

            _detalles.Remove(detalle);
        }

        public DetalleVenta? ObtenerDetalle(int productoId) =>
            _detalles.FirstOrDefault(d => d.ProductoId == productoId);

        public void ValidarDetalles()
        {
            if (_detalles.Count == 0)
                throw new ArgumentException("La venta debe tener al menos un producto.");
        }
    }
}
