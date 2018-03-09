using System.Collections.Generic;
using System.Data.SqlClient;
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

        public IEnumerable<ReturnMessage> ReadMessages(IEnumerable<Message> messages, int userId)
        {
            if (messages == null)
            {
                return GetMessages(userId);
            }
            foreach (var el in messages)
            {
                if (!el.CheckReadMes)
                {
                    var entity = context.Messages.Find(el.MessageId);
                    if (entity != null)
                    {
                        entity.CheckReadMes = true;
                    }
                }
            }
            context.SaveChanges();
            return GetMessages(userId);
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

        public IEnumerable<ReturnMessage> GetMessages(int userId)
        {
            return context.Database.SqlQuery<ReturnMessageWithTheQueryBD>(
                    @"SELECT [MessageId]
                          , B.[Id] [SenderId]
	                      , B.[FirstName] [SenderFirstName]
	                      , B.[LastName] [SenderLastName]
	                      , B.[Email] [SenderEmail]
	                      , B.[Role] [SenderRole]
                          , A.[Id] [BeneficiaryId]
	                      , A.[FirstName] [BeneficiaryFirstName]
	                      , A.[LastName] [BeneficiaryLastName]
	                      , A.[Email] [BeneficiaryEmail]
	                      , A.[Role] [BeneficiaryRole]
                          ,[Text]
                          ,[Time]
                          ,[CheckReadMes]
                      FROM [EngMan].[dbo].[Messages]
                      JOIN [EngMan].[dbo].[Users] A ON A.[Id] = [EngMan].[dbo].[Messages].[BeneficiaryId]
                      JOIN [EngMan].[dbo].[Users] B ON B.[Id] = [EngMan].[dbo].[Messages].[SenderId]
                      WHERE [EngMan].[dbo].[Messages].[BeneficiaryId] = @userId OR [EngMan].[dbo].[Messages].[SenderId] = @userId",
                new SqlParameter("userId", userId))
                .Select(x => new ReturnMessage
                {
                    MessageId = x.MessageId,
                    CheckReadMes = x.CheckReadMes,
                    Text = x.Text,
                    Time = x.Time,
                    Sender = new UserView
                    {
                        Id = x.SenderId,
                        FirstName = x.SenderFirstName,
                        LastName = x.SenderLastName,
                        Email = x.SenderEmail,
                        Role = x.SenderRole
                    },
                    Beneficiary = new UserView
                    {
                        Id = x.BeneficiaryId,
                        FirstName = x.BeneficiaryFirstName,
                        LastName = x.BeneficiaryLastName,
                        Email = x.BeneficiaryEmail,
                        Role = x.BeneficiaryRole
                    }
                });
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