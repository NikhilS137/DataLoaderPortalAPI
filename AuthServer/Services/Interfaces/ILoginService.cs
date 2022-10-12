using AuthServer.Models;

namespace AuthServer.Services.Interfaces
{
    public interface ILoginService
    {
        dynamic Login(UserValidationModel login);
    }
}
