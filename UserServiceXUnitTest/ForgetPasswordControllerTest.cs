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
    public class ForgetPasswordControllerTest : IClassFixture<ForgetPasswordControllerFixture>
    {
        ForgetPasswordController forgetPasswordController;

        /**
         * xUnit constructor runs before each test. 
         */
        public ForgetPasswordControllerTest(ForgetPasswordControllerFixture fixture)
        {
            forgetPasswordController = fixture.forgetPasswordController;
        }

        [Fact]
        public async Task Put_WithExitingUserId_Ok_Test()
        {
            forgetpassword obj = new forgetpassword();
            obj.username = "Admin@Gmail.com";
            obj.password = "Admin@123";

            IActionResult result = await forgetPasswordController.Put(obj);

            // Assert
            OkResult objectResponse = Assert.IsType<OkResult>(result);

            Assert.Equal(200, objectResponse.StatusCode);
        }
        [Fact]
        public async Task Put_WithoutUsernameOrPassword_BadRequest_Test()
        {
            forgetpassword obj = new forgetpassword();
            obj.username = "";
            obj.password = "";

            IActionResult result = await forgetPasswordController.Put(obj);

            // Assert
            BadRequestResult objectResponse = Assert.IsType<BadRequestResult>(result);

            Assert.Equal(400, objectResponse.StatusCode);
        }
        [Fact]
        public async Task Put_WithNotExitedUsername_500_Test()
        {
            forgetpassword obj = new forgetpassword();
            obj.username = "user@gmail.com";
            obj.password = "Pass@123";

            IActionResult result = await forgetPasswordController.Put(obj);

            // Assert
            ObjectResult objectResponse = Assert.IsType<ObjectResult>(result);

            Assert.Equal(500, objectResponse.StatusCode);
        }
    }
}
