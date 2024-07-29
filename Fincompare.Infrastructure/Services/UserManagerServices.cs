using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Models;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Domain.Entities.UserManagementEntities;

namespace Fincompare.Infrastructure.Services
{
    public class UserManagerServices : IUserManagerServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ICryptographyService _cryptographyService;

        public UserManagerServices(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService,
                                    ICryptographyService cryptographyService)
        {
            _unitOfWork = unitOfWork;
            _authenticatedUserService = authenticatedUserService;
            _cryptographyService = cryptographyService;
        }
        public async Task<Response<string>> ChangeExistingPassword(ChangePasswordRequest request)
        {
            var user = await _authenticatedUserService.GetLoggedInUser();
            if (user == null)
            {
                throw new ApplicationException("User not exist");
            }
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new ApplicationException("The new password and confirmation password do not match.");
            }
            var isCurrenPasswordValid = CheckPasswordForUser(user, request.CurrentPassword);

            if (!isCurrenPasswordValid)
            {
                throw new ApplicationException("Invalid Current Password");
            }

            user.PasswordHash = _cryptographyService.EncryptPassword(request.NewPassword);
            await _unitOfWork.GetRepository<User>().Upsert(user);
            await _unitOfWork.SaveChangesAsync();
            var response = new Response<string>
            {
                Status = true,
                Message = "Password Changed Successfully",
                Data = null
            };

            return response;
        }

        public async Task<Response<User>> GetUserById(int id)
        {
            var response = new Response<User>();
            var user = await _unitOfWork.GetRepository<User>().GetById(id);

            if (user == null)
            {
                response.Status = false;
                response.Message = "User Not Found";
                return response;
            }

            response.Status = true;
            response.Message = "User found";
            response.Data = user;
            return response;
        }

        private bool CheckPasswordForUser(User user, string password)
        {
            var bytePassword = _cryptographyService.EncryptPassword(password);

            var isValidPassword = _cryptographyService.ValidatePassword(bytePassword, user.PasswordHash);

            return isValidPassword;
        }
    }
}
