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

        private IQueryable<ReturnMessage> GetAll()
        {
            return context.Messages
                .Join(context.Users,
                    message => message.BeneficiaryId,
                    user => user.Id,
                    (message, user) => new { Message = message, Beneficiary = user })
                .Join(context.Users,
                    message => message.Message.SenderId,
                    user => user.Id,
                    (message, user) => new { message.Message, message.Beneficiary, Sender = user })
                .Select(x => new ReturnMessage
                {
                    MessageId = x.Message.MessageId,
                    CheckReadMes = x.Message.CheckReadMes,
                    Text = x.Message.Text,
                    Time = x.Message.Time,
                    Sender = new UserView
                    {
                        Id = x.Sender.Id,
                        FirstName = x.Sender.FirstName,
                        LastName = x.Sender.LastName,
                        Email = x.Sender.Email,
                        Role = x.Sender.Role
                    },
                    Beneficiary = new UserView
                    {
                        Id = x.Beneficiary.Id,
                        FirstName = x.Beneficiary.FirstName,
                        LastName = x.Beneficiary.LastName,
                        Email = x.Beneficiary.Email,
                        Role = x.Beneficiary.Role
                    }
                });
        }

        public int ReadMessages(int senderId, int beneficiaryId)
        {
            var query = context.Messages.Where(x => x.SenderId == senderId && x.BeneficiaryId == beneficiaryId && !x.CheckReadMes);
            var count = 0;
            foreach (var el in query)
            {
                el.CheckReadMes = true;
                count++;
            }
            context.SaveChanges();
            return count;
        }

        public bool SendMessage(Message mes, int userId)
        {
            if (mes == null)
            {
                throw new System.ArgumentNullException();
            }
            if (mes.SenderId != userId)
            {
                return false;
            }
            mes.CheckReadMes = false;
            mes.Time = System.DateTime.Now;
            mes.SenderId = userId;
            context.Messages.Add(mes);
            context.SaveChanges();
            return true;
        }

        public IQueryable<ReturnMessage> GetMessages(int userId)
        {
            return GetAll()
                .Where(x => x.Beneficiary.Id == userId || x.Sender.Id == userId)
                .OrderByDescending(x => x.MessageId);
        }

        public IQueryable<ReturnMessage> GetNewMessages(int userId)
        {
            var messages = GetAll();
            var query = messages
                .Where(x => !x.CheckReadMes
                && x.Beneficiary.Id == userId
                && messages.Where(mes => mes.Sender.Id == x.Sender.Id).Max(mes => mes.MessageId) == x.MessageId);
            return query.OrderByDescending(x => x.MessageId);
        }

        public IQueryable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId, int lastReceivedMessageId)
        {
            const int minRecivedId = 1;
            var sqlQuery = GetAll()
                .Where(x => 
                (x.Sender.Id == currentUserId 
                || x.Beneficiary.Id == currentUserId) 
                && (x.Sender.Id == otherUserId 
                || x.Beneficiary.Id == otherUserId));

            if (lastReceivedMessageId > minRecivedId)
            {
                sqlQuery = sqlQuery.Where(x => x.MessageId < lastReceivedMessageId);
            }
            
            return sqlQuery.OrderByDescending(x => x.MessageId).Take(100);
        }

        public int DeleteMessage(int mesId, int userId)
        {
            var entity = context.Messages.Where(x => x.MessageId == mesId && (x.SenderId == userId || x.BeneficiaryId == userId)).FirstOrDefault();
            if (entity == null)
            {
                return -1;
            }
            context.Messages.Remove(entity);
            context.SaveChanges();
            return mesId;
        }
    }
}