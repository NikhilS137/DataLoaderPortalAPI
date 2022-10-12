using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.DBContext;
using UserService.Dtos;

namespace UserService.Services
{
    public interface ILoginService
    {
        dynamic Login(LoginMaster login); 
    }
    public class LoginService : ILoginService
    {
        private DBDataLoaderPortalContext _DBDataLoaderPortalContext;

        private IMapper _mapper;

        public LoginService(DBDataLoaderPortalContext dBDataLoaderPortalContext)
        {
            _DBDataLoaderPortalContext = dBDataLoaderPortalContext;
            //_mapper = mapper;
        }

        dynamic ILoginService.Login(LoginMaster login)
        {
            LoginMaster loginMaster = null;
            
            loginMaster = (from x in _DBDataLoaderPortalContext.LoginMasters
                           where x.Username == login.Username && x.Password == login.Password
                           select x).SingleOrDefault();

            return  loginMaster;
        }
        object BadRequest()
        {
            return BadRequest();
        }

    }
}
