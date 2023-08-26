using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;

namespace LibraryManagmentAPI.Common.IServices
{
    public interface IAuthService
    {
        public Task<bool> ValidateUser(LoginUserDTO loginUserDTO);
        public Task<string> GenerateToken();

        Task<BaseCommandResponse> Register(UserDTO userDTO);
        Task<BaseCommandResponse> Login(LoginUserDTO loginUserDTO);
    }
}
