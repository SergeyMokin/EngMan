using System.Collections.Generic;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;

namespace EngMan.Service
{
    public class MessageService: IMessageService
    {
        private IMessageRepository rep;
        public IUserService usserv;

        public MessageService(IMessageRepository _rep)
        {
            rep = _rep;
        }

        public Message SendMessage(Message mes, int userId)
        {
            return rep.SendMessage(mes, userId);
        }

        public IEnumerable<ReturnMessage> GetMessages(int userId)
        {
            return rep.GetMessages(userId);
        }

        public IEnumerable<ReturnMessage> ReadMessages(IEnumerable<Message> messages, int userId) {
            return rep.ReadMessages(messages, userId);
        }

        public int DeleteMessage(int mesId, int userId)
        {
            return rep.DeleteMessage(mesId, userId);
        }
    }
}