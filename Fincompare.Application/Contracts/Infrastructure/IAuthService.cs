using Fincompare.Application.Models;
using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IAuthService
    {
        Task<Response<AuthResponse>> Login(AuthRequest request);

        Task<Response<User>> Register(RegisterUserRequest request);

        Task<string> GenerateToken(User user);

        Task<bool> IsTokenValid(string jwtToken);
    }
}
