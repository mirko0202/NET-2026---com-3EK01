namespace WindowsForms
{
    public class ProductoDisponible
    {
        public int Id { get; }
        public string Nombre { get; }
        public string? Unidad { get; }
        public int Disponible { get; }

        public ProductoDisponible(int id, string nombre, string? unidad, int disponible)
        {
            Id = id;
            Nombre = nombre;
            Unidad = unidad;
            Disponible = disponible;
        }
    }
}
