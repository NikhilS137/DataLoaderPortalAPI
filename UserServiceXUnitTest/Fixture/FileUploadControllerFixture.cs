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
    public class FileUploadControllerFixture : IDisposable
    {

        private TestDbContextMock testDbContextMock { get; set; }
        private FileUploadService fileUploadService { get; set; }
        private IMapper mapper { get; set; }

        public FileUploadController fileUploadController { get; private set; }

        public FileUploadControllerFixture()
        {
            #region Create mock/memory database

            testDbContextMock = new TestDbContextMock();

            // mock data created by https://barisates.github.io/pretend
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

            #endregion

            #region Mapper settings with original profiles.

            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //mapper = mappingConfig.CreateMapper();

            #endregion

            // Create UserService with Memory Database
            fileUploadService = new FileUploadService(testDbContextMock);

            // Create Controller
            fileUploadController = new FileUploadController(fileUploadService);

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
        ~FileUploadControllerFixture()
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

                fileUploadService = null;
                mapper = null;

                fileUploadController = null;
            }
        }
        #endregion
    }
}
