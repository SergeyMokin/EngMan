using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
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
            if (HttpContext.Current == null)
            {
                return BadRequest();
            }
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var list = service.GetMessages(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                    if (list != null)
                    {
                        return Ok(list);
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
        public IHttpActionResult SendMessage(Message mes)
        {
            if (HttpContext.Current == null)
            {
                return BadRequest();
            }
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    return Ok(service.SendMessage(mes, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value)));
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult ReadMessages(IEnumerable<Message> mesgs)
        {
            if (HttpContext.Current == null)
            {
                return BadRequest();
            }
            try
            {
                var result = service.ReadMessages(mesgs, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            if (HttpContext.Current == null)
            {
                return BadRequest();
            }
            if (HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() > 0)
            {
                try
                {
                    var _id = service.DeleteMessage(id, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                    if (_id != -1)
                    {
                        return Ok("Delete completed successful");
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
