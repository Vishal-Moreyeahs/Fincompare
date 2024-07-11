using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Application.Repositories
{
    public interface IAuthenticatedUserService
    {
        Task<User> GetLoggedInUser();

    }
}
