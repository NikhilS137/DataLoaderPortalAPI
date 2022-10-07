namespace AuthServer.Models
{
    public class UserValidationModel
    {
        private readonly DBDataLoaderPortalContext _context = new DBDataLoaderPortalContext();
        public string userName { get; set; }
        public string password { get; set; }

        public LoginMaster ValidateCredentials(string UserName, string Password)
        {
            LoginMaster loginMaster = null;

            if (_context.LoginMasters == null)
            {
                return loginMaster;
                //return false;
            }

            //loginMaster = (from x in _context.LoginMaster
            //               where x.UserName == UserName && x.Password == EncryptionDecryption.EncodePasswordToBase64(Password)
            //               select x).SingleOrDefault();

             loginMaster = (from x in _context.LoginMasters
                              where x.Username == UserName && x.Password == Password
                              select x).SingleOrDefault();

            //if (userMaster > 0)
            //{
            //    return true;
            //}

            //return false;
            return loginMaster;

        }
    }
}
