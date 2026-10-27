namespace Domain.Model
{
    public enum CategoriaProducto
    {
        Panificados = 1,
        Facturas = 2,
        Pasteleria = 3,
        Salados = 4,
        Cafeteria = 5,
        Bebidas = 6
    }

    public static class CategoriaProductoExtensions
    {
        public static string Nombre(this CategoriaProducto categoria) => categoria switch
        {
            CategoriaProducto.Panificados => "Panificados",
            CategoriaProducto.Facturas => "Facturas",
            CategoriaProducto.Pasteleria => "Pastelería",
            CategoriaProducto.Salados => "Salados",
            CategoriaProducto.Cafeteria => "Cafetería",
            CategoriaProducto.Bebidas => "Bebidas",
            _ => categoria.ToString()
        };
    }
}
