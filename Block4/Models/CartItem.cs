using Microsoft.IdentityModel.Tokens;

namespace Block4.Models
{
    public class CartItem(Product product, int quantity, string? img)
    {
        public int Quantity { private set; get; } = quantity;

        public string? ImgPath { set; get; } = img ?? "./ErrorNotFound";

        public required Product Product { set; get; } = product;

        public decimal LineTotal => Product.Price * Quantity;

        public void ChangeQuantity(int newQuantity)
        {
            if (newQuantity >= 0)
            {
                Quantity = newQuantity;
            }
        }
    }
}
