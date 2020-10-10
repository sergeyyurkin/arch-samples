using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserRegisterInput
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
