using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Trabajadores.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string CapitalInicial { get; set; }

    public string Resultado { get; set; }
    public bool TieneErrores { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
       
        if (!decimal.TryParse(CapitalInicial, out decimal capital) || capital <= 0)
        {
            Resultado = " Por favor, ingresa un capital inicial válido mayor a cero.";
            TieneErrores = true;
            return;
        }

        decimal dineroActual = capital;
        string detalleMeses = "";

        
        for (int mes = 1; mes <= 12; mes++)
        {
           
            decimal gananciaMes = dineroActual * 0.02m;
            
            
            dineroActual += gananciaMes;

            
            detalleMeses += $"Mes {mes}: Ganancia = ${gananciaMes:N2} | Total = ${dineroActual:N2}<br/>";
        }

        
        string reporte = $"<b>Capital Inicial:</b> ${capital:N2}<br/>" +
                         $"<b>Interés mensual:</b> 2% (Reinversion mensual)<br/>" +
                         $" <b>Plazo:</b> 1 año (12 meses)<br/><br/>" +
                         $"<b>Evolución mes a mes:</b><br/>" +
                         $"<div style='background: #f8f9fa; padding: 10px; border-radius: 5px; margin-top: 5px; font-size: 0.95rem;'>" +
                         detalleMeses +
                         $"</div><br/>" +
                         $" <b>Cantidad final de dinero al cabo de un año:</b> <span style='color: #28a745; font-size: 1.2rem;'><b>${dineroActual:N2}</b></span>";

        TieneErrores = false;
        Resultado = reporte;
    }
}