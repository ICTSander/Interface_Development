using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webshop_DenkTank.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        public string OrderNumber { get; set; }
        public string OrderTotal { get; set; }
        public string OrderName { get; set; }

        public IActionResult OnGet()
        {
            if (TempData.ContainsKey("OrderNumber"))
            {
                OrderNumber = TempData["OrderNumber"] as string;
                OrderTotal = TempData["OrderTotal"] as string;
                OrderName = TempData["OrderName"] as string;
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
