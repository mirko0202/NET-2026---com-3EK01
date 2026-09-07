using DTOs;

namespace API.Clients
{
    public static class SesionActual
    {
        public static UsuarioDTO? Usuario { get; private set; }

        public static bool HayUsuarioAutenticado => Usuario != null;

        public static bool EsAdministrador => Usuario?.EsAdministrador == true;

        public static void Iniciar(UsuarioDTO usuario) => Usuario = usuario;

        public static void Cerrar() => Usuario = null;
    }
}
