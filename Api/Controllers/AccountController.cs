using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;

        public AccountController(IAuthService authService,IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseCommandResponse>> Register(UserDTO userDTO)
        {
            return Ok(await authService.Register(userDTO));
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseCommandResponse>> Login(LoginUserDTO loginUserDTO)
        {
            return Ok(await authService.Login(loginUserDTO));
        }


        [HttpGet("getAllUsers")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllUsers()
        {
            return Ok(await userService.GetAllUsers());
        }

        [HttpGet("getUserByUserName")]
        public async Task<ActionResult<BaseCommandResponse>> GetUserByUserName(string userName)
        {
            return Ok(await userService.GetUser(userName));
        }
    }
}
