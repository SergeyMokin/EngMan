using System.Threading.Tasks;
using EngMan.Models;
using System.Linq;
using System.Collections.Generic;

namespace EngMan.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        Task<bool> SaveUser(UserView user);

        bool AddUser(User user);

        int DeleteUser(int id);

        bool ChangePassword(int id, string oldpassword, string newpassword);

        Task<bool> ChangeRole(UserView user);
    }
}
