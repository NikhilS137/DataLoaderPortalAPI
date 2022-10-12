using AuthServer.Controllers;
using AuthServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using UserService.Models;
using UserServiceXUnitTest.Fixture;

namespace UserServiceXUnitTest
{
    public class LoginControllerTest : IClassFixture<LoginFixture>
    {
        LoginController loginController;

        /**
         * xUnit constructor runs before each test. 
         */
        public LoginControllerTest(LoginFixture fixture)
        {
            loginController = fixture.loginController;
        }

        [Fact]
        public void Login_WithValidCredentials_Ok_Test()
        {
            UserValidationModel obj = new UserValidationModel();
            obj.userName = "Admin@Gmail.com";
            obj.password = "Admin@123";

            var result = loginController.PostLogin(obj) as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void Login_WithInvalidCredentials_BadReq_Test()
        {
            UserValidationModel obj = new UserValidationModel();
            obj.userName = "Admin@Gmail.com";
            obj.password = "pass@123";

            IActionResult response = loginController.PostLogin(obj);

            // Assert
            BadRequestResult objectResponse = Assert.IsType<BadRequestResult>(response);

            Assert.Equal(400, objectResponse.StatusCode);
        }

    }
}
