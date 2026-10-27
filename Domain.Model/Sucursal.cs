namespace Domain.Model
{
    public class Sucursal
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string? Direccion { get; private set; }
        public string? Telefono { get; private set; }
        public bool Activa { get; private set; }

        public Sucursal(int id, string nombre, string? direccion, string? telefono, bool activa = true)
        {
            SetId(id);
            SetNombre(nombre);
            SetDireccion(direccion);
            SetTelefono(telefono);
            SetActiva(activa);
        }

        private Sucursal() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la sucursal no puede ser nulo o vacío.", nameof(nombre));
            if (nombre.Length > 100)
                throw new ArgumentException("El nombre de la sucursal no puede superar los 100 caracteres.", nameof(nombre));
            Nombre = nombre.Trim();
        }

        public void SetDireccion(string? direccion)
        {
            if (direccion != null && direccion.Length > 150)
                throw new ArgumentException("La dirección no puede superar los 150 caracteres.", nameof(direccion));
            Direccion = direccion;
        }

        public void SetTelefono(string? telefono)
        {
            if (telefono != null && telefono.Length > 30)
                throw new ArgumentException("El teléfono no puede superar los 30 caracteres.", nameof(telefono));
            Telefono = telefono;
        }

        public void SetActiva(bool activa) => Activa = activa;
    }
}
