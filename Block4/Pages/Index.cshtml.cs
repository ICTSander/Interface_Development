using Block4.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class IndexModel(IProductRepository productRepository) : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IProductRepository _productRepository = productRepository;

        public required IList<Product> products;

        public void OnGet()
        {
            products = new List<Product>();
            // Test: Add a dummy cart item to session
            // List<CartItem> testCart = new();

            // Test: Add a dummy cart item to session
            var testCart = new List<CartItem>
                {
                    new CartItem
                    {
                        Product = products[0],
                        Quantity = 2,
                        ImgPath = "/images/test.jpg"
                    }
                };

            var cartJson = JsonSerializer.Serialize(testCart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}
