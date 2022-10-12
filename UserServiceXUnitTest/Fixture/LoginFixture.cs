using AuthServer.Controllers;
using AuthServer.Services;
using AuthServer.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using UserService.Helpers;
using UserService.Services;
using UserServiceTest.Mock;

namespace UserServiceXUnitTest.Fixture
{
    public class LoginFixture : IDisposable
    {
        private TestAuthDbContextMock testAuthDbContextMock { get; set; }
        private LoginService loginService { get; set; }
        private TokenService tokenService { get; set; }
        private Configuration configuration { get; set; }
        public LoginController loginController { get; private set; }

        public LoginFixture()
        {
            #region Create mock/memory database

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

            #endregion

            // Create UserService with Memory Database
            loginService = new LoginService(testAuthDbContextMock);


            var myConfiguration = new Dictionary<string, string>
                    {
                        { "Jwt:Key", "SampleSecretKeyToRemovedDuringProduction"},
                        { "Jwt:Issuer", "api.AuthServer.com"},
                        { "Jwt:Aud1", "api.UserService.com"}
                    };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var service = new TokenService();
            tokenService = service;

            // Create Controller
            loginController = new LoginController(loginService, tokenService, configuration);

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
        ~LoginFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                testAuthDbContextMock.Dispose();
                testAuthDbContextMock = null;

                loginService = null;

                loginController = null;
            }
        }
        #endregion
    }
}
