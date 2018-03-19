using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
using EngMan.Repository;
using System.Threading.Tasks;
using EngMan.Extensions;
using System;
using System.Dynamic;
using Microsoft.Owin.Testing;
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

        public async Task<ExpandoObject> Login(UserLogin user)
        {
            var testServer = TestServer.Create<Startup>();
            var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", user.Email),
                    new KeyValuePair<string, string>("password", user.Password)
                };
            var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
            //Get token
            var tokenServiceResponse = await testServer.HttpClient.PostAsync(
                Startup.TokenPath, requestParamsFormUrlEncoded);
            //Read getting info to ExpandoObject
            var _user = await tokenServiceResponse.Content.ReadAsAsync<ExpandoObject>();
            return _user;
        }

        public User ValidateUser(string email, string password)
        {
            if (!email.IsEmail())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password.VerifyHashedPassword(password)) 
                    ?? throw new Exception("User is not registered");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserView GetUser(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return GetUserList().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ExpandoObject> Registration(User user)
        {
            if (!user.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.AddUser(user) 
                    ? await Login(new UserLogin { Email = user.Email, Password = user.Password })
                    : throw new Exception("This user is alredy exists");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SaveUser(UserView user)
        {
            if (!user.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return await rep.SaveUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteUser(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.DeleteUser(id) > 0 
                    ? "Delete completed successful"
                    : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            if (!id.Validate() && String.IsNullOrEmpty(oldpassword) && !newpassword.IsCorrectPassword())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.ChangePassword(id, oldpassword, newpassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangeRole(UserView user)
        {
            if (!user.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return await rep.ChangeRole(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}