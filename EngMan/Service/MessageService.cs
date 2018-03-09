using System.Collections.Generic;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using EngMan.Extensions;
using System.Net.Http;
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
            try
            {
                return rep.SendMessage(mes, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ReturnMessage> GetMessages(int userId)
        {
            if (!userId.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.GetMessages(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IEnumerable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId)
        {
            if (!currentUserId.Validate() || !otherUserId.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.GetMessagesByUserId(currentUserId, otherUserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ReturnMessage> GetNewMessages(int userId)
        {
            if (!userId.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.GetNewMessages(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ReturnMessage> ReadMessages(int senderId, int beneficiaryId)
        {
            if (!senderId.Validate() && !beneficiaryId.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.ReadMessages(senderId, beneficiaryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteMessage(int mesId, int userId)
        {
            if (!userId.Validate() && !mesId.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.DeleteMessage(mesId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}