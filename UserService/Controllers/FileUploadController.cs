using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DBContext;
using UserService.Models;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        //private readonly DBDataLoaderPortalContext _context;
        private IFileUploadService _FileUploadService;

        public FileUploadController(IFileUploadService fileUploadService)
        {
            _FileUploadService = fileUploadService;
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
                bool result = false;

                result = _FileUploadService.FileUpload(fileUploadModel);

                if (result)    
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
                lsUploadFileLog = _FileUploadService.GetFileUploadLog();
            }
            catch (Exception ex)
            {
            }

            return lsUploadFileLog;
        }
    }
}
