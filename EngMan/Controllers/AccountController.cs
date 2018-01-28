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

        public AccountController(IUserService _service)
        {
            service = _service;
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Registration(User user) {
            var _user = service.Registration(user);
            if (_user != null)
            {
                return Ok(_user);
            }
            return NotFound();
        }
        
        [HttpGet]
        public IHttpActionResult GetAllUsers()
        {
            var users = service.GetUserList();
            if (users != null)
            {
                return Ok(users);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetUserData()
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                var user = service.GetUser(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            var user = service.GetUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        
        [HttpPut]
        public async Task<IHttpActionResult> EditUser(UserView user)
        {
            var _user = await service.SaveUser(user);
            if (_user != null)
            {
                return Ok(_user);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var _id = service.DeleteUser(id);
            if (_id != -1)
            {
                return Ok(_id);
            }
            return NotFound();
        }
        
        [HttpGet]
        public IHttpActionResult LogOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut();    
            return Ok();
        }
    }
}
