﻿using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IMessageRepository
    {
        Message SendMessage(Message mes, int userId);

        IEnumerable<ReturnMessage> GetMessages(int userId);

        int DeleteMessage(int mesId, int userId);

        IEnumerable<ReturnMessage> ReadMessages(IEnumerable<Message> messages, int userId);
    }
}