using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using EngMan.Models;
using System.Web.Http;

namespace EngMan
{
    
    public class ChatHub : Hub
    {
        static List<UserConnect> Users = new List<UserConnect>();

        // Отправка сообщений
        public void Send()
        {
            Clients.All.addMessage(true);
        }

        // Подключение нового пользователя
        public void Connect(UserView user)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.Id == user.Id) && !Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new UserConnect {
                    Id = user.Id,
                    ConnectionId = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(user);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(user);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var user = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                Users.Remove(user);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(user.Email);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}