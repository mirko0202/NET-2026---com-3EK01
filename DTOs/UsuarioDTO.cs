namespace DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public bool EsAdministrador { get; set; }
        public bool Activo { get; set; } = true;
        public int? SucursalId { get; set; }
        public string? SucursalNombre { get; set; }
        public string? Password { get; set; }
    }
}
