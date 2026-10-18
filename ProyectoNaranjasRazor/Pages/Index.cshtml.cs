using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProyectoNaranjasRazor.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string KilosTexto { get; set; } = string.Empty;

    [BindProperty]
    public double PrecioKilo { get; set; } = 4000; // Precio base de ejemplo por kilo

    public List<string> DetalleClientes { get; set; } = new();
    public double TotalTienda { get; set; } = 0;
    public bool HaCalculado { get; set; } = false;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(KilosTexto))
        {
            var partes = KilosTexto.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int clientesAProcesar = Math.Min(partes.Length, 15); 
            
            TotalTienda = 0;
            DetalleClientes.Clear();

            for (int i = 0; i < clientesAProcesar; i++)
            {
                if (double.TryParse(partes[i], out double kilos))
                {
                    double subtotal = kilos * PrecioKilo;
                    double totalPagar = subtotal;
                    string estadoDescuento = "Sin descuento";

                   
                    if (kilos > 10)
                    {
                        totalPagar = subtotal * 0.85; 
                        estadoDescuento = "¡15% de descuento aplicado!";
                    }

                    TotalTienda += totalPagar;
                    DetalleClientes.Add($"Cliente {i + 1}: {kilos} kg | Paga: ${totalPagar:F2} ({estadoDescuento})");
                }
            }
            HaCalculado = true;
        }
    }
}