namespace Block4.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public required Product Product { get; set; }
    }
}
