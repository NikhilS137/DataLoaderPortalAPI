using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        //private readonly DBDataLoaderPortalContext _context;
        private IForgetPasswordService _ForgetPasswordService;

        public ForgetPasswordController(IForgetPasswordService forgetPasswordService)
        {
            //_context = context;
            _ForgetPasswordService = forgetPasswordService;
        }

        [HttpPut()]
        public async Task<IActionResult> Put(forgetpassword forgetpassword)
        {

            if (string.IsNullOrEmpty(forgetpassword.username) && string.IsNullOrEmpty(forgetpassword.password))
            {
                return BadRequest();
            }

            bool retVal = false;
            try
            {
                //var user = new LoginMaster()
                //{
                //    Id = _context.LoginMasters.Where(x => x.Username == forgetpassword.username).Select( x => x.Id).SingleOrDefault(),
                //    Username = forgetpassword.username,
                //    Password = forgetpassword.password
                //};


                //using (var db = new DBDataLoaderPortalContext())
                //{
                //    db.LoginMasters.Attach(user);
                //    db.Entry(user).Property(x => x.Password).IsModified = true;
                //    db.SaveChanges();

                //    return Ok();
                //}

                retVal = _ForgetPasswordService.UpdatePassword(forgetpassword);
                if (retVal)
                    return Ok();
                else
                    return Problem("Something went wrong.");


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ForgetPasswordService.UserExists(forgetpassword.username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        //private bool UserExists(string username)
        //{
        //    return _context.LoginMasters.Any(e => e.Username == username);
        //}
    }
}
