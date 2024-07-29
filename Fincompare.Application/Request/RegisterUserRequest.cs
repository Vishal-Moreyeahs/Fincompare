using Fincompare.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fincompare.Application.Request
{
    public class RegisterUserRequest
    {
        [Required]
        public string Email { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; } = RoleEnum.Customer.ToString();

        [JsonIgnore]
        public int? CreatedBy { get; set; } = null;

        [JsonIgnore]
        public int StatusId { get; set; } = (int)StatusEnum.Enabled;
    }
}
