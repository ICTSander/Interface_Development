namespace Block4.Models
{
    public class CartSummaryView
    {
        public List<CartItem> CartItems { get; set; } = [];
        public decimal Subtotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total => Subtotal + ShippingCost;
        public int ItemCount { get; set; }
        public string RemoveImg { get; set; } = string.Empty;

    }
}