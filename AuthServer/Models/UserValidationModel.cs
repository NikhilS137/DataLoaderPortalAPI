namespace AuthServer.Models
{
    public class UserValidationModel
    {
        private readonly DBDataLoaderPortalContext _context = new DBDataLoaderPortalContext();
        public string userName { get; set; }
        public string password { get; set; }

        public bool ValidateCredentials(string UserName, string Password)
        {
            LoginMaster loginMaster = null;

            if (_context.LoginMasters == null)
            {
                //return userMaster1;
                return false;
            }

            //loginMaster = (from x in _context.LoginMaster
            //               where x.UserName == UserName && x.Password == EncryptionDecryption.EncodePasswordToBase64(Password)
            //               select x).SingleOrDefault();

            var userMaster = (from x in _context.LoginMasters
                              where x.Username == UserName && x.Password == Password
                              select x.Id).SingleOrDefault();

            if (userMaster > 0)
            {
                return true;
            }

            return false;
            //return loginMaster;

        }
    }
}
