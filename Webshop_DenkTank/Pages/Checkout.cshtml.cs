using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Webshop_DenkTank.Models;
using Webshop_DenkTank.Services;

namespace Webshop_DenkTank.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ProductService _service;
        private readonly OrderService _orderService;
        private const string SessionKey = "cart";

        public List<CartItem> Items { get; set; } = new();
        public decimal Total { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public CheckoutModel(ProductService service, OrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }

        public void OnGet()
        {
            LoadCart();
            CalculateTotal();
        }

        public IActionResult OnPost()
        {
            LoadCart();
            CalculateTotal();

            if (!Items.Any())
            {
                return RedirectToPage("/Cart");
            }

            var orderNumber = System.Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            var userEmail = HttpContext.Session.GetString("user") ?? Email ?? "guest@matrix.inc";

            var order = new Order
            {
                OrderNumber = orderNumber,
                Date = System.DateTime.UtcNow,
                Status = "Besteld",
                UserEmail = userEmail,
                Items = Items.Select(i => new OrderItem { Product = i.Product, Quantity = i.Quantity }).ToList(),
                History = new List<OrderHistoryItem>
                {
                    new OrderHistoryItem { Timestamp = System.DateTime.UtcNow, Status = "Besteld", Note = "Bestelling geplaatst via Checkout" }
                }
            };

            _orderService.AddOrder(order);

            TempData["OrderNumber"] = orderNumber;
            TempData["OrderTotal"] = order.Total.ToString("F2");
            TempData["OrderName"] = Name ?? userEmail;

            // Clear cart
            HttpContext.Session.Remove(SessionKey);

            return RedirectToPage("/OrderConfirmation");
        }

        private void LoadCart()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json))
            {
                Items = new List<CartItem>();
                return;
            }

            var dto = JsonSerializer.Deserialize<List<CartItemDto>>(json);
            Items = dto.Select(d => new CartItem { Product = _service.GetById(d.ProductId), Quantity = d.Quantity })
                       .Where(i => i.Product != null)
                       .ToList();
        }

        private void CalculateTotal()
        {
            Total = Items.Sum(i => i.Product.Price * i.Quantity);
        }

        public class CartItem { public Product Product { get; set; } public int Quantity { get; set; } }
        private class CartItemDto { public int ProductId { get; set; } public int Quantity { get; set; } }
    }
}
