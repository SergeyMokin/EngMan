using System.Collections.Generic;
using EngMan.Models;
using System.Threading.Tasks;
namespace EngMan.Service
{
    public interface IUserService
    {
        User ValidateUser(string email, string password);
        List<UserView> GetUserList();
        bool Registration(User user);
        Task<bool> SaveUser(UserView user);
        int DeleteUser(int id);
        UserView GetUser(int id);
        bool ChangePassword(int id, string oldpassword, string newpassword);
        Task<bool> ChangeRole(UserView user);
    }
}
