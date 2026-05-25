using Block4.Models;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public required Product Product;

        public ProductDetailModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            Product = new Product();
        }

        public void OnGet(int id)
        {
            Product = _productRepository.GetProductById(id);
        }

        public IActionResult OnPostAddToCart(int productId, string productName, decimal price, string category, string imageUrl)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existing != null)
                existing.Quantity++;
            else
                cart.Add(new CartItem { ProductId = productId, ProductName = productName, Price = price, Quantity = 1, Category = category, ImageUrl = imageUrl });

            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            return RedirectToPage(new { id = productId });
        }
    }
}