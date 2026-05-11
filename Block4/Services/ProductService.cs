using Block4.Models;

namespace Block4.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Classic White T-Shirt", Description = "Tijdloos wit katoenen t-shirt", Price = 29.95m, Category = "Heren", ImageUrl = "/images/products/tshirt-white.jpg" },
            new Product { Id = 2, Name = "Slim Fit Jeans", Description = "Donkerblauwe slim fit denim", Price = 79.95m, Category = "Heren", ImageUrl = "/images/products/jeans-slim.jpg" },
            new Product { Id = 3, Name = "Linen Summer Dress", Description = "Luchtige linnen jurk voor de zomer", Price = 89.95m, Category = "Dames", ImageUrl = "/images/products/dress-linen.jpg" },
            new Product { Id = 4, Name = "Wool Blend Blazer", Description = "Wollen blazer met moderne pasvorm", Price = 149.95m, Category = "Heren", ImageUrl = "/images/products/blazer-wool.jpg" },
            new Product { Id = 5, Name = "Canvas Sneakers", Description = "Witte canvas sneakers", Price = 59.95m, Category = "Dames", ImageUrl = "/images/products/sneakers-canvas.jpg" },
            new Product { Id = 6, Name = "Striped Kids Polo", Description = "Gestreept poloshirt voor kinderen", Price = 24.95m, Category = "Kinderen", ImageUrl = "/images/products/polo-kids.jpg" },
        };

        public List<Product> GetAll() => _products;

        public List<Product> GetByCategory(string category) =>
            _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
    }
}
