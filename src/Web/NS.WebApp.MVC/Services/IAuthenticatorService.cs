using NS.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public interface IAuthenticatorService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);
        Task<UserResponseLogin> Register(UserRegister userRegister);
    }
}
