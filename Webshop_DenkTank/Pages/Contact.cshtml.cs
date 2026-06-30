using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webshop_DenkTank.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string Naam { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Onderwerp { get; set; }

        [BindProperty]
        public string Bericht { get; set; }

        public bool Verzonden { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Hier zou je het bericht kunnen opslaan of mailen
            // Voor nu tonen we alleen de bevestiging
            Verzonden = true;
            return Page();
        }
    }
}
