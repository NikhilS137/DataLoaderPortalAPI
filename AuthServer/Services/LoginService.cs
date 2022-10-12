using AuthServer.Models;
using AuthServer.Services.Interfaces;

namespace AuthServer.Services
{
    public class LoginService : ILoginService
    {
        private DBDataLoaderPortalContext _DBDataLoaderPortalContext;

        public LoginService(DBDataLoaderPortalContext dBDataLoaderPortalContext)
        {
            _DBDataLoaderPortalContext = dBDataLoaderPortalContext;
        }

        dynamic ILoginService.Login(UserValidationModel login)
        {
            LoginMaster loginMaster = null;

            loginMaster = (from x in _DBDataLoaderPortalContext.LoginMasters
                           where x.Username == login.userName && x.Password == login.password
                           select x).SingleOrDefault();

            return loginMaster;
        }

    }
}
