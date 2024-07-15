using Fincompare.Application.Models;
using Fincompare.Application.Request;
using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IUserManagerServices
    {
        Task<Response<string>> ChangeExistingPassword(ChangePasswordRequest request);
        Task<Response<User>> GetUserById(int id);
    }
}
