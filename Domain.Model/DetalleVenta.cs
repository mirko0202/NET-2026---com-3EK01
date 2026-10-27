namespace Domain.Model
{
    public class DetalleVenta
    {
        public int VentaId { get; private set; }
        public int ProductoId { get; private set; }
        public Producto? Producto { get; private set; }
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }

        public decimal Subtotal => Cantidad * PrecioUnitario;

        public DetalleVenta(int ventaId, int productoId, int cantidad, decimal precioUnitario)
        {
            SetVentaId(ventaId);
            SetProductoId(productoId);
            SetCantidad(cantidad);
            SetPrecioUnitario(precioUnitario);
        }

        private DetalleVenta() { }

        public void SetVentaId(int ventaId)
        {
            if (ventaId < 0)
                throw new ArgumentException("El Id de la venta debe ser mayor o igual a 0.", nameof(ventaId));
            VentaId = ventaId;
        }

        public void SetProductoId(int productoId)
        {
            if (productoId <= 0)
                throw new ArgumentException("Debe indicar un producto válido.", nameof(productoId));
            ProductoId = productoId;
        }

        public void SetCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.", nameof(cantidad));
            Cantidad = cantidad;
        }

        public void SetPrecioUnitario(decimal precioUnitario)
        {
            if (precioUnitario <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor a 0.", nameof(precioUnitario));
            PrecioUnitario = decimal.Round(precioUnitario, 2);
        }
    }
}
