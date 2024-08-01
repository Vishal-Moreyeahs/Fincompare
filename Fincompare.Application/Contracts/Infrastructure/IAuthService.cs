using Fincompare.Application.Models;
using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities.UserManagementEntities;
using static Fincompare.Application.Response.UserResponse.UserResponseViewClass;

namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Task<Response<AuthResponse>> Login(AuthRequest request);

        Task<ApiResponse<CreateUserResponseClass>> Register(RegisterUserRequest request);

        Task<string> GenerateToken(User user);
        Task<bool> IsTokenValid(string jwtToken);
    }
}
