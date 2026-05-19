using Block4.Models;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Views.Shared.Components.CartWidget
{
    // is geen page maar view component ( deel van page zoals alle andere pages op _layout
    [ViewComponent(Name = "CartWidget")]
    public class CartWidgetViewComponent(IProductRepository productRepository) : ViewComponent
    {
        // Data Access layer
        private readonly IProductRepository _productRepository = productRepository;

        public IViewComponentResult Invoke()
        {
            List<CartItem>? cartItems = LoadCart() ?? new List<CartItem>();
            var cartSummary = new CartSummaryView
            {
                CartItems = cartItems,
                Subtotal = GetCartSubtotal(cartItems),
                ShippingCost = GetShippingCost(cartItems),
                ItemCount = cartItems.Sum(item => item.Quantity),
                RemoveImg = SetCartItemRemmoveImagePath()
            };

            return View(cartSummary);
        }

        private List<CartItem>? LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();

            }
            return new List<CartItem>();
        }

        private static decimal GetShippingCost(List<CartItem> cartItems)
        {
            // default is 0;
            decimal shippingCost = 0;
            return shippingCost;
        }
        private decimal GetCartSubtotal(List<CartItem> cartItems)
        {
            decimal cartSubtotal = 0;
            foreach (var cart in cartItems)
            {
                int quatity = cart.Quantity;
                decimal price = cart.Product.Price;
                cartSubtotal += price * quatity;
            }

            return cartSubtotal;
        }
        private static string SetCartItemRemmoveImagePath()
        {
            // TODO: edit the path to the image when image exist
            string removeImg = "./notFound";
            return removeImg;
        }
    }
}
