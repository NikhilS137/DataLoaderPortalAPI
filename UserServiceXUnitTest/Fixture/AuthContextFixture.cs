using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceTest.Mock;

namespace UserServiceXUnitTest.Fixture
{
    public class AuthContextFixture : IDisposable
    {
        public TestAuthDbContextMock testAuthDbContextMock;

        public AuthContextFixture()
        {
            testAuthDbContextMock = new TestAuthDbContextMock();

            testAuthDbContextMock.LoginMasters.AddRange(new AuthServer.Models.LoginMaster[]
            {
                new AuthServer.Models.LoginMaster()
                {
                   Id = 1,
                Username = "Admin@Gmail.com",
                Password = "Admin@123",
                RoleId = 1,
                IsActive = true
                },
                new AuthServer.Models.LoginMaster()
                {
                     Id = 2,
                Username = "nikhil@Gmail.com",
                Password = "nikhil@123",
                RoleId = 1,
                IsActive = true
                }
            });
            testAuthDbContextMock.SaveChanges();
        }

        #region ImplementIDisposableCorrectly
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~AuthContextFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (testAuthDbContextMock != null)
                {
                    testAuthDbContextMock.Dispose();
                    testAuthDbContextMock = null;
                }
            }
        }
        #endregion
    }
}
