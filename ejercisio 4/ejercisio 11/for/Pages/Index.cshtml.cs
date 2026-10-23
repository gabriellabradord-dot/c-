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

        
        for (int p = 1; p <= 5; p++)
        {
            string pesoAnteriorStr = Request.Form[$"PesoAnterior_{p}"];
            string pesajesStr = Request.Form[$"Pesajes_{p}"];

            if (!double.TryParse(pesoAnteriorStr, out double pesoAnterior))
            {
                Resultado = $"Ingresa un peso anterior válido para la Persona {p}.";
                return;
            }

            
            string[] lecturas = Regex.Split(pesajesStr.Trim(), @"[\s,]+");

            if (lecturas.Length != 10)
            {
                Resultado = $"La Persona {p} debe tener exactamente 10 pesajes registrados. Ingresaste {lecturas.Length}.";
                return;
            }

            double sumaPesos = 0;

            
            for (int b = 0; b < 10; b++)
            {
                if (double.TryParse(lecturas[b], out double pesoActual))
                {
                    sumaPesos += pesoActual;
                }
            }

            
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
        }

        Resultado = reporte;
    }
}