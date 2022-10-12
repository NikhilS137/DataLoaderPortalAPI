using UserService.DBContext;

namespace UserService.Dtos
{
    public class LoginMasterDtos
    {
        public class LoginMaster
        {
            public int Id { get; set; }
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
            public int RoleId { get; set; }
            public bool? IsActive { get; set; }

            public virtual RoleMaster Role { get; set; } = null;
        }
    }
}
