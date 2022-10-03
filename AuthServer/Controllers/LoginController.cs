using AuthServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostLogin(UserValidationModel userValidationModel)
        {
            if (string.IsNullOrEmpty(userValidationModel.userName) 
                || string.IsNullOrEmpty(userValidationModel.password))
            {
                return BadRequest();
            }

            bool result = false;

            try
            {
                result = userValidationModel.ValidateCredentials(userValidationModel.userName, userValidationModel.password);

                if (result)
                    return Ok();
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
