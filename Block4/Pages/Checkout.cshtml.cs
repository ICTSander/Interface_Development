using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Block4.Models;
using Block4.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Block4.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ProductService _productService;

        public CheckoutModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public CheckoutInput Input { get; set; } = new();

        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(i => i.Price * i.Quantity);

        public IActionResult OnGet()
        {
            LoadCart();

            if (CartItems.Count == 0)
            {
                CartItems = new List<CartItem>
                {
                    new CartItem { ProductId = 1, ProductName = "Classic White T-Shirt", Price = 29.95m, Quantity = 2 },
                    new CartItem { ProductId = 3, ProductName = "Linen Summer Dress", Price = 89.95m, Quantity = 1 }
                };
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(CartItems));
            }

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

    public class CheckoutInput
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Volledige naam")]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres is verplicht")]
        [Display(Name = "Bezorgadres")]
        public string Adres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kies een betaalwijze")]
        [Display(Name = "Betaalwijze")]
        public string Betaalwijze { get; set; } = string.Empty;
    }
}
