using Webshop_DenkTank.Models;
using System.Collections.Generic;
using System.Linq;

namespace Webshop_DenkTank.Services
{
    public class ProductService
    {
        private List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Schroeven - 100 st.", ImageUrl = "/images/schroeven.svg", Price = 4.99m, Stock = 50, Category = "Schroeven", Description = "Universele metaal- en houtschroeven, 100 stuks per verpakking." },
        new Product { Id = 2, Name = "Bout en Moer - set", ImageUrl = "/images/bout_moer.svg", Price = 6.49m, Stock = 30, Category = "Bevestiging", Description = "Sterke bout- en moerset, geschikt voor diverse bevestigingen." },
        new Product { Id = 3, Name = "Pluggen - 50 st.", ImageUrl = "/images/pluggen.svg", Price = 3.99m, Stock = 80, Category = "Bevestiging", Description = "Universele pluggen voor muurbevestiging, 50 stuks." },
        new Product { Id = 4, Name = "Houtschroeven - 200 st.", ImageUrl = "/images/houtschroeven.svg", Price = 9.99m, Stock = 40, Category = "Schroeven", Description = "Houtschroeven met diep profiel, 200 stuks voor grotere klussen." },
        new Product { Id = 5, Name = "Kruiskop Schroevendraaier", ImageUrl = "/images/schroevendraaier.svg", Price = 7.49m, Stock = 25, Category = "Gereedschap", Description = "Ergonomische schroevendraaier met stevige grip." },
        new Product { Id = 6, Name = "Hamer - 16 oz", ImageUrl = "/images/hamer.svg", Price = 14.99m, Stock = 15, Category = "Gereedschap", Description = "Klassieke hamerslag, 16 oz hoofd voor precisiewerk." },
        new Product { Id = 7, Name = "Zaag - Universeel", ImageUrl = "/images/zaag.svg", Price = 24.99m, Stock = 10, Category = "Gereedschap", Description = "Universele handzaag, geschikt voor hout en lichte metalen." },
        new Product { Id = 8, Name = "Beitel Set - 3 st.", ImageUrl = "/images/beitel.svg", Price = 19.99m, Stock = 8, Category = "Gereedschap", Description = "Professionele beitel set voor houtbewerking, 3 maten." },
        new Product { Id = 9, Name = "Meetlint 5m", ImageUrl = "/images/meetlint.svg", Price = 5.99m, Stock = 60, Category = "Meetgereedschap", Description = "Flexibel meetlint van 5 meter met duidelijke markeringen." },
        new Product { Id = 10, Name = "Schuurpapier Set", ImageUrl = "/images/schuurpapier.svg", Price = 4.49m, Stock = 100, Category = "Schuurmateriaal", Description = "Set schuurpapier in diverse korrels voor afwerking." }
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
