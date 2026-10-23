using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace Trabajadores.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string CantidadAutos { get; set; }

    [BindProperty]
    public string UltimosDigitos { get; set; }

    public string Resultado { get; set; }
    public bool TieneErrores { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        
        if (!int.TryParse(CantidadAutos, out int totalAutos) || totalAutos <= 0)
        {
            Resultado = "Ingresa un número total de autos válido mayor a cero.";
            TieneErrores = true;
            return;
        }

       
        if (string.IsNullOrWhiteSpace(UltimosDigitos))
        {
            Resultado = " El campo de los dígitos de las placas está vacío.";
            TieneErrores = true;
            return;
        }

        
        string[] listaDigitos = Regex.Split(UltimosDigitos.Trim(), @"[\s,]+");

        
        if (listaDigitos.Length != totalAutos)
        {
            Resultado = $"Indicaste que eran <b>{totalAutos}</b> autos, pero escribiste <b>{listaDigitos.Length}</b> dígitos. Revisa que la cantidad coincida.";
            TieneErrores = true;
            return;
        }

        
        int amarilla = 0; 
        int rosa = 0;     
        int roja = 0;     
        int verde = 0;    
        int azul = 0;     
        int noReconocidos = 0;

        
        for (int i = 0; i < totalAutos; i++)
        {
            string digito = listaDigitos[i].Trim();

            switch (digito)
            {
                case "1":
                case "2":
                    amarilla++;
                    break;
                case "3":
                case "4":
                    rosa++;
                    break;
                case "5":
                case "6":
                    roja++;
                    break;
                case "7":
                case "8":
                    verde++;
                    break;
                case "9":
                case "0":
                    azul++;
                    break;
                default:
                    noReconocidos++;
                    break;
            }
        }

       
        string reporte = $" <b>Total de autos analizados (n):</b> {totalAutos}<br/>" +
                         $" <b>Calcomanía Amarilla (1 o 2):</b> {amarilla}<br/>" +
                         $" <b>Calcomanía Rosa (3 o 4):</b> {rosa}<br/>" +
                         $" <b>Calcomanía Roja (5 o 6):</b> {roja}<br/>" +
                         $" <b>Calcomanía Verde (7 o 8):</b> {verde}<br/>" +
                         $" <b>Calcomanía Azul (9 o 0):</b> {azul}";

        if (noReconocidos > 0)
        {
            TieneErrores = true;
            reporte += $"<br/><br/><b>Atención:</b> Encontramos {noReconocidos} valor(es) que no corresponden a un dígito válido (0-9).</span>";
        }
        else
        {
            TieneErrores = false;
        }

        Resultado = reporte;
    }
}