using System.Collections.Generic;
using EngMan.Models;
using System.Threading.Tasks;
using System.Dynamic;
namespace EngMan.Service
{
    public interface IUserService
    {
        //log in user
        Task<ExpandoObject> Login(UserLogin user);

        //validation of the incoming user
        User ValidateUser(string email, string password);

        //get list of all users
        List<UserView> GetUserList();

        //add user to db
        Task<ExpandoObject> Registration(User user);

        //edit user in db
        Task<bool> SaveUser(UserView user);

        //delete user from db
        string DeleteUser(int id);

        //get user by id
        UserView GetUser(int id);

        //change password of user
        bool ChangePassword(int id, string oldpassword, string newpassword);

        //change role of user
        Task<bool> ChangeRole(UserView user);
    }
}
