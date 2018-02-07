using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IMessageService
    {
        Message SendMessage(Message mes, int userId);

        IEnumerable<ReturnMessage> GetMessages(int userId);

        int DeleteMessage(int mesId, int userId);
    }
}