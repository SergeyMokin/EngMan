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
        
        public void Connect(UserView user)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id) && !Users.Any(x => x.Id == user.Id))
            {
                var usr = new UserConnect
                {
                    ConnectionId = id,
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                };
                Users.Add(usr);
            }
        }

        public void Disconnect() {
            OnDisconnected(true);
        }
        
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var usr = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (usr != null)
            {
                Users.Remove(usr);
            }

            return base.OnDisconnected(stopCalled);
        }

    }
}