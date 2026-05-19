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





        // cart
        [BindProperty]
        public required string CartItemCartSubtotal { get; set; }
        [BindProperty]
        public required string CarItemShippingCost { get; set; }

        public required IList<Product> products;
        public required List<CartItem> CartItems;
        public required decimal ShippingCost;
        public required decimal CartSubtotal;
        public required string removeImg = setCartItemRemmoveImagePath();
        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            products = new List<Product>();
            // cart - items
            CartItems = new List<CartItem>();
        }

        public void OnGet()
        {
            products = _productRepository.GetAllProducts().ToList();

            // cart
            LoadCart();
            ShippingCost = getShippingCost();
            CartSubtotal = getCartSubtotal();
        }

        public IActionResult? OnPost()
        {
            if (!ModelState.IsValid)
            {
                // ga terug zonder iets te sturen
                return BadRequest(ModelState);
                
            }
            // Load cart from session
            LoadCart();

            // Create order data with all cart information
            var orderData = new
            {
                Subtotal = CartSubtotal,
                ShippingCost = ShippingCost,
                Total = CartSubtotal + ShippingCost,
                Items = CartItems.Select(item => new
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    ImagePath = item.ImgPath,
                    LineTotal = item.Product.Price * item.Quantity
                }).ToList()
            };

            // Save to session or pass to checkout
            HttpContext.Session.SetString("OrderData", JsonSerializer.Serialize(orderData));

            // Log for debugging
            _logger.LogInformation($"Order created: {orderData.Items.Count} items, Total: €{orderData.Total}");

            // Redirect to checkout page
            return RedirectToPage("/Checkout");
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }
        }
        // helpers
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
    }
}
