using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Block4.Models;
using System.Text.Json;

namespace Block4.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Naam { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string Betaalwijze { get; set; } = string.Empty;
        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total { get; set; }

        public IActionResult OnGet()
        {
            Naam = TempData["Naam"] as string ?? string.Empty;
            Adres = TempData["Adres"] as string ?? string.Empty;
            Betaalwijze = TempData["Betaalwijze"] as string ?? string.Empty;
            decimal.TryParse(TempData["Total"] as string, out var total);
            Total = total;

            var cartJson = TempData["CartJson"] as string;
            if (!string.IsNullOrEmpty(cartJson))
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();

            if (string.IsNullOrEmpty(Naam))
                return RedirectToPage("/Checkout");

            return Page();
        }
    }
}
