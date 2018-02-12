using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
using EngMan.Repository;
using System.Threading.Tasks;
namespace EngMan.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository rep;

        public UserService(IUserRepository _rep)
        {
            rep = _rep;
        }

        public User ValidateUser(string email, string password)
        {
            var userList = rep.Users;
            var user = userList.FirstOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }

        public UserView GetUser(int id)
        {
            return GetUserList().Where(x => x.Id == id).FirstOrDefault();
        }

        public UserView Registration(User user)
        {
            return rep.AddUser(user);
        }

        public async Task<UserView> SaveUser(UserView user)
        {
            return await rep.SaveUser(user);
        }

        public int DeleteUser(int id)
        {
            return rep.DeleteUser(id);
        }

        public List<UserView> GetUserList()
        {
            return rep.Users.Select(x => new UserView
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Role = x.Role
            }).ToList();
        }

        public UserView ChangePassword(int id, string oldpassword, string newpassword)
        {
            return rep.ChangePassword(id, oldpassword, newpassword);
        }

        public async Task<UserView> ChangeRole(UserView user)
        {
            return await rep.ChangeRole(user);
        }
    }
}