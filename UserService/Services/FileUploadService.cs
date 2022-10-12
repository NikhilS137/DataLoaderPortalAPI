using AutoMapper;
using UserService.DBContext;
using UserService.Models;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class FileUploadService : IFileUploadService
    {
        private DBDataLoaderPortalContext _DBDataLoaderPortalContext;

        public FileUploadService(DBDataLoaderPortalContext dBDataLoaderPortalContext)
        {
            _DBDataLoaderPortalContext = dBDataLoaderPortalContext;
        }

        public bool FileUpload(FileUploadModel fileUploadModel)
        {
            bool result = false;
            string FilePath = "";
            int id = 0;
            FilePath = fileUploadModel.SaveFileOnServer(out id);
         
            result = fileUploadModel.SaveRecordsInDB(FilePath, id);
           
            return result;  
        }

        public List<UploadFileLog> GetFileUploadLog()
        {
            List<UploadFileLog> lsUploadFileLog = new List<UploadFileLog>();

            lsUploadFileLog = (from files in _DBDataLoaderPortalContext.UploadFileLogs
                               where files.Status != "Failed"
                               orderby files.FileUploadId descending
                               select new UploadFileLog
                               {
                                   FileUploadId = files.FileUploadId,
                                   FileName = files.FileName,
                                   SavedRecordsCount = files.SavedRecordsCount,
                                   ValidationFailedRecordsCount = files.ValidationFailedRecordsCount,
                                   TotalRecordsCount = files.TotalRecordsCount,
                                   Status = files.Status
                               }).ToList();

            return lsUploadFileLog;
        }
    }
}
