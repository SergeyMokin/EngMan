using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Controllers;
using EngMan.Service;
using Moq;

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
    }
}
