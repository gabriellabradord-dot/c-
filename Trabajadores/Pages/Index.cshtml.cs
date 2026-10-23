using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Trabajadores.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Txtcantidad { get; set; }

    [BindProperty]
    public string Txtht { get; set; }

    public string Resultado { get; set; }

    public void OnGet()
    {
    }

    public void OnPost()
    {
        try
        {
            int i = 0;
            int ht, he;
            decimal salario;

            if (!int.TryParse(Txtcantidad, out int obreros))
            {
                Resultado = "Escribe un número entero válido para la cantidad de obreros.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Txtht))
            {
                Resultado = "Por favor ingresa las horas de los obreros.";
                return;
            }

            // Separar las horas de cada obrero.
            string[] horas = Txtht.Split(',');

            if (obreros <= 0 || horas.Length != obreros)
            {
                Resultado = "Ingresa una cantidad positiva de obreros y las horas de cada uno separadas por comas.";
                return;
            }

            string resultadoTemp = "";

            while (i < obreros)
            {
                if (!int.TryParse(horas[i].Trim(), out ht))
                {
                    Resultado = $"El valor de las horas para el obrero {i + 1} no es un número válido.";
                    return;
                }

                if (ht < 0)
                {
                    Resultado = "Las horas del obrero " + (i + 1) + " no pueden ser negativas.";
                    return;
                }

                if (ht > 40)
                {
                    he = ht - 40;
                    salario = 40 * 20m + he * 25m;
                }
                else
                {
                    salario = ht * 20m;
                }

                resultadoTemp += "El salario semanal del obrero " + (i + 1) + " es: $" + salario.ToString("N2") + "<br/>";
                i++;
            }

            Resultado = resultadoTemp;
        }
        catch (OverflowException)
        {
            Resultado = "Ingresaste un número demasiado grande.";
        }
    }
}