using AutoMapper;
using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using LibraryManagmentAPI.Domain.Entities;
using LibraryManagmentAPI.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LibraryManagmentAPI.Common.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> GetAllUsers()
        {
            var response = new BaseCommandResponse();
            try
            {
                var users = await unitOfWork.Users.GetAll(includes: new List<string> { "Checkouts" });
                var result = users.Where(user => user.Checkouts.Any());
                var usersDTO = mapper.Map<IList<UserDataDTO>>(result);
                response.Success = true;
                response.responseCode = Enums.ResponseCode.SUCCESS;
                response.Data = usersDTO;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.responseCode = Enums.ResponseCode.ERROR;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseCommandResponse> GetUser(string userName)
        {
            var response = new BaseCommandResponse();
            if (userName == null)
            {
                response.Success = false;
                response.Message = "Invalid User Name!";
                response.responseCode = Enums.ResponseCode.ERROR;
                return response;
            }
            var user = await unitOfWork.Users.GetAll(s => s.UserName == userName, includes: new List<string> { "Checkouts" });
            var userDTO=mapper.Map<IList<UserDataDTO>>(user);
            response.Success = true;
            response.Data=userDTO;
            response.responseCode = Enums.ResponseCode.SUCCESS;
            return response;
        }
    }
}
