using LibraryManagmentAPI.Common.BaseResponse;

namespace LibraryManagmentAPI.Common.IServices
{
    public interface IUserService
    {
        Task<BaseCommandResponse> GetAllUsers();
        Task<BaseCommandResponse> GetUser(string userName);
    }
}
