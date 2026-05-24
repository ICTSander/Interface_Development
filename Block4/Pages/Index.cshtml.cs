using Block4.Models;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class IndexModel(IProductRepository productRepository) : PageModel
    {
        private readonly IProductRepository _productRepository = productRepository;

        public IList<Product> Products { get; set; } = new List<Product>();

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts().ToList();
        }

        public IActionResult OnPostAddToCart(int productId, string productName, decimal price, string imageUrl)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existing != null)
                existing.Quantity++;
            else
                cart.Add(new CartItem { ProductId = productId, ProductName = productName, Price = price, Quantity = 1, ImageUrl = imageUrl });

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            return RedirectToPage();
        }
    }
}
