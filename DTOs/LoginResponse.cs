namespace DTOs
{
    public class LoginResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public UsuarioDTO? Usuario { get; set; }
    }
}
