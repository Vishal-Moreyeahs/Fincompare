using Fincompare.Domain.Enums;
using System.ComponentModel.DataAnnotations;

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
        public string Role { get; set; } = RoleEnum.Customer.ToString();
        public int? CreatedBy { get; set; } = null;
        public int? StatusId { get; set; } = (int)StatusEnum.Enabled;
    }
}
