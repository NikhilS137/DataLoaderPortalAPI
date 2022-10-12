using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Dtos;
using Xunit;

namespace UserServiceTest.Theory
{
    public class LoginTheoryData : TheoryData<LoginMasterDtos.LoginMaster>
    {
        public LoginTheoryData()
        {
            /**
             * Each item you add to the TheoryData collection will try to pass your unit test's one by one.
             */

            Add(new LoginMasterDtos.LoginMaster()
            {
                Id = 1,
                Username = "Admin@Gmail.com",
                Password = "Admin@123",
                RoleId = 1,
                IsActive = true
            });
        }
    }
}
