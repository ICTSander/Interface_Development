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
        public string Total { get; set; } = "0.00";

        public IActionResult OnGet()
        {
            Naam = TempData["Naam"] as string ?? string.Empty;
            Adres = TempData["Adres"] as string ?? string.Empty;
            Betaalwijze = TempData["Betaalwijze"] as string ?? string.Empty;
            Total = TempData["Total"] as string ?? "0.00";

            var cartJson = TempData["CartJson"] as string;
            if (!string.IsNullOrEmpty(cartJson))
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();

            if (string.IsNullOrEmpty(Naam))
                return RedirectToPage("/Checkout");

            return Page();
        }
    }
}
