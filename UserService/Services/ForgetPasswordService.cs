using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Models;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class ForgetPasswordService : IForgetPasswordService
    {
        private DBDataLoaderPortalContext _DBDataLoaderPortalContext;

        private IMapper _mapper;

        public ForgetPasswordService(DBDataLoaderPortalContext dBDataLoaderPortalContext)
        {
            _DBDataLoaderPortalContext = dBDataLoaderPortalContext;
            //_mapper = mapper;
        }


        public bool UpdatePassword(forgetpassword forgetpassword)
        {
            bool retVal = false;

            try
            {
                var id = _DBDataLoaderPortalContext.LoginMasters.
                       Where(x => x.Username == forgetpassword.username).Select(x => x.Id).SingleOrDefault();

                var dbUser = _DBDataLoaderPortalContext.LoginMasters.FirstOrDefault(p => p.Id.Equals(id));

                if (dbUser == null)
                {
                    throw new DbUpdateConcurrencyException();
                }
                dbUser.Password = forgetpassword.password;

                var isPasswordModified = _DBDataLoaderPortalContext.Entry(dbUser).Property("Password").IsModified;
                _DBDataLoaderPortalContext.SaveChanges();
                retVal = true;

            }
            catch (Exception)
            {
                retVal = false;
            }
            return retVal;
        }

        public bool UserExists(string username)
        {
            return _DBDataLoaderPortalContext.LoginMasters.Any(e => e.Username == username);
        }
    }
}
