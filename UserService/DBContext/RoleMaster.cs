using System;
using System.Collections.Generic;

namespace UserService.DBContext
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            LoginMasters = new HashSet<LoginMaster>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<LoginMaster> LoginMasters { get; set; }
    }
}
