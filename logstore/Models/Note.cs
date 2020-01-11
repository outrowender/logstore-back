using System.ComponentModel.DataAnnotations;

namespace logstore.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field user id is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The field title is required")]
        [MinLength(5, ErrorMessage = "The field title shoud be greater than 5 characters")]
        [MaxLength(50, ErrorMessage = "The field title shoud be smaller than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field detail is required")]
        [MinLength(5, ErrorMessage = "The field detail shoud be greater than 5 characters")]
        [MaxLength(256, ErrorMessage = "The field detail shoud be smaller than 50 characters")]
        public string Detail { get; set; }
        public User User { get; set; }

    }

}