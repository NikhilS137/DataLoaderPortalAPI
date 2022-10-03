using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DBContext;
using UserService.Models;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly DBDataLoaderPortalContext _context;

        public FileUploadController(DBDataLoaderPortalContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("FileUpload")]
        public IActionResult FileUpload(FileUploadModel fileUploadModel)
        {
            if (string.IsNullOrEmpty(fileUploadModel.FileName)
                || string.IsNullOrEmpty(fileUploadModel.strFile))
            {
                return BadRequest();
            }
            

            try
            {
                string FilePath = "";
                int id = 0;
                FilePath = fileUploadModel.SaveFileOnServer(out id);

                bool result = false;
                result = fileUploadModel.SaveRecordsInDB(FilePath,id);
                if(result)    
                    return Ok();
                else
                    return Problem();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpGet]
        public List<UploadFileLog> GetFileUploadLog()
        {
            List<UploadFileLog> lsUploadFileLog = new List<UploadFileLog>();

            try
            {

                lsUploadFileLog = (from files in _context.UploadFileLogs
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
            }
            catch (Exception ex)
            {
            }

            return lsUploadFileLog;
        }
    }
}
