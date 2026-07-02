using System;
using System.Collections.Generic;
using System.Linq;
using Webshop_DenkTank.Models;

namespace Webshop_DenkTank.Services
{
    public class OrderService
    {
        private readonly ProductService _productService;
        private readonly List<Order> _orders = new();
        private readonly object _lock = new();

        public OrderService(ProductService productService)
        {
            _productService = productService;
            SeedOrders();
        }

        private void SeedOrders()
        {
            var products = _productService.GetAllProducts();
            if (products == null || !products.Any()) return;

            _orders.Add(new Order
            {
                OrderNumber = "ORD-2026-001",
                Date = DateTime.UtcNow.AddDays(-7),
                Status = "Besteld",
                UserEmail = "demo@denktank.nl",
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = products.FirstOrDefault(p => p.Id == 1)!, Quantity = 2 },
                    new OrderItem { Product = products.FirstOrDefault(p => p.Id == 5)!, Quantity = 1 }
                },
                History = new List<OrderHistoryItem>
                {
                    new OrderHistoryItem { Timestamp = DateTime.UtcNow.AddDays(-7), Status = "Besteld", Note = "Bestelling ontvangen" },
                    new OrderHistoryItem { Timestamp = DateTime.UtcNow.AddDays(-6), Status = "Wordt verwerkt", Note = "Pakket inpakken" }
                }
            });

            _orders.Add(new Order
            {
                OrderNumber = "ORD-2026-002",
                Date = DateTime.UtcNow.AddDays(-3),
                Status = "Wordt verwerkt",
                UserEmail = "demo@denktank.nl",
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = products.FirstOrDefault(p => p.Id == 3)!, Quantity = 4 }
                },
                History = new List<OrderHistoryItem>
                {
                    new OrderHistoryItem { Timestamp = DateTime.UtcNow.AddDays(-3), Status = "Besteld", Note = "Betaling ontvangen" },
                    new OrderHistoryItem { Timestamp = DateTime.UtcNow.AddDays(-2), Status = "Wordt verwerkt", Note = "Producten verzamelen" }
                }
            });

            _orders.Add(new Order
            {
                OrderNumber = "ORD-2026-003",
                Date = DateTime.UtcNow.AddDays(-1),
                Status = "Onderweg",
                UserEmail = "demo@denktank.nl",
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = products.FirstOrDefault(p => p.Id == 7)!, Quantity = 1 },
                    new OrderItem { Product = products.FirstOrDefault(p => p.Id == 9)!, Quantity = 2 }
                },
                History = new List<OrderHistoryItem>
                {
                    new OrderHistoryItem { Timestamp = DateTime.UtcNow.AddDays(-1), Status = "Besteld", Note = "Bestelling aangemaakt" },
                    new OrderHistoryItem { Timestamp = DateTime.UtcNow, Status = "Onderweg", Note = "Pakket door bezorger opgehaald" }
                }
            });
        }

        public List<Order> GetOrdersForUser(string userEmail)
        {
            return _orders.Where(o => string.Equals(o.UserEmail, userEmail, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Order? GetOrder(string orderNumber)
        {
            return _orders.FirstOrDefault(o => string.Equals(o.OrderNumber, orderNumber, StringComparison.OrdinalIgnoreCase));
        }

        public void AddOrder(Order order)
        {
            if (order == null) return;
            if (string.IsNullOrWhiteSpace(order.OrderNumber))
            {
                order.OrderNumber = "ORD-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
            }

            lock (_lock)
            {
                _orders.Add(order);
            }
        }
    }
}
