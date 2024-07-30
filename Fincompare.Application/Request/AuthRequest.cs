using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request
{
    public class AuthRequest
    {
        [Required]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Password field must not contain any whitespace.")]
        [MinLength(5)]
        [DefaultValue("Password@123")]
        public string Password { get; set; }
    }
}
