using API.Clients;

namespace WindowsForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            while (true)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        return;
                }

                var home = new Home();
                Application.Run(home);

                SesionActual.Cerrar();

                if (!home.CerroSesion)
                    return;
            }
        }
    }
}
