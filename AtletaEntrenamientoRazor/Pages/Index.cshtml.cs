using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AtletaEntrenamientoRazor.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string TiemposTexto { get; set; } = string.Empty;

    public double PromedioTiempos { get; set; }
    public bool NingunoMayor16 { get; set; }
    public bool AlMenosUnoMayor16 { get; set; }
    public bool PromedioValido { get; set; }
    public bool EsApto { get; set; }
    public bool HaCalculado { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(TiemposTexto))
        {
            var partes = TiemposTexto.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int totalDias = Math.Min(partes.Length, 10); 

            if (totalDias > 0)
            {
                double sumaTiempos = 0;
                bool tieneMayor16 = false;

                for (int i = 0; i < totalDias; i++)
                {
                    if (double.TryParse(partes[i], out double tiempo))
                    {
                        sumaTiempos += tiempo;

                        if (tiempo > 16)
                        {
                            tieneMayor16 = true;
                        }
                    }
                }

                PromedioTiempos = sumaTiempos / totalDias;

                
                NingunoMayor16 = !tieneMayor16;
                
                
                AlMenosUnoMayor16 = tieneMayor16;
                
                
                PromedioValido = PromedioTiempos <= 15;

                
                EsApto = NingunoMayor16 || AlMenosUnoMayor16 || PromedioValido;

                HaCalculado = true;
            }
        }
    }
}