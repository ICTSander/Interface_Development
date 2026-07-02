using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop_DenkTank.Models;
using Webshop_DenkTank.Services;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Webshop_DenkTank.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly ProductService _service;
        private readonly OrderService _orderService;
        private const string SessionKey = "cart";

        public Product Product { get; set; }

        public ProductDetailModel(ProductService service, OrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }

        public IActionResult OnGet(int id)
        {
            Product = _service.GetById(id);
            if (Product == null) return NotFound();
            return Page();
        }

        public IActionResult OnPostAddToCart(int id, int quantity)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            var json = HttpContext.Session.GetString(SessionKey);
            var list = string.IsNullOrEmpty(json) ? new List<CartItemDto>() : JsonSerializer.Deserialize<List<CartItemDto>>(json)!;

            var existing = list.FirstOrDefault(i => i.ProductId == id);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                list.Add(new CartItemDto { ProductId = id, Quantity = quantity });
            }

            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(list));

            // If AJAX request, return JSON with updated count
            if (Request.Headers.TryGetValue("X-Requested-With", out var header) && header == "XMLHttpRequest")
            {
                var total = list.Sum(i => i.Quantity);
                return new JsonResult(new { success = true, count = total });
            }

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostOrder(int id, int quantity)
        {
            var product = _service.GetById(id);
            if (product == null) return NotFound();

            var userEmail = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(userEmail))
            {
                // Create order immediately for logged-in user
                var orderNumber = System.Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                var order = new Order
                {
                    OrderNumber = orderNumber,
                    Date = System.DateTime.UtcNow,
                    Status = "Besteld",
                    UserEmail = userEmail,
                    Items = new List<OrderItem> { new OrderItem { Product = product, Quantity = quantity } },
                    History = new List<OrderHistoryItem>
                    {
                        new OrderHistoryItem { Timestamp = System.DateTime.UtcNow, Status = "Besteld", Note = "Directe aankoop via productpagina" }
                    }
                };

                _orderService.AddOrder(order);

                TempData["OrderNumber"] = orderNumber;
                TempData["OrderTotal"] = order.Total.ToString("F2");
                TempData["OrderName"] = userEmail;

                return RedirectToPage("/OrderConfirmation");
            }

            // Not logged in: add to cart and redirect to checkout to complete order
            var json = HttpContext.Session.GetString(SessionKey);
            var list = string.IsNullOrEmpty(json) ? new List<CartItemDto>() : JsonSerializer.Deserialize<List<CartItemDto>>(json)!;

            var existing = list.FirstOrDefault(i => i.ProductId == id);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                list.Add(new CartItemDto { ProductId = id, Quantity = quantity });
            }

            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(list));

            return RedirectToPage("/Checkout");
        }

        private class CartItemDto { public int ProductId { get; set; } public int Quantity { get; set; } }
    }
}