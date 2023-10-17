using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            this.checkoutService = checkoutService;
        }

        [HttpGet("getAllCheckouts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllCheckouts()
        {
            var checkouts=await checkoutService.GetAllCheckoutsAsync();
            return Ok(checkouts);
        }

        [HttpGet("getCheckout/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> GetCheckout(int id)
        {
            var checkouts = await checkoutService.GetCheckoutAsync(id);
            return Ok(checkouts);
        }

        [HttpPost("addCheckout")]
        public async Task<ActionResult<BaseCommandResponse>> AddCheckout(CreateCheckoutDTO checkoutDTO)
        {
            return Ok(await checkoutService.AddCheckoutAsync(checkoutDTO));
        }

        [HttpPut("updateCheckout")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateCheckout(CheckoutDTO checkoutDTO)
        {
            return Ok(await checkoutService.UpdateCheckoutAsync(checkoutDTO));
        }
    }
}
