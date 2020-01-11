using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using logstore.Models.Validators;

namespace logstore.Models.ViewModels
{
    public class UserViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field name is required")]
        [MinLength(2, ErrorMessage = "The field name shoud be greater than 2 characters")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "The field email shoud be a valid mail adress")]
        [EmailUserUnique]
        public string Email { get; set; }
        public List<Note> Notes { get; set; }
        [Required(ErrorMessage = "The password is required")]
        [MinLength(6, ErrorMessage = "The password shoud be greater than 6 characters")]
        [MaxLength(20, ErrorMessage = "The password shoud be smaller than 20 characters")]
        public string Password { get; set; }

    }

}