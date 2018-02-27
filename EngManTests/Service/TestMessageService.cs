using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using EngMan.Service;
using System.Linq;
using Moq;

namespace EngManTests.Service
{
    [TestClass]
    public class TestMessageService
    {
        private IMessageRepository rep;
        private IMessageService service;

        public TestMessageService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var dataToReturn = GenerateData();
            var _rep = new Mock<IMessageRepository>();
            _rep.Setup(x => x.DeleteMessage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int>(x => x);
            _rep.Setup(x => x.GetMessages(It.IsAny<int>()))
                .Returns<int>(x => dataToReturn.Where(mes => mes.Sender.Id == x || mes.Beneficiary.Id == x));
            _rep.Setup(x => x.ReadMessages(It.IsAny<IEnumerable<Message>>(), It.IsAny<int>()))
                .Returns<int>(x => dataToReturn.Where(mes => mes.Sender.Id == x || mes.Beneficiary.Id == x)
                .Select(mes => new ReturnMessage {
                    MessageId = mes.MessageId,
                    Sender = mes.Sender,
                    Beneficiary = mes.Beneficiary,
                    Text = mes.Text,
                    CheckReadMes = true,
                    Time = mes.Time
                }));
            _rep.Setup(x => x.SendMessage(It.IsAny<Message>(), It.IsAny<int>()))
                .Returns(true);
            service = new MessageService(_rep.Object);
            rep = _rep.Object;
        }

        private IQueryable<ReturnMessage> GenerateData()
        {
            var lst = new List<ReturnMessage>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new ReturnMessage
                {
                    MessageId = i,
                    Sender = new UserView
                    {
                        Id = i,
                        Email = "email" + i,
                        FirstName = "fname" + i,
                        LastName = "lname" + i,
                        Role = "user"
                    },
                    Beneficiary = new UserView
                    {
                        Id = i+1,
                        Email = "email" + (i + 1),
                        FirstName = "fname" + (i + 1),
                        LastName = "lname" + (i + 1),
                        Role = "user"
                    },
                    CheckReadMes = false,
                    Text = "text" + i,
                    Time = DateTime.Now
                });
            }
            return lst.AsQueryable();
        }
    }
}
