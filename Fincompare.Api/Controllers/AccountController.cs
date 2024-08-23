using AutoMapper;
using Fincompare.Api.Middleware;
using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Request;
using Fincompare.Application.Response;
using Fincompare.Domain.Entities.UserManagementEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Fincompare.Application.Response.UserResponse.UserResponseViewClass;

namespace Fincompare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private readonly IUserManagerServices _userManagerServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountController(IAuthService authenticationService, IUserManagerServices userManagerServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _userManagerServices = userManagerServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [ValidateModelState]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("add-user")]
        [ValidateModelState]
        public async Task<ActionResult<int>> Register(RegisterUserRequest user)
        {
            return Ok(await _authenticationService.Register(user));
        }

        [HttpPost("verify-token")]
        private IActionResult VerifyResetToken(string token)
        {
            return Ok(_authenticationService.IsTokenValid(token));
        }

        [Authorize]
        [HttpPost, Route("change-password")]
        [ValidateModelState]
        public async Task<ActionResult> ChangeCurrentPassword(ChangePasswordRequest model)
        {
            var response = await _userManagerServices.ChangeExistingPassword(model);
            return Ok(response);
        }

        [HttpGet]
        [Route("fetch-user")]
        public async Task<IActionResult> GetUser()
        {
            var response = await _unitOfWork.GetRepository<User>().GetAll();
            var data = _mapper.Map<IEnumerable<CreateUserResponseClass>>(response);
            return Ok(data);
        }

        //[HasPermission(PermissionEnum.CanAccessAdmin)]
        //[HttpGet]
        //[Route("test-admin")]
        //public IActionResult TestAdmin()
        //{
        //    return Ok("admin authorized");
        //}

        //[HasPermission(PermissionEnum.CanAccessMerchant)]
        //[HttpGet]
        //[Route("test-merchant")]
        //public IActionResult TestMerchant()
        //{
        //    return Ok("Merchant-Authorized");
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }
            return Ok("Logout Successfully");
        }


    }
}
