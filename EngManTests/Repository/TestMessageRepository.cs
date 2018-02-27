using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using Moq;
using System.Data.Entity;

namespace EngManTests.Repository
{
    [TestClass]
    public class TestMessageRepository
    {
        private Mock<EFDbContext> context;
        private IMessageRepository rep;

        public TestMessageRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<Message>>();
            mockSet.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>((d) =>
            {
                return data.FirstOrDefault(x => x.MessageId == (int)d[0]);
            });
            context.Setup(x => x.Set<Message>()).Returns(mockSet.Object);
            rep = new MessageRepository(context.Object);
        }

        private IQueryable<Message> GenerateData()
        {
            var lst = new List<Message>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new Message
                {
                    BeneficiaryId = i,
                    SenderId = i+1,
                    CheckReadMes = false,
                    MessageId = i,
                    Text = "text" + i,
                    Time = DateTime.Now
                });
            }
            return lst.AsQueryable();
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Send_validModel()
        {
            var result = rep.SendMessage(new Message {
                MessageId = 1,
                SenderId = 1,
                BeneficiaryId = 2,
                CheckReadMes = false,
                Text = "text",
                Time = DateTime.Now
            }, 1);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Send_invalidModel()
        {
            var result = rep.SendMessage(new Message
            {
                MessageId = 1,
                SenderId = 1,
                BeneficiaryId = 2,
                CheckReadMes = false,
                Text = "text",
                Time = DateTime.Now
            }, 2);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Send_nullException()
        {
            try
            {
                rep.SendMessage(null, 1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Delete_validModel()
        {
            var result = rep.DeleteMessage(1, 1);
            Assert.AreEqual(1, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Delete_invalidModel()
        {
            var result = rep.DeleteMessage(0, 1);
            Assert.AreEqual(-1, result);
        }
    }
}
