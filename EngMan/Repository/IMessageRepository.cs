using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IMessageRepository
    {
        Message SendMessage(Message mes, int userId);

        IEnumerable<Message> GetMessages(int userId);

        int DeleteMessage(int mesId, int userId);
    }
}