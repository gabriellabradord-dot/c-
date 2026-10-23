using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace Trabajadores.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string CantidadPersonas { get; set; }

    [BindProperty]
    public string Generos { get; set; }

    public string Resultado { get; set; }
    public bool TieneErrores { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        // 1. Validar que la cantidad sea un número real mayor a cero
        if (!int.TryParse(CantidadPersonas, out int totalPersonas) || totalPersonas <= 0)
        {
            Resultado = "⚠️ Por favor, ingresa un número de personas válido que sea mayor a cero.";
            TieneErrores = true;
            return;
        }

        // 2. Validar que no esté vacío
        if (string.IsNullOrWhiteSpace(Generos))
        {
            Resultado = "⚠️ El espacio de géneros está vacío. Escribe las letras correspondientes a cada persona.";
            TieneErrores = true;
            return;
        }

        // 3. Limpieza ultra tolerante: separa tanto por comas como por espacios (incluso múltiples espacios juntos)
        string[] listaGeneros = Regex.Split(Generos.Trim(), @"[\s,]+");

        // 4. Validar que la cantidad escrita coincida con el número total indicado
        if (listaGeneros.Length != totalPersonas)
        {
            Resultado = $"Indicaste que eran <b>{totalPersonas}</b> personas en total, pero en la lista escribiste <b>{listaGeneros.Length}</b> datos. Por favor, verifica que la cantidad coincida.";
            TieneErrores = true;
            return;
        }

        int hombres = 0;
        int mujeres = 0;
        int noReconocidos = 0;

        // 5. Analizar cada elemento de forma muy flexible
        foreach (var item in listaGeneros)
        {
            string limpio = item.ToUpper().Trim();

            switch (limpio)
            {
                case "H":
                case "HOMBRE":
                case "HOMBRES":
                case "MASCULINO":
                case "1":
                    hombres++;
                    break;

                case "M":
                case "MUJER":
                case "MUJERES":
                case "FEMENINO":
                case "2":
                    mujeres++;
                    break;

                default:
                    noReconocidos++;
                    break;
            }
        }

        // 6. Construir el reporte final visual
        string reporte = $"👥 <b>Total de personas analizadas:</b> {totalPersonas}<br/>" +
                         $"👨 <b>Hombres:</b> {hombres}<br/>" +
                         $"👩 <b>Mujeres:</b> {mujeres}";

        if (noReconocidos > 0)
        {
            TieneErrores = true;
            reporte += $"<br/><br/><span style='color: #856404;'>⚠️ <b>Nota:</b> Encontramos {noReconocidos} dato(s) que no pudimos identificar. Asegúrate de usar solo <b>H</b> para hombre o <b>M</b> para mujer e inténtalo de nuevo.</span>";
        }
        else
        {
            TieneErrores = false;
        }

        Resultado = reporte;
    }
}