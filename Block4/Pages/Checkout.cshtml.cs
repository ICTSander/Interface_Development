using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Block4.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public CheckoutInput Input { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            TempData["Naam"] = Input.Naam;
            TempData["Adres"] = Input.Adres;
            TempData["Betaalwijze"] = Input.Betaalwijze;

            return RedirectToPage("/Confirmation");
        }
    }

    public class CheckoutInput
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Volledige naam")]
        public string Naam { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres is verplicht")]
        [Display(Name = "Bezorgadres")]
        public string Adres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kies een betaalwijze")]
        [Display(Name = "Betaalwijze")]
        public string Betaalwijze { get; set; } = string.Empty;
    }
}
