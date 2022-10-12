using AuthServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthServer.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _LoginService;
        private ITokenService _TokenService;
        private readonly IConfiguration _configuration;
        public LoginController(ILoginService loginService, ITokenService tokenService, IConfiguration configuration)
        {
            _LoginService = loginService;   
            _TokenService = tokenService;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult PostLogin(UserValidationModel userValidationModel)
        {
            if (string.IsNullOrEmpty(userValidationModel.userName) 
                || string.IsNullOrEmpty(userValidationModel.password))
            {
                return BadRequest();
            }

            LoginMaster result = null;

            try
            {
                result = _LoginService.Login(userValidationModel);
               
                if (result != null)
                {
                    var token = _TokenService.BuildToken(_configuration.GetValue<string>("Jwt:Key"),
                                                         _configuration.GetValue<string>("Jwt:Issuer"),
                                                         new[]
                                                         {
                                                                _configuration.GetValue<string>("Jwt:Aud1")
                                                                 },
                                                         userValidationModel.userName);
                    dynamic retVal = new
                    {
                        Token = token,
                        IsAuthenticated = true,
                        User = result
                    };
                    return Ok(retVal);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }
}
