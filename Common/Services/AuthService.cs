using AutoMapper;
using LibraryManagmentAPI.Common.BaseResponse;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Common.IServices;
using LibraryManagmentAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagmentAPI.Common.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly SignInManager<User> signInManager;
        private User user;

        public AuthService(UserManager<User> userManager, IConfiguration configuration,
            IMapper mapper, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        public async Task<string> GenerateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
                 new Claim("Email", user.UserName),
                 new Claim("UserId", user.Id)
             };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim("RoleName", role));
                claims.Add(new Claim("Email", user.Email));
                claims.Add(new Claim("UserId", user.Id));
            }

            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public async Task<BaseCommandResponse> Register(UserDTO userDTO)
        {
            var response = new BaseCommandResponse();

            if (userDTO == null)
            {
                response.responseCode = Enums.ResponseCode.ERROR;
                response.Success = false;
                response.Message = "Failed Attempt!";
            }
            var user = mapper.Map<User>(userDTO);
            user.UserName = userDTO.Email;
            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = "Failed Attempt!";
            }
            else{ 
            await userManager.AddToRolesAsync(user, userDTO.Roles);
            response.Success = true;
            response.responseCode = Enums.ResponseCode.SUCCESS;
            response.Message = "Your Account Created Successfully!";
            response.Data = result;        
        }
            return response;
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            user = await userManager.FindByNameAsync(userDTO.Email);
            var validPassword = await userManager.CheckPasswordAsync(user, userDTO.Password);
            return (user != null && validPassword);
        }

        public async Task<BaseCommandResponse> Login(LoginUserDTO loginUserDTO)
        {
            var response = new BaseCommandResponse();
            if (loginUserDTO == null)
            {
                response.Success = false;
                response.Message = "Failed to login!";
                response.responseCode = Enums.ResponseCode.ERROR;
            }
            try
            {
                if (!await ValidateUser(loginUserDTO))
                {
                    response.Success = false;
                    response.Message = "Unauthorized!";
                    response.responseCode = Enums.ResponseCode.ERROR;
                }
                else
                {
                    var result = new { Token = await GenerateToken() };
                    response.Success = true;
                    response.Message = "Login Successfully!";
                    response.responseCode = Enums.ResponseCode.SUCCESS;
                    response.Data = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message=ex.Message;
                response.responseCode=Enums.ResponseCode.ERROR;
                response.Data=ex.Message;
                return response;
            }
            return response;
        }
    }
}
