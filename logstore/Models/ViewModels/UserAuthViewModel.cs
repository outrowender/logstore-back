using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using logstore.Models.Validators;

namespace logstore.Models.ViewModels
{
    public class UserAuthViewModel
    {
        [Required(ErrorMessage = "The field email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field password is required")]
        public string Password { get; set; }

    }

}