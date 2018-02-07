using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Web;
using System.Linq;

namespace EngMan.Controllers
{
    [Authorize]
    public class MessageController : ApiController
    {
        private IMessageService service;

        public MessageController(IMessageService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetAllMessages()
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                var list = service.GetMessages(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (list != null)
                {
                    return Ok(list);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult SendMessage(Message mes)
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                if(mes != null)
                {
                    return Ok(service.SendMessage(mes, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value)));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                if (id > 0)
                {
                    return Ok(service.DeleteMessage(id, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value)));
                }
            }
            return NotFound();
        }
    }
}
