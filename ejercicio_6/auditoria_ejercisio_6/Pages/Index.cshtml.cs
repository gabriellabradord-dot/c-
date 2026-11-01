using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Trabajadores.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string CapitalInicial { get; set; }

    [BindProperty]
    public string Metodo { get; set; } = "FOR";

    public string Resultado { get; set; }
    public bool TieneErrores { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        // 1. Validar la entrada de dinero
        if (!decimal.TryParse(CapitalInicial, out decimal capital) || capital <= 0)
        {
            Resultado = "<h3 class='text-danger fw-bold'>⚠️ ¡Atención!</h3>" +
                        "<p class='fs-4 mb-0'>Por favor escribe o selecciona una cantidad de dinero válida antes de presionar el botón.</p>";
            TieneErrores = true;
            return;
        }

        decimal dineroActual = capital;
        string detalleMeses = "";

        // 2. Ejecutar el ciclo según la elección (FOR vs WHILE)
        if (Metodo == "WHILE")
        {
            int mes = 1;
            while (mes <= 12)
            {
                decimal gananciaMes = dineroActual * 0.02m;
                dineroActual += gananciaMes;
                detalleMeses += $"<div class='d-flex justify-content-between border-bottom py-1 fs-5'><span>Mes {mes}:</span> <span>+${gananciaMes:N2} ➔ <b>${dineroActual:N2}</b></span></div>";
                mes++;
            }
        }
        else // Por defecto usa FOR
        {
            for (int mes = 1; mes <= 12; mes++)
            {
                decimal gananciaMes = dineroActual * 0.02m;
                dineroActual += gananciaMes;
                detalleMeses += $"<div class='d-flex justify-content-between border-bottom py-1 fs-5'><span>Mes {mes}:</span> <span>+${gananciaMes:N2} ➔ <b>${dineroActual:N2}</b></span></div>";
            }
        }

        decimal gananciaTotal = dineroActual - capital;

        // 3. Generar el reporte con diseño gigante para adultos mayores
        string reporte = $"<div class='text-center mb-3'>" +
                         $"  <span class='badge bg-primary fs-6 mb-2'>Calculado con {Metodo}</span>" +
                         $"  <h3 class='fw-bold text-dark fs-2'>En 1 año tendrás:</h3>" +
                         $"  <div class='display-4 fw-bold text-success'>${dineroActual:N2}</div>" +
                         $"  <div class='fs-4 text-muted mt-2'>Tu ganancia limpia fue de: <b class='text-success'>+${gananciaTotal:N2}</b></div>" +
                         $"</div>" +
                         $"<hr/>" +
                         $"<details class='mt-3'>" +
                         $"  <summary class='btn btn-outline-secondary w-100 fs-5 fw-bold'>🔍 Toca aquí para ver el detalle de cada mes</summary>" +
                         $"  <div class='p-3 bg-white rounded mt-2 border' style='max-height: 250px; overflow-y: auto;'>" +
                         $"    {detalleMeses}" +
                         $"  </div>" +
                         $"</details>";

        TieneErrores = false;
        Resultado = reporte;
    }
}