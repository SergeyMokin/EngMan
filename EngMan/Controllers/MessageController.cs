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
        private int currentUserId;

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
                currentUserId = int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
                var list = service.GetMessages(currentUserId);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/message/GetMesagesByUserId
        [HttpGet]
        public IHttpActionResult GetMessagesByUserId(int otherUserId)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                currentUserId = int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
                var list = service.GetMessagesByUserId(currentUserId, otherUserId);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/message/GetNewMessages
        [HttpGet]
        public IHttpActionResult GetNewMessages()
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                currentUserId = int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
                var list = service.GetNewMessages(currentUserId);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                currentUserId = int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
                return Ok(service.SendMessage(mes, currentUserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/message/ReadMessages
        [HttpPost]
        public IHttpActionResult ReadMessages(int senderId)
        {
            if (HttpContext.Current == null || HttpContext.Current.GetOwinContext().Authentication.User.Claims.Count() == 0)
            {
                return BadRequest("Invalid user");
            }
            try
            {
                currentUserId = int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
                var result = service.ReadMessages(senderId, currentUserId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                currentUserId = int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
                var _id = service.DeleteMessage(id, currentUserId);
                if (_id != -1)
                {
                    return Ok("Delete completed successful");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
