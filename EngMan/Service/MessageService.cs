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
                var result = rep.SendMessage(mes, userId);
                return result;
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
                var result = rep.GetMessages(userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ReturnMessage> ReadMessages(IEnumerable<Message> messages, int userId)
        {
            if (!userId.Validate() && messages == null)
            {
                throw new Exception("Invalid model");
            }
            foreach (var el in messages)
            {
                if (!el.Validate())
                {
                    throw new Exception("Invalid model");
                }
            }
            try
            {
                var result = rep.ReadMessages(messages, userId);
                return result;
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
                var result = rep.DeleteMessage(mesId, userId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}