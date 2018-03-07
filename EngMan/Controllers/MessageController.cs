using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System;
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

        //GET api/message/GetAllMessages
        [HttpGet]
        public IHttpActionResult GetAllMessages()
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var list = service.GetMessages(int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
                if (list != null)
                {
                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //POST api/message/SendMessage
        [HttpPost]
        public IHttpActionResult SendMessage(Message mes)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                return Ok(service.SendMessage(mes, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/message/ReadMessages
        [HttpPost]
        public IHttpActionResult ReadMessages(IEnumerable<Message> mesgs)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var result = service.ReadMessages(mesgs, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
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

        //DELETE api/message/DeleteMessage
        [HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                var _id = service.DeleteMessage(id, int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value));
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
    }
}
