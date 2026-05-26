using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Block4.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository _productRepository;

        public required Product Product;

        public ProductDetailModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            Product = new Product();
        }

        public void OnGet(int id)
        {
            Product = _productRepository.GetProductById(id);
        }
    }
}
