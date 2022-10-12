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
    public class PatientMastersControllerFixture : IDisposable
    {
        private TestDbContextMock testDbContextMock { get; set; }
        private PatientMastersService patientMastersService { get; set; }
        private IMapper mapper { get; set; }

        public PatientMastersController patientMastersController { get; private set; }

        public PatientMastersControllerFixture()
        {
            #region Create mock/memory database

            testDbContextMock = new TestDbContextMock();

            // mock data created by https://barisates.github.io/pretend
            testDbContextMock.PatientMasters.AddRange(new UserService.DBContext.PatientMaster[]
          {
                new UserService.DBContext.PatientMaster()
                {
                    Id=1,
                    PatientName= "P1",
                    Address1 = "Add1",
                    Address2 = "Add1",
                    Address3 = "Add1",
                    District = "Sangli",
                    State = "Maharashtra",
                    Country = "India",
                    Dob = DateTime.Now,
                    EmailId = "P1@gmail.com",
                    PhoneNumber ="1234556670",
                    DrugId ="12345-1234-11",
                    DrugName = "Drug 1",
                    Status = "Pending"
    },
                new UserService.DBContext.PatientMaster()
                {
                    Id=2,
                    PatientName= "P2",
                    Address1 = "Add2",
                    Address2 = "Add2",
                    Address3 = "Add2",
                    District = "Pune",
                    State = "Maharashtra",
                    Country = "India",
                    Dob = DateTime.Now,
                    EmailId = "P2@gmail.com",
                    PhoneNumber ="1234556671",
                    DrugId ="12345-1234-12",
                    DrugName = "Drug 2",
                    Status = "Pending"
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
            patientMastersService = new PatientMastersService(testDbContextMock);

            // Create Controller
            patientMastersController = new PatientMastersController(patientMastersService);

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
        ~PatientMastersControllerFixture()
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

                patientMastersService = null;
                mapper = null;

                patientMastersController = null;
            }
        }
        #endregion
    }
}
