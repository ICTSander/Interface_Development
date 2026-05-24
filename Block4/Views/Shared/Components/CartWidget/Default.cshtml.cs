using Block4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Block4.Views.Shared.Components.CartWidget
{
    [ViewComponent(Name = "CartWidget")]
    public class CartWidgetViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cartItems = LoadCart();
            var cartSummary = new CartSummaryView
            {
                CartItems = cartItems,
                Subtotal = cartItems.Sum(item => item.Price * item.Quantity),
                ShippingCost = 0,
                ItemCount = cartItems.Sum(item => item.Quantity),
                RemoveImg = "/img/remove.png"
            };

            return View(cartSummary);
        }

        private List<CartItem> LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
                return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
            return new List<CartItem>();
        }
    }
}
