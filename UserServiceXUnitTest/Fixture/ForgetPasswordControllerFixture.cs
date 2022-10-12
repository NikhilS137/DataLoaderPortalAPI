using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using UserService.Services;
using UserServiceTest.Mock;

namespace UserServiceXUnitTest.Fixture
{
    public class ForgetPasswordControllerFixture : IDisposable
    {
        private TestDbContextMock testDbContextMock { get; set; }
        private ForgetPasswordService forgetPasswordService { get; set; }
        private IMapper mapper { get; set; }

        public ForgetPasswordController forgetPasswordController { get; private set; }

        public ForgetPasswordControllerFixture()
        {
            #region Create mock/memory database

            testDbContextMock = new TestDbContextMock();

            // mock data created by https://barisates.github.io/pretend
            testDbContextMock.LoginMasters.AddRange(new UserService.DBContext.LoginMaster[]
              {
                // for delete test
                new UserService.DBContext.LoginMaster()
                {
                   Id = 1,
                Username = "Admin@Gmail.com",
                Password = "Admin@123",
                RoleId = 1,
                IsActive = true
                },
                // for get test
                new UserService.DBContext.LoginMaster()
                {
                     Id = 2,
                Username = "nikhil@Gmail.com",
                Password = "nikhil@123",
                RoleId = 1,
                IsActive = true
                }
              });
            testDbContextMock.SaveChanges();

            #endregion

            #region Mapper settings with original profiles.

            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //mapper = mappingConfig.CreateMapper();

            #endregion

            // Create UserService with Memory Database
            forgetPasswordService = new ForgetPasswordService(testDbContextMock);

            // Create Controller
            forgetPasswordController = new ForgetPasswordController(forgetPasswordService);

        }

        #region ImplementIDisposableCorrectly
        /** https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1063?view=vs-2019 */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources, but leave the other methods
        // exactly as they are.
        ~ForgetPasswordControllerFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                testDbContextMock.Dispose();
                testDbContextMock = null;

                forgetPasswordService = null;
                mapper = null;

                forgetPasswordController = null;
            }
        }
        #endregion
    }
}
