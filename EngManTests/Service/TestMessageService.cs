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
                .Returns<int, int>((id, userid) => id);
            _rep.Setup(x => x.GetMessages(It.IsAny<int>()))
                .Returns<int>(x => dataToReturn.Where(mes => mes.Sender.Id == x || mes.Beneficiary.Id == x));
            _rep.Setup(x => x.GetNewMessages(It.IsAny<int>()))
                .Returns<int>(x => dataToReturn.Where(mes => mes.Sender.Id == x || mes.Beneficiary.Id == x));
            _rep.Setup(x => x.GetMessagesByUserId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int, int>((x,y,z) => dataToReturn.Where(mes => mes.Sender.Id == x || mes.Beneficiary.Id == x));
            _rep.Setup(x => x.ReadMessages(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((messages, x) => dataToReturn.Where(mes => mes.Sender.Id == x || mes.Beneficiary.Id == x)
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

        [TestMethod]
        public void MessageServiceTest_SendMessage_valid()
        {
            var model = new Message
            {
                MessageId = 0,
                SenderId = 1,
                BeneficiaryId = 2,
                CheckReadMes = false,
                Text = "text",
                Time = DateTime.Now
            };
            var expected = rep.SendMessage(model, 1);
            var actual = service.SendMessage(model, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageServiceTest_SendMessage_invalid()
        {
            var model = new Message
            {
                MessageId = 0,
                SenderId = -1,
                BeneficiaryId = -2,
                CheckReadMes = false,
                Text = "text",
                Time = DateTime.Now
            };
            try
            {
                service.SendMessage(model, 1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }
        
        [TestMethod]
        public void MessageServiceTest_GetMessages_valid()
        {
            var expected = rep.GetMessages(1).Count();
            var actual = service.GetMessages(1).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageServiceTest_GetMessages_invalid()
        {
            try
            {
                service.GetMessages(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void MessageServiceTest_GetNewMessages_valid()
        {
            var expected = rep.GetNewMessages(1).Count();
            var actual = service.GetNewMessages(1).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageServiceTest_GetNewMessages_invalid()
        {
            try
            {
                service.GetNewMessages(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void MessageServiceTest_GetMessagesByUserId_valid()
        {
            var expected = rep.GetMessagesByUserId(1,1,1).Count();
            var actual = service.GetMessagesByUserId(1,1,1).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageServiceTest_GetMessagesByUserId_invalid()
        {
            try
            {
                service.GetMessagesByUserId(-1, -1, -1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void MessageServiceTest_ReadMessagess_valid()
        {
            var expected = rep.ReadMessages(1, 1).Count();
            var actual = service.ReadMessages(1, 1).Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageServiceTest_ReadMessages_invalid()
        {
            try
            {
                service.ReadMessages(-1, -1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void MessageServiceTest_DeleteMessage_valid()
        {
            var expected = "Delete completed successful";
            var actual = service.DeleteMessage(1, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MessageServiceTest_DeleteMessage_invalid()
        {
            try
            {
                service.DeleteMessage(-1, -1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }
    }
}
