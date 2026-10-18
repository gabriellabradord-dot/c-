using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuestreoPesosRazor.Pages;

public class IndexModel : PageModel
{
   
    [BindProperty]
    public string EdadesTexto { get; set; } = string.Empty;

    [BindProperty]
    public string PesosTexto { get; set; } = string.Empty;

    
    public int ContNiños { get; set; }
    public double PromedioNiños { get; set; }

    public int ContJovenes { get; set; }
    public double PromedioJovenes { get; set; }

    public int ContAdultos { get; set; }
    public double PromedioAdultos { get; set; }

    public int ContAdultosMayores { get; set; }
    public double PromedioAdultosMayores { get; set; }
    
    public bool HaCalculado { get; set; } = false;

    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(EdadesTexto) && !string.IsNullOrEmpty(PesosTexto))
        {
            
            var partesEdades = EdadesTexto.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var partesPesos = PesosTexto.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int[] edades = Array.ConvertAll(partesEdades, int.Parse);
            double[] pesos = Array.ConvertAll(partesPesos, double.Parse);

            int contNiños = 0, contJovenes = 0, contAdultos = 0, contAdultosMayores = 0;
            double sumaNiños = 0, sumaJovenes = 0, sumaAdultos = 0, sumaAdultosMayores = 0;

            
            int total = Math.Min(edades.Length, pesos.Length);
            for (int i = 0; i < total; i++)
            {
                int edad = edades[i];
                double peso = pesos[i];

                if (edad <= 12)
                {
                    contNiños++;
                    sumaNiños += peso;
                }
                else if (edad <= 29)
                {
                    contJovenes++;
                    sumaJovenes += peso;
                }
                else if (edad <= 59)
                {
                    contAdultos++;
                    sumaAdultos += peso;
                }
                else
                {
                    contAdultosMayores++;
                    sumaAdultosMayores += peso;
                }
            }

           
            ContNiños = contNiños;
            PromedioNiños = contNiños > 0 ? sumaNiños / contNiños : 0;

            ContJovenes = contJovenes;
            PromedioJovenes = contJovenes > 0 ? sumaJovenes / contJovenes : 0;

            ContAdultos = contAdultos;
            PromedioAdultos = contAdultos > 0 ? sumaAdultos / contAdultos : 0;

            ContAdultosMayores = contAdultosMayores;
            PromedioAdultosMayores = contAdultosMayores > 0 ? sumaAdultosMayores / contAdultosMayores : 0;

            HaCalculado = true;
        }
    }
}