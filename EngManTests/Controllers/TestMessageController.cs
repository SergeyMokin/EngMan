﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Controllers;
using EngMan.Service;
using Moq;
using System.Web.Http.Results;

namespace EngManTests.Controllers
{
    [TestClass]
    public class TestMessageController
    {
        private IMessageService service;
        private MessageController controller;

        public TestMessageController()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var _service = new Mock<IMessageService>();
            controller = new MessageController(_service.Object);
            service = _service.Object;
        }

        [TestMethod]
        public void MessageControllerTest_SendMessage()
        {
            var actual = controller.SendMessage(null);
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void MessageControllerTest_GetMessages()
        {
            var actual = controller.GetAllMessages();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }


        [TestMethod]
        public void MessageControllerTest_ReadMessagess()
        {
            var actual = controller.ReadMessages(null);
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void MessageControllerTest_DeleteMessage()
        {
            var actual = controller.DeleteMessage(0);
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }
    }
}