using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop_DenkTank.Services;
using Webshop_DenkTank.Models;

namespace Webshop_DenkTank.Pages
{
    public class OrderDetailsModel : PageModel
    {
        private readonly OrderService _orderService;

        public Order? Order { get; set; }

        public OrderDetailsModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult OnGet(string number)
        {
            if (string.IsNullOrEmpty(number)) return RedirectToPage("/Account");
            Order = _orderService.GetOrder(number);
            if (Order == null) return NotFound();
            return Page();
        }
    }
}
