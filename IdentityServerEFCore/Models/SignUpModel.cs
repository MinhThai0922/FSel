using System.ComponentModel.DataAnnotations;

namespace IdentityServerEFCore.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        public bool view_customer { get; set; }
        [Required]
        public bool create_customer { get; set; }
        [Required]
        public bool update_customer { get; set; }
        [Required]
        public bool delete_customer { get; set; }
    }
}
