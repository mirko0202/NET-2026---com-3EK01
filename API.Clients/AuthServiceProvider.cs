namespace API.Clients
{
    public static class AuthServiceProvider
    {
        private static IAuthService? instance;
        private static readonly object candado = new();

        public static IAuthService Instance =>
            instance ?? throw new InvalidOperationException(
                "No hay un servicio de autenticación registrado. Llamar a AuthServiceProvider.Register() al iniciar la aplicación.");

        public static void Register(IAuthService authService)
        {
            lock (candado)
                instance = authService;
        }
    }
}
