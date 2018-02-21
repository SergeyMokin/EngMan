using System.Collections.Generic;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using EngMan.Extensions;
using System.Net.Http;

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
            if (mes.Validate() && userId.Validate())
            {
                try
                {
                    var result = rep.SendMessage(mes, userId);
                    return result;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<ReturnMessage> GetMessages(int userId)
        {
            if (userId.Validate())
            {
                try
                {
                    var result = rep.GetMessages(userId);
                    return result;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<ReturnMessage> ReadMessages(IEnumerable<Message> messages, int userId)
        {
            if (userId.Validate() && messages != null)
            {
                if (messages.Count() > 0)
                {
                    foreach (var el in messages)
                    {
                        if (!el.Validate())
                        {
                            throw new HttpRequestException("Invalid model");
                        }
                    }
                }
                try
                {
                    var result = rep.ReadMessages(messages, userId);
                    return result;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public int DeleteMessage(int mesId, int userId)
        {
            if (userId.Validate() && mesId.Validate())
            {
                try
                {
                    var result = rep.DeleteMessage(mesId, userId);
                    return result;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }
    }
}