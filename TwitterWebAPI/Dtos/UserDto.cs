using System.ComponentModel.DataAnnotations;
using TwitterWebAPI.Utilities;

namespace TwitterWebAPI.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage="Enter first name")]
        [MaxLength(50, ErrorMessage="First name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="First name can only have letters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage="Enter last name")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only have letters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage ="Enter valid email address")]
        [UniqueEmailUserName]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter username")]
        [UniqueEmailUserName]
        public string LoginId { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter confirm password")]
        [Compare("Password", ErrorMessage ="Passwords didn't match. Try again.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Enter contact number")]
        [StringLength(10)]
        [Phone(ErrorMessage = "Enter contact number")]
        public string ContactNumber { get; set; }
    }
}
