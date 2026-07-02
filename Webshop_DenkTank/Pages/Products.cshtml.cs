using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop_DenkTank.Models;
using Webshop_DenkTank.Services;

namespace Webshop_DenkTank.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ProductService _service;

        public List<Product> Products { get; set; }
        public List<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

        public ProductsModel(ProductService service)
        {
            _service = service;
        }

        public void OnGet(string cat)
        {
            var all = _service.GetAllProducts();
            Categories = all.Select(p => p.Category).Where(c => !string.IsNullOrEmpty(c)).Distinct().ToList();
            SelectedCategory = cat;

            if (!string.IsNullOrEmpty(cat))
            {
                Products = all.Where(p => !string.IsNullOrEmpty(p.Category) &&
                                           p.Category.Equals(cat, StringComparison.OrdinalIgnoreCase))
                              .ToList();
            }
            else
            {
                Products = all;
            }
        }
    }
}