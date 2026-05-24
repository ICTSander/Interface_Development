using System.ComponentModel.DataAnnotations;

namespace Block4.Models
{
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
