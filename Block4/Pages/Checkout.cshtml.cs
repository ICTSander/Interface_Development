using Block4.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Block4.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        [BindProperty]
        public CheckoutInput Input { get; set; } = new();

        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(i => i.Price * i.Quantity);

        public Customer Customer { get; set; } = new();
        public Order Order { get; set; } = new();

        public CheckoutModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult OnGet()
        {
            LoadCart();
            return Page();
        }

        public IActionResult OnPost()
        {
            LoadCart();

            if (!ModelState.IsValid)
                return Page();

            TempData["Naam"] = Input.Naam;
            TempData["Adres"] = Input.Adres;
            TempData["Betaalwijze"] = Input.Betaalwijze;
            TempData["CartJson"] = JsonSerializer.Serialize(CartItems);
            TempData["Total"] = Total.ToString("F2");

            CreateOrder();

            HttpContext.Session.Remove("Cart");

            return RedirectToPage("/Confirmation");
        }

        private void LoadCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
                CartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new();
        }

        private void CreateOrder()
        {
            Customer = new Customer
            {
                Name = Input.Naam,
                Address = Input.Adres,
                Active = true
            };
            Order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = Customer.Id,
                Customer = Customer,
            };
            _orderRepository.AddOrder(Order);
        }
    }
}
