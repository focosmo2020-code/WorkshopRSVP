using System.ComponentModel.DataAnnotations;
namespace WorkshopRSVP.Models 
{
    public class RSVP
    {
        [Required(ErrorMessage = "Enter your full name.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "I need accommodation")]
        public bool NeedsAccommodation { get; set; }

        [Required(ErrorMessage = "Choose a workshop.")]
        [Display(Name = "Workshop")]
        public string WorkshopName { get; set; } = string.Empty;
    }
}
