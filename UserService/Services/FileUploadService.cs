﻿using AutoMapper;
using UserService.DBContext;
using UserService.Models;

namespace UserService.Services
{
    public interface IFileUploadService
    {
        bool FileUpload(FileUploadModel fileUploadModel);
        List<UploadFileLog> GetFileUploadLog();
    }
    public class FileUploadService : IFileUploadService
    {
        private DBDataLoaderPortalContext _DBDataLoaderPortalContext;

        private IMapper _mapper;

        public FileUploadService(DBDataLoaderPortalContext dBDataLoaderPortalContext)
        {
            _DBDataLoaderPortalContext = dBDataLoaderPortalContext;
            //_mapper = mapper;
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
