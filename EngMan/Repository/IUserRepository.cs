using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        Task<UserView> SaveUser(UserView user);

        UserView AddUser(User user);

        int DeleteUser(int id);

        UserView ChangePassword(int id, string oldpassword, string newpassword);

        Task<UserView> ChangeRole(UserView user);
    }
}
