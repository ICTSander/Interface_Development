namespace Block4.Models
{
    public class Cart
    {
        public int Quantity { get; set; }

        public string imgPath {  get; set; }

        public required Product Product { get; set; }
    }
}
