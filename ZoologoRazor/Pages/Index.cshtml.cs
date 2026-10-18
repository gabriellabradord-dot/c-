using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZoologoRazor.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string TipoAnimal { get; set; } = "Elefantes";

    [BindProperty]
    public string EdadesTexto { get; set; } = string.Empty;

    // Resultados
    public int TotalMuestrasRequeridas { get; set; }
    public int TotalMuestrasIngresadas { get; set; }

    public int ContCategoria1 { get; set; } // 0 a 1 año
    public double Porcentaje1 { get; set; }

    public int ContCategoria2 { get; set; } // Más de 1 y menos de 3 años
    public double Porcentaje2 { get; set; }

    public int ContCategoria3 { get; set; } // 3 o más años
    public double Porcentaje3 { get; set; }

    public bool HaCalculado { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        // Definimos el tamaño de la muestra según el animal elegido[cite: 6]
        if (TipoAnimal == "Elefantes") TotalMuestrasRequeridas = 20;
        else if (TipoAnimal == "Jirafas") TotalMuestrasRequeridas = 15;
        else if (TipoAnimal == "Chimpancés") TotalMuestrasRequeridas = 40;

        if (!string.IsNullOrEmpty(EdadesTexto))
        {
            var partes = EdadesTexto.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int cantidadAProcesar = Math.Min(partes.Length, TotalMuestrasRequeridas);

            int cat1 = 0, cat2 = 0, cat3 = 0;

            for (int i = 0; i < cantidadAProcesar; i++)
            {
                if (double.TryParse(partes[i], out double edad))
                {
                    // Categorías según la guía[cite: 6]
                    if (edad >= 0 && edad <= 1)
                    {
                        cat1++;
                    }
                    else if (edad > 1 && edad < 3)
                    {
                        cat2++;
                    }
                    else if (edad >= 3)
                    {
                        cat3++;
                    }
                }
            }

            TotalMuestrasIngresadas = cantidadAProcesar;

            if (TotalMuestrasIngresadas > 0)
            {
                ContCategoria1 = cat1;
                Porcentaje1 = ((double)cat1 / TotalMuestrasIngresadas) * 100;

                ContCategoria2 = cat2;
                Porcentaje2 = ((double)cat2 / TotalMuestrasIngresadas) * 100;

                ContCategoria3 = cat3;
                Porcentaje3 = ((double)cat3 / TotalMuestrasIngresadas) * 100;
            }

            HaCalculado = true;
        }
    }
}