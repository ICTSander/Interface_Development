using Block4.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        // Data Access layer
        private readonly IProductRepository _productRepository;

        



        // cart item

        public required IList<Product> products;

        public required List<Cart> CartItems;
        public required decimal cartShippingCost = getShippingCost();
        public required decimal cartSubtotal;
        public required string removeImg = setCartItemRemmoveImagePath();
        private static decimal getShippingCost()
        {
            // default is 0;
            decimal shippingCost = 0;
            return shippingCost;
        }
        private decimal getCartSubtotal()
        {
            decimal cartSubtotal = 0;
            foreach (var cart in CartItems)
            {
                int quatity = cart.Quantity;
                decimal price = cart.Product.Price;
                cartSubtotal += price * quatity;
            }

            return cartSubtotal;
        }
        private static string setCartItemRemmoveImagePath()
        {
            // TODO: edit the path to the image when image exist
            string removeImg = "./notFound";
            return removeImg;
        }


        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            products = new List<Product>();
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            products = _productRepository.GetAllProducts().ToList();
            CartItems.Clear();
            LoadCart();
            cartSubtotal = getCartSubtotal();
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
                CartItems = JsonSerializer.Deserialize<List<Cart>>(cartJson) ?? new();
        }
    }
}
