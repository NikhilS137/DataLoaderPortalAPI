using UserService.DBContext;
using UserService.Models;

namespace UserService.Services.Interfaces
{
    public interface IFileUploadService
    {
        bool FileUpload(FileUploadModel fileUploadModel);
        List<UploadFileLog> GetFileUploadLog();
    }
}
