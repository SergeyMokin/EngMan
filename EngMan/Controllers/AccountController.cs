using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Linq;
using Microsoft.Owin.Testing;
using System.Collections.Generic;
using System.Dynamic;
using System;

namespace EngMan.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IUserService service;
        private readonly IUserDictionaryService serviceDictionary;

        public AccountController(IUserService _service, IUserDictionaryService _serviceDictionary)
        {
            service = _service;
            serviceDictionary = _serviceDictionary;
        }

        //POST api/account/login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Login(UserLogin user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user data");
                }

                //Get token
                var testServer = TestServer.Create<Startup>();
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", user.Email),
                    new KeyValuePair<string, string>("password", user.Password)
                };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await testServer.HttpClient.PostAsync(
                    Startup.TokenPath, requestParamsFormUrlEncoded);

                //Parse token
                var data = await tokenServiceResponse.Content.ReadAsAsync<ExpandoObject>();
                return Json(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/account/registration
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Registration(User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }
            try
            {
                var _user = service.Registration(user);
                if (_user)
                {
                    return await Login(new UserLogin { Email = user.Email, Password = user.Password });
                }
                return BadRequest("Unsuccessful registration");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        //GET api/account/GetAllUsers
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var users = service.GetUserList();
                if (users != null)
                {
                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/account/GetAllCategoriesOfDictionary
        [HttpGet]
        public IHttpActionResult GetAllCategoriesOfDictionary()
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var result = serviceDictionary.GetAllCategories(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/account/GetByCategoryDictionary
        [HttpGet]
        public IHttpActionResult GetByCategoryDictionary(string category)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var result = serviceDictionary.GetByCategory(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), category);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/account/GetUserDictionary
        [HttpGet]
        public IHttpActionResult GetUserDictionary()
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var result = serviceDictionary.Get(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //POST api/account/AddWordToDictionary
        [HttpPost]
        public IHttpActionResult AddWordToDictionary(UserWord word)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var result = serviceDictionary.Add(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), word);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE api/account/DeleteWordFromDictionary
        [HttpDelete]
        public IHttpActionResult DeleteWordFromDictionary(int id)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var result = serviceDictionary.Delete(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), id);
                if (result > 0)
                {
                    return Ok("Delete completed successful");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/account/GetUserData
        [HttpGet]
        public IHttpActionResult GetUserData()
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var user = service.GetUser(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/account/GetUserById
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                var user = service.GetUser(id);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //PUT api/account/EditUser
        [HttpPut]
        public async Task<IHttpActionResult> EditUser(UserView user)
        {
            try
            {
                var _user = await service.SaveUser(user);
                return Ok(_user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE api/account/DeleteUser
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                var _id = service.DeleteUser(id);
                if (_id != -1)
                {
                    return Ok("Delete completed successful");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        
        //POST api/account/LogOut
        [HttpPost]
        public IHttpActionResult LogOut()
        {
            if (HttpContext.Current == null)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                HttpContext.Current.GetOwinContext().Authentication.SignOut();
                return Ok("Successfully completed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT api/account/ChangeRole
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> ChangeRole(UserView user)
        {
            try
            {
                var _user = await service.ChangeRole(user);
                return Ok(_user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT api/account/ChangePassword
        [HttpPut]
        public IHttpActionResult ChangePassword(string oldpassword, string newpassword)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var _user = service.ChangePassword(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), oldpassword, newpassword);
                return Ok(_user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
