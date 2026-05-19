using Microsoft.IdentityModel.Tokens;

namespace Block4.Models
{
    public class CartItem(Product product, int _quantity, string? img)
    {
        public int Quantity { private set; get; } = _quantity;

        public string? ImgPath { set; get; } = img ?? "./ErrorNotFound";

        public required Product Product { set; get; } = product;

        public void ChangeQuantity(int newQuantity)
        {
            if (newQuantity >= 0)
            {
                Quantity = newQuantity;
            }
        }
    }
}
