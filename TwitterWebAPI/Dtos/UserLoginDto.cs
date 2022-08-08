using System.ComponentModel.DataAnnotations;

namespace TwitterWebAPI.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Enter username")]
        public string LoginId { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
    }
}
