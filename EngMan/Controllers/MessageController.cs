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

        //GET api/message/GetAllMessages
        [HttpGet]
        public IEnumerable<ReturnMessage> GetAllMessages()
        {
            return service.GetMessages(GetCurrentUserId());
        }

        //GET api/message/GetMesagesByUserId
        [HttpGet]
        public IEnumerable<ReturnMessage> GetMessagesByUserId(int otherUserId, int lastReceivedMessageId)
        {
            return service.GetMessagesByUserId(GetCurrentUserId(), otherUserId, lastReceivedMessageId);
        }

        //GET api/message/GetNewMessages
        [HttpGet]
        public IEnumerable<ReturnMessage> GetNewMessages()
        {
            return service.GetNewMessages(GetCurrentUserId());
        }

        //POST api/message/SendMessage
        [HttpPost]
        public bool SendMessage(Message mes)
        {
            return service.SendMessage(mes, GetCurrentUserId());
        }

        //POST api/message/ReadMessages
        [HttpPost]
        public IEnumerable<ReturnMessage> ReadMessages(int senderId)
        {
            return service.ReadMessages(senderId, GetCurrentUserId());
        }

        //DELETE api/message/DeleteMessage
        [HttpDelete]
        public string DeleteMessage(int id)
        {
            return service.DeleteMessage(id, GetCurrentUserId());
        }

        //get id of current user
        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
        }
    }
}
