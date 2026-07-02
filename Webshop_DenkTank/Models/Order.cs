using System;
using System.Collections.Generic;
using System.Linq;

namespace Webshop_DenkTank.Models
{
    public class Order
    {
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty; // e.g. Besteld, Wordt verwerkt, Onderweg
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public string UserEmail { get; set; } = string.Empty;

        public List<OrderHistoryItem> History { get; set; } = new List<OrderHistoryItem>();

        public decimal Total => Items?.Sum(i => (i.Product?.Price ?? 0m) * i.Quantity) ?? 0m;
    }

    public class OrderItem
    {
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class OrderHistoryItem
    {
        public DateTime Timestamp { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
