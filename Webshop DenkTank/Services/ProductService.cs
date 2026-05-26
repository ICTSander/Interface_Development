using Webshop_DenkTank.Models;

namespace Webshop_DenkTank.Services
{
    public class ProductService
    {
        private List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Zwarte Hoodie", ImageUrl = "/images/zwarte hoodie.jpg", Price = 49.99m, Stock = 5 },
        new Product { Id = 2, Name = "Witte Sneakers", ImageUrl = "/images/witte sneakers.jpg", Price = 89.99m, Stock = 2 },
        new Product { Id = 3, Name = "Denim Jacket", ImageUrl = "/images/denim jacket.jpg", Price = 119.99m, Stock = 0 },
        new Product { Id = 4, Name = "Basic T‑Shirt", ImageUrl = "/images/basic t-shirt.jpg", Price = 19.99m, Stock = 12 }
    };

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}
