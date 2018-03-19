using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IMessageService
    {
        //send message to another user
        bool SendMessage(Message mes, int userId);

        //get messages of user
        IEnumerable<ReturnMessage> GetMessages(int userId);

        //delete message of user from db
        string DeleteMessage(int mesId, int userId);

        //read unread messages
        bool ReadMessages(int senderId, int beneficiaryId);

        //get messages with current user and other user
        IEnumerable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId, int lastReceivedMessageId);

        //get new messages
        IEnumerable<ReturnMessage> GetNewMessages(int userId);
    }
}