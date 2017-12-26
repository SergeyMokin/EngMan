using System.Web.Http;
using EngMan.Account;
using EngMan.Models;
namespace EngMan.Controllers
{
    public class AccountController : ApiController
    {
        IAuthProvider provider;

        public AccountController(IAuthProvider _provider)
        {
            provider = _provider;
        }

        [HttpPost]
        public IHttpActionResult Login(AdminAccount model)
        {
            if (provider.Authenticate(model.UserName, model.Password))
            {
                return Ok("authorize");
            }
            else
            {
                return Ok("incorrect login or password");
            }
        }
    }
}
