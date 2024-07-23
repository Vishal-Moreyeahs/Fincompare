using AutoMapper;
using Fincompare.Application.Contracts.Infrastructure;
using Fincompare.Application.Contracts.Persistence;
using Fincompare.Application.Repositories;
using Fincompare.Application.Request;
using Fincompare.Application.Request.CustomerRequests;
using Fincompare.Application.Response;
using Fincompare.Application.Response.CustomerUserResponse;
using Fincompare.Domain.Entities;
using Fincompare.Domain.Entities.UserManagementEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fincompare.Application.Services
{
    public class CustomerUserService : ICustomerUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ICryptographyService _cryptographyService;
        public CustomerUserService(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, ICryptographyService cryptographyService )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
            _cryptographyService = cryptographyService;
        }
    
        public async Task<ApiResponse<CustomerUserResponseViewModel>> AddCustomerAsUser(AddCustomerRequest model)
        {
            try
            {
                var emailExist = await _unitOfWork.GetRepository<CustomerUser>().GetAll();

                if (emailExist.Any(x => x.EmailId == model.EmailId))
                {
                    return new ApiResponse<CustomerUserResponseViewModel> { Status = false, Message = "Email Id already exist." };
                }

                //var hashedPassword = _cryptographyService.EncryptPassword(model.Password);

                var user = new RegisterUserRequest
                {
                    FirstName = model.CustomerName,
                    LastName = model.CustomerName,
                    Email = model.EmailId,
                    Password = model.Password,
                    Phone = model.Phone,
                    CreatedBy = null
                };

                var registeredUser = await _authService.Register(user);

                var data = _mapper.Map<CustomerUser>(model);
                data.UserId = registeredUser.Data.Id;

                await _unitOfWork.GetRepository<CustomerUser>().Add(data);
                await _unitOfWork.SaveChangesAsync();

                var responseData = _mapper.Map<CustomerUserResponseViewModel>(model);
                responseData.CustomerId = data.Id;
                return new ApiResponse<CustomerUserResponseViewModel> { Status = true, Message = "Customer registered successfully", Data = responseData };

            }
            catch (Exception ex) {
                return new ApiResponse<CustomerUserResponseViewModel> { Status = false, Message = "Customer registration failed" };

            }

        }

        public async Task<ApiResponse<IEnumerable<CustomerUserResponseViewModel>>> GetCustomerAsUser(int? customerId)
        {
            try
            {
                var customers = await _unitOfWork.GetRepository<CustomerUser>().GetAll();

                if (customers == null)
                {
                    return new ApiResponse<IEnumerable<CustomerUserResponseViewModel>> { Status = false, Message = "Customers not found." };

                }

                if (customerId.HasValue)
                    customers = customers.Where(x => x.Id == customerId);

                var data = _mapper.Map<IEnumerable<CustomerUserResponseViewModel>>(customers);

                return new ApiResponse<IEnumerable<CustomerUserResponseViewModel>> { Status = true, Message = "Customers fetched.", Data = data };

            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CustomerUserResponseViewModel>> { Status = false, Message = "Customers fetched failed."};

            }

        }

        public async Task<ApiResponse<UpdateCustomerRequest>> UpdateCustomerAsUser(UpdateCustomerRequest model)
        {
            try
            {
                var emailExist = await _unitOfWork.GetRepository<CustomerUser>().GetById(model.Id);

                if (emailExist == null)
                {
                    return new ApiResponse<UpdateCustomerRequest> { Status = false, Message = "Customer not found." };
                }

                var user = await _unitOfWork.GetRepository<User>().GetById(emailExist.UserId);

                if (emailExist == null)
                {
                    return new ApiResponse<UpdateCustomerRequest> { Status = false, Message = "User not found." };
                }
                var updateCustomer = _mapper.Map(model, emailExist);
                await _unitOfWork.GetRepository<CustomerUser>().Upsert(updateCustomer);
                await _unitOfWork.SaveChangesAsync();

                user.FirstName = updateCustomer.CustomerName;
                user.LastName = updateCustomer.CustomerName;
                user.Email = updateCustomer.EmailId;
                user.Phone = updateCustomer.Phone;

                await _unitOfWork.GetRepository<User>().Upsert(user);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<UpdateCustomerRequest> { Status = true, Message = "Customer updated successfully", Data = model };


            }
            catch (Exception ex)
            {
                return new ApiResponse<UpdateCustomerRequest> { Status = false, Message = "Customer updated failed" };

            }

        }
    }
}
