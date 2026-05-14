using Block4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        // cart item
        public required List<Cart> CartItems;
        public required float cartShippingCost = getShippingCost();
        public required float cartSubtotal = getCartSubtotal();
        public required string removeImg = setCartItemRemmoveImagePath();
        private static float getShippingCost()
        {
            // default is 0;
            decimal shippingCost = 0;
            throw new NotImplementedException();
        }
        private static float getCartSubtotal()
        {
            throw new NotImplementedException();
        }
        private static string setCartItemRemmoveImagePath()
        {
            throw new NotImplementedException();
        }




        public void OnGet()
        {
            CartItems.Clear();
            LoadCart();
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
                CartItems = JsonSerializer.Deserialize<List<Cart>>(cartJson) ?? new();
        }
    }
}
