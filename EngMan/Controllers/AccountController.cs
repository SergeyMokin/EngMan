using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Linq;
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

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Registration(User user) {
            try
            {
                var _user = service.Registration(user);
                if (_user != null)
                {
                    return Ok(_user);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        
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
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetUserDictionary()
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var result = serviceDictionary.Get(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult AddWordToDictionary(UserWord word)
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var result = serviceDictionary.Add(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), word);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteWordFromDictionary(int id)
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var result = serviceDictionary.Delete(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), id);
                    if (result > 0)
                    {
                        return Ok(result);
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetUserData()
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var user = service.GetUser(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                    if (user != null)
                    {
                        return Ok(user);
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

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
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        
        [HttpPut]
        public async Task<IHttpActionResult> EditUser(UserView user)
        {
            try
            {
                var _user = await service.SaveUser(user);
                if (_user != null)
                {
                    return Ok(_user);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                var _id = service.DeleteUser(id);
                if (_id != -1)
                {
                    return Ok(_id);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        
        [HttpPost]
        public IHttpActionResult LogOut()
        {
            try
            {
                HttpContext.Current.GetOwinContext().Authentication.SignOut();
                return Ok();
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> ChangeRole(UserView user)
        {
            try
            {
                var _user = await service.ChangeRole(user);
                if (_user != null)
                {
                    return Ok(_user);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpPut]
        public IHttpActionResult ChangePassword(string oldpassword, string newpassword)
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var _user = service.ChangePassword(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value), oldpassword, newpassword);
                    if (_user != null)
                    {
                        return Ok(_user);
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }
    }
}
