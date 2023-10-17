using AutoMapper;
using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using LibraryManagmentAPI.Domain.Entities;
using LibraryManagmentAPI.Infrastructure.UnitOfWork;

namespace LibraryManagmentAPI.Common.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CheckoutService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseCommandResponse> AddCheckoutAsync(CreateCheckoutDTO checkoutDTO)
        {
            var response = new BaseCommandResponse();
            var existedCheckout=await unitOfWork.Checkouts.GetAll(s=>s.UserId == checkoutDTO.UserId&&s.BookId==checkoutDTO.BookId);
            if (existedCheckout.Count!=0)
            {
                response.Success = false;
                response.Message = "This Book Already Checkedout!";
                return response;
            }
            var newCheckout = mapper.Map<Checkout>(checkoutDTO);
            await unitOfWork.Checkouts.AddAsync(newCheckout);
            await unitOfWork.Save();

            response.Success = true;
            response.Message = "Success!";
            response.responseCode = Enums.ResponseCode.SUCCESS;
            return response;
        }
        public async Task<BaseCommandResponse> GetAllCheckoutsAsync()
        {
            var response=new BaseCommandResponse();
            var checkouts = await unitOfWork.Checkouts.GetAllAsync(x=>x.UserId!=null);
            if (checkouts == null)
            {
                response.Success = false;
                response.Message = "No data found!";
                response.responseCode = Enums.ResponseCode.NOT_FOUND;
            }
            var result=mapper.Map<IList<CheckoutDTO>>(checkouts);
            response.Success = true;
            response.responseCode = Enums.ResponseCode.SUCCESS;
            response.Message = "Success!";
            response.Data=result;
            return response;
        }
        public async Task<BaseCommandResponse> GetCheckoutAsync(int id)
        {
            var response=new BaseCommandResponse();
            var checkout=await unitOfWork.Checkouts.GetAsync(s=>s.Id==id);
            if(checkout == null)
            {
                response.Success = false;
                response.Message = "Not Found!";
                response.responseCode = Enums.ResponseCode.NOT_FOUND;
                return response;
            }
            var result = mapper.Map<CheckoutDTO>(checkout);
            response.Success = true;
            response.Data = result;
            response.responseCode=Enums.ResponseCode.SUCCESS;
            return response;
        }
        public async Task<BaseCommandResponse> UpdateCheckoutAsync(CheckoutDTO checkoutDTO)
        {
            var response = new BaseCommandResponse();
            var checkout=mapper.Map<Checkout>(checkoutDTO);
            await unitOfWork.Checkouts.UpdateAsync(checkout);
            await unitOfWork.Save();
            response.Success=true;
            response.Message = "Updated Successfully!";
            response.responseCode = Enums.ResponseCode.SUCCESS;
            return response;
        }

       
    }
}
