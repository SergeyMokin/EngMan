using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
using EngMan.Repository;
using System.Threading.Tasks;
using EngMan.Extensions;
using System;

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
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public UserView GetUser(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return GetUserList().Where(x => x.Id == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public bool Registration(User user)
        {
            if (user.Validate())
            {
                try
                {
                    return rep.AddUser(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public async Task<bool> SaveUser(UserView user)
        {
            if (user.Validate())
            {
                try
                {
                    return await rep.SaveUser(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public int DeleteUser(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.DeleteUser(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangePassword(int id, string oldpassword, string newpassword)
        {
            if (id.Validate() && !String.IsNullOrEmpty(oldpassword) && newpassword.IsCorrectPassword())
            {
                try
                {
                    return rep.ChangePassword(id, oldpassword, newpassword);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public async Task<bool> ChangeRole(UserView user)
        {
            if (user.Validate())
            {
                try
                {
                    return await rep.ChangeRole(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }
    }
}