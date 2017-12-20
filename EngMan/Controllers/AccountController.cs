using System.Web.Http;
using EngMan.Account;
using System.Web.Security;
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
        public IHttpActionResult Login(AdminAccountModel model)
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
