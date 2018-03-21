using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using EngMan.Extensions;
using System;

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

        public bool SendMessage(Message mes, int userId)
        {
            if (!mes.Validate() && !userId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.SendMessage(mes, userId);
        }

        public IQueryable<ReturnMessage> GetMessages(int userId)
        {
            if (!userId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.GetMessages(userId);
        }


        public IQueryable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId, int lastReceivedMessageId)
        {
            if (!currentUserId.Validate() || !otherUserId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.GetMessagesByUserId(currentUserId, otherUserId, lastReceivedMessageId);
        }

        public IQueryable<ReturnMessage> GetNewMessages(int userId)
        {
            if (!userId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.GetNewMessages(userId);
        }

        public bool ReadMessages(int senderId, int beneficiaryId)
        {
            if (!senderId.Validate() && !beneficiaryId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.ReadMessages(senderId, beneficiaryId) > 0
                    ? true
                    : false;
        }

        public string DeleteMessage(int mesId, int userId)
        {
            if (!userId.Validate() && !mesId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.DeleteMessage(mesId, userId) > 0
                    ? "Delete completed successful"
                    : null; ;
        }
    }
}