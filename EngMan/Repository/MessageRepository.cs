using System.Collections.Generic;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private EFDbContext context;

        public MessageRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Message> ReadMessages(IEnumerable<Message> messages){
            if (messages != null)
            {
                foreach (var el in messages)
                {
                    if (el != null)
                    {
                        var entity = context.Messages.Find(el.MessageId);
                        if (entity != null)
                        {
                            entity.CheckReadMes = true;
                        }
                    }
                }
                context.SaveChanges();
            }
            return messages;
        }

        public Message SendMessage(Message mes, int userId)
        {
            if (mes.SenderId == userId)
            {
                context.Messages.Add(mes);
                context.SaveChanges();
                mes.MessageId = context.Messages.Where(x => x.SenderId == userId).ToArray().Last().MessageId;
            }
            return mes;
        }

        public IEnumerable<Message> GetMessages(int userId)
        {
            return context.Messages.Where(x => x.SenderId == userId || x.BeneficiaryId == userId);
        }

        public int DeleteMessage(int mesId, int userId)
        {
            var entity = context.Messages.Where(x => x.MessageId == mesId && (x.SenderId == userId || x.BeneficiaryId == userId)).FirstOrDefault();
            if (entity != null)
            {
                context.Messages.Remove(entity);
            }
            else
            {
                return 0;
            }
            context.SaveChanges();
            return mesId;
        }
    }
}