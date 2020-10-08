using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Models.Identities
{
    public class RegisterModel
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
