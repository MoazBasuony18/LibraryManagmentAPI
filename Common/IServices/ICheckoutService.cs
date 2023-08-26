using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;

namespace LibraryManagmentAPI.Common.IServices
{
    public interface ICheckoutService
    {
        Task<BaseCommandResponse> GetAllCheckoutsAsync();
        Task <BaseCommandResponse>UpdateCheckoutAsync(CheckoutDTO checkoutDTO);
        Task<BaseCommandResponse> AddCheckoutAsync(CreateCheckoutDTO checkoutDTO);
        Task<BaseCommandResponse> GetCheckoutAsync(int id);
    }
}
