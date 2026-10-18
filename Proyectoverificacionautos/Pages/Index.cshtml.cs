using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Proyectoverificacionautos.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string PuntosTexto { get; set; } = string.Empty;

    public double PromedioPuntos { get; set; }
    public double MenorContaminacion { get; set; }
    public double MayorContaminacion { get; set; }
    public bool HaCalculado { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(PuntosTexto))
        {
            var partes = PuntosTexto.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int totalAutos = Math.Min(partes.Length, 25); 

            if (totalAutos > 0)
            {
                double sumaPuntos = 0;
                double menor = double.MaxValue;
                double mayor = double.MinValue;

                for (int i = 0; i < totalAutos; i++)
                {
                    if (double.TryParse(partes[i], out double puntos))
                    {
                        sumaPuntos += puntos;

                        
                        if (puntos < menor) menor = puntos;
                        if (puntos > mayor) mayor = puntos;
                    }
                }

                PromedioPuntos = sumaPuntos / totalAutos;
                MenorContaminacion = menor;
                MayorContaminacion = mayor;
                HaCalculado = true;
            }
        }
    }
}