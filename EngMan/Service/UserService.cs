using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
using EngMan.Repository;
using System.Threading.Tasks;
using EngMan.Extensions;
using System.Net.Http;

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
            if (email.IsEmail())
            {
                try
                {
                    var user = rep.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password.VerifyHashedPassword(password));
                    return user;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public UserView GetUser(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return GetUserList().Where(x => x.Id == id).FirstOrDefault();
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public bool Registration(User user)
        {
            if (user.Validate())
            {
                try
                {
                    return rep.AddUser(user);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<bool> SaveUser(UserView user)
        {
            if (user.Validate())
            {
                try
                {
                    return await rep.SaveUser(user);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public int DeleteUser(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.DeleteUser(id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public List<UserView> GetUserList()
        {
            try
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
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public bool ChangePassword(int id, string oldpassword, string newpassword)
        {
            if (id.Validate() && oldpassword.Validate() && newpassword.IsCorrectPassword())
            {
                try
                {
                    return rep.ChangePassword(id, oldpassword, newpassword);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<bool> ChangeRole(UserView user)
        {
            if (user.Validate())
            {
                try
                {
                    return await rep.ChangeRole(user);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }
    }
}