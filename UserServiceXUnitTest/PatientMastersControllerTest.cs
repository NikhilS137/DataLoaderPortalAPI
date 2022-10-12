using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using UserService.DBContext;
using UserService.Dtos;
using UserService.Models;
using UserServiceXUnitTest.Fixture;
using UserServiceXUnitTest.Theory;

namespace UserServiceXUnitTest
{
    public class PatientMastersControllerTest : IClassFixture<PatientMastersControllerFixture>
    {

        PatientMastersController patientMastersController;

        /**
         * xUnit constructor runs before each test. 
         */
        public PatientMastersControllerTest(PatientMastersControllerFixture fixture)
        {
            patientMastersController = fixture.patientMastersController;
        }

        [Fact]
        public void GetPatientMasters_WithTestData_ThenOk_Test()
        {
            var result = patientMastersController.GetPatientMasters();

            Assert.IsType<List<PatientMaster>>(result.Result.Value);
        }
        [Fact]
        public async Task PutPatientMaster_WithDiffIDs_ThenBadRequest_Test()
        {
            PatientMasterModel obj = new PatientMasterModel();

            obj.Id = 2;
            obj.Address1 = "Add2";
            obj.Address2 = "Add2";
            obj.Address3 = "Add2";
            obj.District = "Mumbai";
            obj.State = "Maharashtra";
            obj.Country = "India";
            obj.Dob = DateTime.Now;
            obj.EmailId = "EmailID@gmail.com";
            obj.PhoneNumber = "1234567890";

            IActionResult response = await patientMastersController.PutPatientMaster(1,obj);

            // Assert
            BadRequestResult objectResponse = Assert.IsType<BadRequestResult>(response);

            Assert.Equal(400, objectResponse.StatusCode);
        }

        [Fact]
        public async Task PutPatientMaster_WithValidData_ThenOk_Test()
        {
            PatientMasterModel obj = new PatientMasterModel();

            obj.Id = 1;
            obj.Address1 = "Add2";
            obj.Address2 = "Add2";
            obj.Address3 = "Add2";
            obj.District = "Mumbai";
            obj.State = "Maharashtra";
            obj.Country = "India";
            obj.Dob = DateTime.Now;
            obj.EmailId = "EmailID@gmail.com";
            obj.PhoneNumber = "1234567890";

            IActionResult response = await patientMastersController.PutPatientMaster(1, obj);

            // Assert
            OkResult objectResponse = Assert.IsType<OkResult>(response);

            Assert.Equal(200, objectResponse.StatusCode);
        }

        [Fact]
        public async Task PutPatientMaster_IdDoesNotExits_Then404_Test()
        {
            PatientMasterModel obj = new PatientMasterModel();

            obj.Id = 5;
            obj.Address1 = "Add2";
            obj.Address2 = "Add2";
            obj.Address3 = "Add2";
            obj.District = "Mumbai";
            obj.State = "Maharashtra";
            obj.Country = "India";
            obj.Dob = DateTime.Now;
            obj.EmailId = "EmailID@gmail.com";
            obj.PhoneNumber = "1234567890";

            IActionResult response = await patientMastersController.PutPatientMaster(5, obj);

            // Assert
            NotFoundResult objectResponse = Assert.IsType<NotFoundResult>(response);

            Assert.Equal(404, objectResponse.StatusCode);
        }

        [Fact]
        public async Task UpdateStatusPatientMaster_IdIsLessThan1_ThenBadRequest_Test()
        {

            IActionResult response = await patientMastersController.UpdateStatusPatientMaster(0, "Approved");

            // Assert
            BadRequestResult objectResponse = Assert.IsType<BadRequestResult>(response);

            Assert.Equal(400, objectResponse.StatusCode);
        }
        [Fact]
        public async Task UpdateStatusPatientMaster_WithValidData_ThenOk_Test()
        {

            IActionResult response = await patientMastersController.UpdateStatusPatientMaster(1, "Approved");

            // Assert
            OkResult objectResponse = Assert.IsType<OkResult>(response);

            Assert.Equal(200, objectResponse.StatusCode);
        }
        [Fact]
        public async Task UpdateStatusPatientMaster_IdDoesNotExits_Then404_Test()
        {
            IActionResult response = await patientMastersController.UpdateStatusPatientMaster(5, "Rejected");

            // Assert
            NotFoundResult objectResponse = Assert.IsType<NotFoundResult>(response);

            Assert.Equal(404, objectResponse.StatusCode);
        }

        [Fact]
        public void GetPatientDetailsByName_WithTestData_ThenOk_Test()
        {
            var result = patientMastersController.GetPatientMaster("P1");

            Assert.IsType<PatientMaster>(result.Result);
        }
        [Fact]
        public void GetPatientDetailsByName_WithDataNotExits_ThenOk_Test()
        {
            var result = patientMastersController.GetPatientMaster("P10");

            Assert.Equal(null, result.Result);
        }
        [Fact]
        public void GetPatientDetailsByNameOrEmail_WithTestData_ThenOk_Test()
        {
            var result = patientMastersController.GetPatientDetailsByNameOrEmail("P1");

            Assert.IsType<List<PatientMaster>>(result);
        }
        [Fact]
        public void GetPatientDetailsByNameOrEmail_WithDataNotExits_ThenOk_Test()
        {
            var result = patientMastersController.GetPatientDetailsByNameOrEmail("P10");

            Assert.Equal(0, result.Count);
        }

    }
}
