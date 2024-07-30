using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fincompare.Application.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Password field must not contain any whitespace.")]
        [MinLength(5)]
        [DefaultValue("Password@123")]
        public string CurrentPassword { get; set; }

        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Password field must not contain any whitespace.")]
        [MinLength(5)]
        [DefaultValue("NewPassword@123")]
        public string NewPassword { get; set; }

        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Password field must not contain any whitespace.")]
        [MinLength(5)]
        [DefaultValue("NewPassword@123")]
        public string ConfirmNewPassword { get; set; }
    }
}
