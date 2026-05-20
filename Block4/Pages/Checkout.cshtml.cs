using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Block4.Models;
using System.Text.Json;

namespace Block4.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public CheckoutInput Input { get; set; } = new();

        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(i => i.Price * i.Quantity);

        public IActionResult OnGet()
        {
            LoadCart();
            return Page();
        }

        public IActionResult OnPost()
        {
            LoadCart();

            if (!ModelState.IsValid)
                return Page();

            TempData["Naam"] = Input.Naam;
            TempData["Adres"] = Input.Adres;
            TempData["Betaalwijze"] = Input.Betaalwijze;
            TempData["CartJson"] = JsonSerializer.Serialize(CartItems);
            TempData["Total"] = Total.ToString("F2");

            HttpContext.Session.Remove("Cart");

            return RedirectToPage("/Confirmation");
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
        }
    }
}
