using System.ComponentModel.DataAnnotations;
using System.Linq;
using logstore.Data;

namespace logstore.Models.Validators
{
    public class EmailUserUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var _context = (DataContext)validationContext.GetService(typeof(DataContext));
            var entity = _context.Users.FirstOrDefault(e => e.Email == value.ToString());

            if (entity != null)
            {
                return new ValidationResult(GetErrorMessage(value.ToString()));
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage(string email)
        {
            return $"The address '{email}' is already taken";
        }
    }
}