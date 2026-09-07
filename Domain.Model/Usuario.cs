using System.Security.Cryptography;

namespace Domain.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string NombreCompleto { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Salt { get; private set; } = string.Empty;
        public RolUsuario Rol { get; private set; }
        public bool Activo { get; private set; }

        public bool EsAdministrador => Rol == RolUsuario.Administrador;

        public Usuario(int id, string username, string nombreCompleto,
                       string password, RolUsuario rol, bool activo = true)
        {
            SetId(id);
            SetUsername(username);
            SetNombreCompleto(nombreCompleto);
            SetPassword(password);
            SetRol(rol);
            SetActivo(activo);
        }

        private Usuario() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(username));
            if (username.Trim().Length < 3)
                throw new ArgumentException("El nombre de usuario debe tener al menos 3 caracteres.", nameof(username));
            if (username.Length > 50)
                throw new ArgumentException("El nombre de usuario no puede superar los 50 caracteres.", nameof(username));
            Username = username.Trim();
        }

        public void SetNombreCompleto(string nombreCompleto)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ArgumentException("El nombre completo no puede ser nulo o vacío.", nameof(nombreCompleto));
            if (nombreCompleto.Length > 100)
                throw new ArgumentException("El nombre completo no puede superar los 100 caracteres.", nameof(nombreCompleto));
            NombreCompleto = nombreCompleto.Trim();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(password));
            if (password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.", nameof(password));

            Salt = GenerarSalt();
            PasswordHash = Hashear(password, Salt);
        }

        public void SetRol(RolUsuario rol)
        {
            if (!Enum.IsDefined(typeof(RolUsuario), rol))
                throw new ArgumentException("El rol indicado no es válido.", nameof(rol));
            Rol = rol;
        }

        public void SetActivo(bool activo) => Activo = activo;

        public bool ValidarPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(Salt))
                return false;

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(PasswordHash),
                Convert.FromBase64String(Hashear(password, Salt)));
        }

        private static string GenerarSalt()
        {
            var salt = new byte[32];
            RandomNumberGenerator.Fill(salt);
            return Convert.ToBase64String(salt);
        }

        private static string Hashear(string password, string salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }
    }
}
