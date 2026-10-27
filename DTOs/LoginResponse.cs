namespace DTOs
{
    public class LoginResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string? Token { get; set; }
        public DateTime? Expiracion { get; set; }
        public UsuarioDTO? Usuario { get; set; }
    }
}
