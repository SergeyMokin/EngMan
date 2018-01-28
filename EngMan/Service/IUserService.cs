using System.Collections.Generic;
using EngMan.Models;
using System.Threading.Tasks;
namespace EngMan.Service
{
    public interface IUserService
    {
        User ValidateUser(string email, string password);
        List<UserView> GetUserList();
        UserView Registration(User user);
        Task<UserView> SaveUser(UserView user);
        int DeleteUser(int id);
        UserView GetUser(int id);
        UserView ChangePassword(int id, string oldpassword, string newpassword);
        Task<UserView> ChangeRole(UserView user);
    }
}
