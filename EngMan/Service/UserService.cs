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
            return rep.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password.VerifyHashedPassword(password));
        }

        public UserView GetUser(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return GetUserList().Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<ExpandoObject> Registration(User user)
        {
            if (!user.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.AddUser(user)
                    ? await Login(new UserLogin { Email = user.Email, Password = user.Password })
                    : throw new Exception("This user is alredy exists");
        }

        public async Task<bool> SaveUser(UserView user)
        {
            if (!user.Validate())
            {
                throw new Exception("Invalid model");
            }
            return await rep.SaveUser(user);
        }

        public string DeleteUser(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.DeleteUser(id) > 0
                    ? "Delete completed successful"
                    : null;
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

        public bool ChangePassword(int id, string oldpassword, string newpassword)
        {
            if (!id.Validate() && String.IsNullOrEmpty(oldpassword) && !newpassword.IsCorrectPassword())
            {
                throw new Exception("Invalid model");
            }
            return rep.ChangePassword(id, oldpassword, newpassword);
        }

        public async Task<bool> ChangeRole(UserView user)
        {
            if (!user.Validate())
            {
                throw new Exception("Invalid model");
            }
            return await rep.ChangeRole(user);
        }
    }
}