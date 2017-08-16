using System.ComponentModel.DataAnnotations;

namespace SimpleVoter.Core.ViewModels.AccountViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}