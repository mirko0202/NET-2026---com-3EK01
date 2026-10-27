using API.Auth.WindowsForms;
using API.Clients;
using System.Globalization;

namespace WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var cultura = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = cultura;
            CultureInfo.DefaultThreadCurrentUICulture = cultura;
            CultureInfo.CurrentCulture = cultura;
            CultureInfo.CurrentUICulture = cultura;

            ApplicationConfiguration.Initialize();

            var authService = new WindowsFormsAuthService();
            AuthServiceProvider.Register(authService);

            while (true)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        return;
                }

                var home = new Home();
                Application.Run(home);

                authService.LogoutAsync().GetAwaiter().GetResult();

                if (!home.CerroSesion)
                    return;
            }
        }
    }
}
