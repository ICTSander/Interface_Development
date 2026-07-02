using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Webshop_DenkTank.Models;
using Webshop_DenkTank.Services;

namespace Webshop_DenkTank.Pages
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class CartModel : PageModel
    {
        private readonly ProductService _service;
        private const string SessionKey = "cart";

        public List<CartItem> Items { get; set; } = new();
        public decimal Total { get; set; }

        public CartModel(ProductService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            LoadCart();
            CalculateTotal();
        }

        public IActionResult OnPostRemove(int productId)
        {
            LoadCart();
            var existing = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (existing != null)
            {
                Items.Remove(existing);
                SaveCart();
            }

            // If AJAX, return new totals
            if (Request.Headers.TryGetValue("X-Requested-With", out var header) && header == "XMLHttpRequest")
            {
                var totalCount = Items.Sum(i => i.Quantity);
                var totalPrice = Items.Sum(i => i.Product.Price * i.Quantity);
                return new JsonResult(new { success = true, count = totalCount, total = totalPrice });
            }

            return RedirectToPage();
        }

        public IActionResult OnPostCheckout()
        {
            // Placeholder: implement checkout flow
            return RedirectToPage("/Checkout");
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

        private void SaveCart()
        {
            var dto = Items.Select(i => new CartItemDto { ProductId = i.Product.Id, Quantity = i.Quantity }).ToList();
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(dto));
        }

        private void CalculateTotal()
        {
            Total = Items.Sum(i => i.Product.Price * i.Quantity);
        }

        private class CartItemDto { public int ProductId { get; set; } public int Quantity { get; set; } }
    }
}
