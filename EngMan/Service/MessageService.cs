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

        public MessageService(IMessageRepository _rep, IUserService _usserv)
        {
            rep = _rep;
            usserv = _usserv;
        }

        public Message SendMessage(Message mes, int userId)
        {
            return rep.SendMessage(mes, userId);
        }

        public IEnumerable<ReturnMessage> GetMessages(int userId)
        {
            return rep.GetMessages(userId).ToList().Select(x => new ReturnMessage
            {
                MessageId = x.MessageId,
                Sender = usserv.GetUser(x.SenderId),
                Beneficiary = usserv.GetUser(x.BeneficiaryId),
                Text = x.Text,
                Time = x.Time,
                CheckReadMes = x.CheckReadMes
            });
        }

        public IEnumerable<ReturnMessage> ReadMessages(IEnumerable<Message> messages) {
            return rep.ReadMessages(messages).ToList().Select(x => new ReturnMessage
            {
                MessageId = x.MessageId,
                Sender = usserv.GetUser(x.SenderId),
                Beneficiary = usserv.GetUser(x.BeneficiaryId),
                Text = x.Text,
                Time = x.Time,
                CheckReadMes = x.CheckReadMes
            });
        }
        public int DeleteMessage(int mesId, int userId)
        {
            return rep.DeleteMessage(mesId, userId);
        }
    }
}