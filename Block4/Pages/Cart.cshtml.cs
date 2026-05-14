using Block4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class CartModel : PageModel
    {
        public required List<Cart> CartItem;

        public void OnGet()
        {
            CartItem.Clear();
            LoadCart();
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
                CartItem = JsonSerializer.Deserialize<List<Cart>>(cartJson) ?? new();
        }
    }
}
