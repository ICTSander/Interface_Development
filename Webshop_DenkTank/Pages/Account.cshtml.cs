using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Webshop_DenkTank.Services;
using Webshop_DenkTank.Models;
using System.Collections.Generic;

namespace Webshop_DenkTank.Pages
{
    public class AccountModel : PageModel
    {
        private readonly OrderService _orderService;

        public string UserEmail { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();

        public AccountModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult OnGet()
        {
            var user = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(user))
            {
                // not logged in
                return Page();
            }

            UserEmail = user;
            Orders = _orderService.GetOrdersForUser(user);
            return Page();
        }
    }
}
