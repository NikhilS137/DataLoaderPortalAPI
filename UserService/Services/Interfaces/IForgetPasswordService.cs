using UserService.Models;

namespace UserService.Services.Interfaces
{
    public interface IForgetPasswordService
    {
        bool UpdatePassword(forgetpassword forgetpassword);
        bool UserExists(string username);
    }
}
