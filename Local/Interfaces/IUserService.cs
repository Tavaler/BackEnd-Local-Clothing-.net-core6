using Local.DTOS.Account;
using Local.Models;

namespace Local.Services
{
    public interface IUserService
    {
        Task<object> Register(RegisterRequest data);
        Task<object> Login(LoginRequest data);
         
        string GenerateToken(User user);
        User GetInfo(string accessToken);
        Task<object> GetByToken(string userToken);

        Task<IEnumerable<User>> FindAll();
    }
}
