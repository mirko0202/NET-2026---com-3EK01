namespace WebAPI
{
    public static class Errores
    {
        public static IResult BadRequest(Exception ex) =>
            Results.BadRequest(new { error = Mensaje(ex) });

        private static string Mensaje(Exception ex)
        {
            if (ex is not ArgumentException { ParamName: not null } argumento)
                return ex.Message;

            var mensaje = argumento.Message;
            var inicioSufijo = mensaje.LastIndexOf(" (", StringComparison.Ordinal);

            return inicioSufijo > 0 && mensaje.EndsWith($"'{argumento.ParamName}')", StringComparison.Ordinal)
                ? mensaje[..inicioSufijo]
                : mensaje;
        }
    }
}
