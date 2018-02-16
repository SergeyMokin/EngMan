using System.Collections.Generic;
using System.Linq;
using EngMan.Models;

namespace EngMan.Hubs
{
    public class Chat: Microsoft.AspNet.SignalR.Hub
    {
        static List<UserConnect> Users = new List<UserConnect>();

        // Уведомление о сообщении
        public void Send(Message mes)
        {
            var user = Users.FirstOrDefault(x => x.Id == mes.BeneficiaryId);
            if (user != null)
            {
                Clients.Client(user.ConnectionId).onUpdateMessages();
            }
        }

    }
}