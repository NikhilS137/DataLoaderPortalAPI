using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Controllers;
using UserService.DBContext;
using UserServiceTest.MockData;
using UserServiceTest.Systems.DBContext;
using UserServiceTest.Systems.Test;

namespace UserServiceTest.Systems.Controllers
{
    [TestFixture]
    public class TestPatientMastersController
    {
        //[Test]
        //public async Task GetPatientMasters_ShouldReturn200Status()
        //{
        //    ///Arrange
        //    //var context = new Mock<DBDataLoaderPortalContext>();

        //    //var Pcontroller = new Mock<PatientMastersController>();


        //    ////context.Setup(_ => _.PatientMasters.ToListAsync())
        //    ////    .ReturnsAsync(PatientMasterMockData.GetPatientMasters());


        //    //Pcontroller.Setup(_ => _.GetPatientMasters())
        //    //    .ReturnsAsync(PatientMasterMockData.GetPatientMasters());

        //    ////var sut = new PatientMastersController(context);
        //    ///

        //    var controller = new PatientMastersController(new TestDBDataLoaderPortalContext());

        //    var result = await controller.GetPatientMasters();

        //    Assert.IsNotNull(result);
        //    //Assert.IsInstanceOf<OkResult>(result);

        //    ///Act
        //    ///Assert
        //}

        [Test]
        public void GetItems_ShouldReturnAllItems()
        {
            var context = new TestUnitTestMockingConext();
            context.PatientMasters.Add(new PatientMaster
            {
                Id = 1,
                Address1 = "Add1",
                Address2 = "Add2",
                Address3 = "Add 3",
                District = "Sangli",
                State = "Maharashtra",
                Country = "India",
                Dob = new DateTime(1990, 12, 12),
                EmailId = "Admin@gmail.com",
                PhoneNumber = "12345678890",
                DrugId = "12344",
                DrugName = "Drug 1"
            });
            context.PatientMasters.Add(new PatientMaster
            {
                Id = 2,
                Address1 = "Add2",
                Address2 = "Add2",
                Address3 = "Add 2",
                District = "Pune",
                State = "Maharashtra",
                Country = "India",
                Dob = new DateTime(1990, 12, 12),
                EmailId = "Pune@gmail.com",
                PhoneNumber = "12345678890",
                DrugId = "12342",
                DrugName = "Drug 2"
            });
            var controller = new PatientMastersController(context);
            var result = controller.GetPatientMasters() as TestItemDbSet;
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }
    }
}
