using AutoMapper;
using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Models;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities.UserManagementEntities;
using Fincompare.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Fincompare.Application.Response.UserResponse.UserResponseViewClass;

namespace Fincompare.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;
        private readonly ICryptographyService _cryptographyService;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings, IMapper mapper,
                            ICryptographyService cryptographyService, IAuthenticatedUserService authenticatedUserService)
        {
            _cryptographyService = cryptographyService;
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettings.Value;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
        }

        public async Task<string> GenerateToken(User user)
        {
            var dbUserRole = await _unitOfWork.GetRepository<UserRole>().GetAll();
            var userRole = dbUserRole.Where(x => x.UserId == user.Id).FirstOrDefault();
            var roleName = await _unitOfWork.GetRepository<Role>().GetById(userRole.RoleId);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, string.Concat(user.FirstName," ",user.LastName)),
                new Claim("RoleId", userRole.RoleId.ToString()),
                new Claim(ClaimTypes.Role, roleName.RoleName)
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public async Task<bool> IsTokenValid(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new ArgumentNullException(nameof(jwtToken), "JWT token cannot be null or empty.");
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new ArgumentException("Invalid JWT token.");
            }

            var now = DateTime.UtcNow;

            return now < jsonToken.ValidTo;
        }

        public async Task<Response<AuthResponse>> Login(AuthRequest request)
        {
            var user = _unitOfWork.GetRepository<User>().GetAll().Result.Where(x => x.Email == request.Email).FirstOrDefault();


            if (user == null)
            {
                throw new ApplicationException($"User with {request.Email} not found.");
            }

            var userEncryptedPassword = _cryptographyService.EncryptPassword(request.Password);

            var isValid = _cryptographyService.ValidatePassword(user.PasswordHash, userEncryptedPassword);

            if (!isValid)
            {
                throw new ApplicationException($"Password for '{request.Email} is't valid.");
            }

            var jwtSecurityToken = await GenerateToken(user);

            var authResponse = new AuthResponse
            {
                Token = jwtSecurityToken,
                Email = user.Email
            };
            var response = new Response<AuthResponse>
            {
                Status = true,
                Message = "Login Successfully",
                Data = authResponse
            };

            return response;
        }

        public async Task<ApiResponse<CreateUserResponseClass>> Register(RegisterUserRequest request)
        {
            var users = await _unitOfWork.GetRepository<User>().GetAll();
            var existingUser = users.Where(x => x.Email.Trim() == request.Email.Trim()).ToList();
            

            if (existingUser.Count > 0)
            {
                //return null;
                throw new ApplicationException($"User '{request.Email}' already exists.");
            }

            if (users.Any(x => x.Phone.Trim() == request.Phone.Trim()))
            {
                //return null;
                throw new ApplicationException($"phone number {request.Phone.Trim()} already exists.");
            }
            var checkUserRole = await _unitOfWork.GetRepository<Role>().GetAll();
            if (!checkUserRole.ToList().Any(x => x.RoleName.Equals(request.Role)))
            {
                return new ApiResponse<CreateUserResponseClass>()
                {
                    Success = false,
                    Message = $"{request.Role} Role Not Exits",
                };
            }
           

            var loggedInUser = await _authenticatedUserService.GetLoggedInUser();

            //AddUser
            var user = _mapper.Map<User>(request);
            user.CreatedBy = loggedInUser == null ? request.CreatedBy : loggedInUser.Id;
            user.PasswordHash = _cryptographyService.EncryptPassword(request.Password);

            await _unitOfWork.GetRepository<User>().Add(user);
            var isDataAdded = await _unitOfWork.SaveChangesAsync();

            //AddUser

            var role = Enum.Parse<RoleEnum>(request.Role);


            //Assign Role
            var userRole = new UserRole();
            userRole.UserId = user.Id;
            userRole.RoleId = (int)role;

            await _unitOfWork.GetRepository<UserRole>().Add(userRole);
            await _unitOfWork.SaveChangesAsync();
            //Assign Role

            //Assign Permissions
            await AssignDefaultPermissionsToUserAsync(user, request.Role);
            //Assign Permissions

            //var response = new Response<User>
            //{
            //    Status = true,
            //    Message = "User Created Successfully",
            //    Data = user
            //};

            //return response;

            var response = _mapper.Map<CreateUserResponseClass>(user);

            return new ApiResponse<CreateUserResponseClass>()
            {
                Success = true,
                Message = "User Created Successfully",
                Data = response
            };
        }


        private async Task<List<Permission>> GetEnabledPermissionsForRoleAsync(string roleName)
        {
            var permissionList = await _unitOfWork.GetRepository<Permission>().GetAll();

            var permissions = permissionList.Where(p => (roleName == "Admin" && p.IsAdmin) ||
                            (roleName == "Vendor" && p.IsVendor) ||
                            (roleName == "Customer" && p.IsCustomer) ||
                            (roleName == "Merchant" && p.IsMerchant)).ToList();

            return permissions;
        }
        private async Task AssignDefaultPermissionsToUserAsync(User user, string roleName)
        {
            if (roleName == null)
            {
                throw new Exception("User does not have a role assigned");
            }

            var permissions = await GetEnabledPermissionsForRoleAsync(roleName);
            var list = new List<UserPermission>();

            foreach (var permission in permissions)
            {
                list.Add(new UserPermission { UserId = user.Id, PermissionId = permission.Id });
            }
            await _unitOfWork.GetRepository<UserPermission>().AddRange(list);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
