using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IMessageRepository
    {
        bool SendMessage(Message mes, int userId);

        IEnumerable<ReturnMessage> GetMessages(int userId);

        int DeleteMessage(int mesId, int userId);

        int ReadMessages(int senderId, int beneficiaryId);

        IEnumerable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId, int lastReceivedMessageId);

        IEnumerable<ReturnMessage> GetNewMessages(int userId);
    }
}