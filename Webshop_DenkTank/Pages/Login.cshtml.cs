using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webshop_DenkTank.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public bool RememberMe { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Tijdelijke demo-login — vervangen door echte authenticatie
            if (Email == "demo@denktank.nl" && Password == "Demo1234!")
            {
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Ongeldig e-mailadres of wachtwoord. Probeer het opnieuw.";
            return Page();
        }
    }
}
