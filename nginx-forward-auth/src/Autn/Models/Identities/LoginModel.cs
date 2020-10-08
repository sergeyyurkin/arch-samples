using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Identities
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
