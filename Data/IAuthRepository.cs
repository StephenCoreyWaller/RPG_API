using System.Threading.Tasks;
using WebAPI_RPG.Models;

namespace WebAPI_RPG.Data
{
    public interface IAuthRepository
    {
        Task<ServiceWrapper<int>> Register(User user, string password); 
        Task<ServiceWrapper<string>> Login(string username, string password); 
        Task<bool> UserExist(string username); 
    }
}