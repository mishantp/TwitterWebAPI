using System.ComponentModel.DataAnnotations;

namespace TwitterWebAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public string ContactNumber { get; set; }
    }
}
