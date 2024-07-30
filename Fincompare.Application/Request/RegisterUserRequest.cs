using Fincompare.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Fincompare.Application.Request
{
    public class RegisterUserRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)?([0-9]{9}|[0-9\-]{9,18})$", ErrorMessage = "Not a valid phone number")]
        [DefaultValue("1234567890")]
        public string? Phone { get; set; }

        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Password field must not contain any whitespace.")]
        [MinLength(5)]
        [DefaultValue("Password@123")]
        public string Password { get; set; }
        public string? Role { get; set; } = RoleEnum.Customer.ToString();

        [JsonIgnore]
        public int? CreatedBy { get; set; } = null;

        [JsonIgnore]
        public int StatusId { get; set; } = (int)StatusEnum.Enabled;
    }
}
