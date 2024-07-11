using Fincompare.Application.Models;
using Fincompare.Application.Request;

namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IUserManagerServices
    {
        Task<Response<string>> ChangeExistingPassword(ChangePasswordRequest request);
    }
}
