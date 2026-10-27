using System.Globalization;

namespace API.Clients
{
    internal static class Fechas
    {
        public static string ParaQuery(DateTime fecha) =>
            fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
