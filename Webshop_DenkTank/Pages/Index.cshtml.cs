using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop_DenkTank.Models;
using Webshop_DenkTank.Services;

namespace Webshop_DenkTank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _service;

        public List<Product> FeaturedProducts { get; set; }

        public IndexModel(ProductService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            FeaturedProducts = _service.GetAllProducts()
                                       .Take(4)
                                       .ToList();
        }
    }
}