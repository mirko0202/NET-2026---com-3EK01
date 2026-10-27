namespace Domain.Model
{
    public class IngresoProducto
    {
        public int Id { get; private set; }
        public DateTime FechaHora { get; private set; }
        public int ProductoId { get; private set; }
        public Producto? Producto { get; private set; }
        public int SucursalId { get; private set; }
        public Sucursal? Sucursal { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }
        public int Cantidad { get; private set; }
        public string Proveedor { get; private set; } = string.Empty;
        public string? Comprobante { get; private set; }
        public string? Observacion { get; private set; }

        public IngresoProducto(int id, DateTime fechaHora, int productoId, int sucursalId, int usuarioId,
                               int cantidad, string proveedor, string? comprobante, string? observacion)
        {
            SetId(id);
            SetFechaHora(fechaHora);
            SetProductoId(productoId);
            SetSucursalId(sucursalId);
            SetUsuarioId(usuarioId);
            SetCantidad(cantidad);
            SetProveedor(proveedor);
            SetComprobante(comprobante);
            SetObservacion(observacion);
        }

        private IngresoProducto() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }

        public void SetFechaHora(DateTime fechaHora)
        {
            if (fechaHora == default)
                throw new ArgumentException("La fecha del ingreso es requerida.", nameof(fechaHora));
            if (fechaHora > DateTime.Now.AddMinutes(5))
                throw new ArgumentException("La fecha del ingreso no puede ser futura.", nameof(fechaHora));
            FechaHora = fechaHora;
        }

        public void SetProductoId(int productoId)
        {
            if (productoId <= 0)
                throw new ArgumentException("Debe indicar el producto que ingresa.", nameof(productoId));
            ProductoId = productoId;
        }

        public void SetSucursalId(int sucursalId)
        {
            if (sucursalId <= 0)
                throw new ArgumentException("Debe indicar la sucursal que recibe la mercadería.", nameof(sucursalId));
            SucursalId = sucursalId;
        }

        public void SetUsuarioId(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Debe indicar el usuario que registra el ingreso.", nameof(usuarioId));
            UsuarioId = usuarioId;
        }

        public void SetCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad ingresada debe ser mayor a 0.", nameof(cantidad));
            if (cantidad > 100_000)
                throw new ArgumentException("La cantidad ingresada no puede superar las 100.000 unidades.", nameof(cantidad));
            Cantidad = cantidad;
        }

        public void SetProveedor(string proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor))
                throw new ArgumentException("El proveedor es requerido.", nameof(proveedor));
            if (proveedor.Trim().Length > 100)
                throw new ArgumentException("El proveedor no puede superar los 100 caracteres.", nameof(proveedor));
            Proveedor = proveedor.Trim();
        }

        public void SetComprobante(string? comprobante)
        {
            if (comprobante != null && comprobante.Trim().Length > 50)
                throw new ArgumentException("El comprobante no puede superar los 50 caracteres.", nameof(comprobante));
            Comprobante = string.IsNullOrWhiteSpace(comprobante) ? null : comprobante.Trim();
        }

        public void SetObservacion(string? observacion)
        {
            if (observacion != null && observacion.Trim().Length > 255)
                throw new ArgumentException("La observación no puede superar los 255 caracteres.", nameof(observacion));
            Observacion = string.IsNullOrWhiteSpace(observacion) ? null : observacion.Trim();
        }
    }
}
