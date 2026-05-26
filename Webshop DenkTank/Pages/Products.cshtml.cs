using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop_DenkTank.Models;
using Webshop_DenkTank.Services;

namespace Webshop_DenkTank.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ProductService _service;

        public List<Product> Products { get; set; }

        public ProductsModel(ProductService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            Products = _service.GetAllProducts();
        }
    }
}