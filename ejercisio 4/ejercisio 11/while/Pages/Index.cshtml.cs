using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace Trabajadores.Pages;

public class IndexModel : PageModel
{
    public string Resultado { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        string reporte = "";
        int p = 1;

        // Primer ciclo WHILE: Para procesar a las 5 personas
        while (p <= 5)
        {
            string pesoAnteriorStr = Request.Form[$"PesoAnterior_{p}"];
            string pesajesStr = Request.Form[$"Pesajes_{p}"];

            if (!double.TryParse(pesoAnteriorStr, out double pesoAnterior))
            {
                Resultado = $"Ingresa un peso anterior válido para la Persona {p}.";
                return;
            }

            // Separar las 10 lecturas por comas o espacios
            string[] lecturas = Regex.Split(pesajesStr.Trim(), @"[\s,]+");

            if (lecturas.Length != 10)
            {
                Resultado = $"La Persona {p} debe tener exactamente 10 pesajes registrados. Ingresaste {lecturas.Length}.";
                return;
            }

            double sumaPesos = 0;
            int b = 0;

            // Segundo ciclo WHILE: Para sumar las 10 básculas de la persona actual
            while (b < 10)
            {
                if (double.TryParse(lecturas[b], out double pesoActual))
                {
                    sumaPesos += pesoActual;
                }
                b++;
            }

            // Calcular promedio y diferencia
            double promedioActual = sumaPesos / 10;
            double diferencia = promedioActual - pesoAnterior;

            if (diferencia > 0)
            {
                reporte += $"<b>Persona {p}:</b> SUBIO {diferencia:F2} kilos.<br/>";
            }
            else if (diferencia < 0)
            {
                double kilosBajados = Math.Abs(diferencia);
                reporte += $"<b>Persona {p}:</b> BAJO {kilosBajados:F2} kilos.<br/>";
            }
            else
            {
                reporte += $"<b>Persona {p}:</b> Mantiene el mismo peso exacto.<br/>";
            }

            p++;
        }

        Resultado = reporte;
    }
}