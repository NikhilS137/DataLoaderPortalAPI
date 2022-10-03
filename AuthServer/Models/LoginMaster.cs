using System;
using System.Collections.Generic;

namespace AuthServer.Models
{
    public partial class LoginMaster
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }

        public virtual RoleMaster Role { get; set; } = null!;
    }
}
