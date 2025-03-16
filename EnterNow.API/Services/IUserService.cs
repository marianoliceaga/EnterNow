using EnterNow.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnterNow.API.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserById(string userId);
        Task<bool> CreateUser(ApplicationUser user, string password);
        Task<bool> UpdateUser(ApplicationUser user);
        Task<bool> DeleteUser(string userId);
    }
}
