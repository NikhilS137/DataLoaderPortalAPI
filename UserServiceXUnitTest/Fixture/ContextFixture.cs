using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServiceTest.Mock;

namespace UserServiceTest.Fixture
{
    public class ContextFixture : IDisposable
    {
        public TestDbContextMock testDbContextMock;

        public ContextFixture()
        {
            testDbContextMock = new TestDbContextMock();

            testDbContextMock.LoginMasters.AddRange(new UserService.DBContext.LoginMaster[]
            {
                new UserService.DBContext.LoginMaster()
                {
                   Id = 1,
                Username = "Admin@Gmail.com",
                Password = "Admin@123",
                RoleId = 1,
                IsActive = true
                },
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

            testDbContextMock.UploadFileLogs.AddRange(new UserService.DBContext.UploadFileLog[]
           {
                // for delete test
                new UserService.DBContext.UploadFileLog()
                {
                    FileUploadId=1,
                    FileName ="ABC.xlsx",
                    ServerFileName ="Abc123.xlsx",
                    Status ="Completed",
                    SavedRecordsCount = 5,
                    ValidationFailedRecordsCount = 2,
                    TotalRecordsCount = 7,
                    FileLocation = "D:\\Nikhil\\DataLoaderPortalAPIGateway\\UserService\\Uploadfiles\\excel_29092022075052.xlsx"
                },
                new UserService.DBContext.UploadFileLog()
                {
                    FileUploadId=2,
                    FileName ="xyz.xlsx",
                    ServerFileName ="xyz123.xlsx",
                    Status ="Failed",
                    SavedRecordsCount = 0,
                    ValidationFailedRecordsCount = 0,
                    TotalRecordsCount = 0,
                    FileLocation = "D:\\Nikhil\\DataLoaderPortalAPIGateway\\UserService\\Uploadfiles\\excel_29092022075052.xlsx"
                },
           });
            testDbContextMock.SaveChanges();

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
        ~ContextFixture()
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
                if (testDbContextMock != null)
                {
                    testDbContextMock.Dispose();
                    testDbContextMock = null;
                }
            }
        }
        #endregion
    }
}
