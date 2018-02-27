using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Controllers;
using EngMan.Service;
using System.Linq;
using Moq;
using System.Web.Http.Results;
using System.Threading.Tasks;
namespace EngManTests.Controllers
{
    [TestClass]
    public class TestSentenceTaskController
    {
        private ISentenceTaskService service;
        private SentenceTaskController controller;

        public TestSentenceTaskController()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _service = new Mock<ISentenceTaskService>();
            _service.Setup(x => x.Add(It.IsAny<SentenceTask>()))
                .Returns(true);
            _service.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<int>(x => Task.FromResult(x));
            _service.Setup(x => x.Edit(It.IsAny<SentenceTask>()))
                .Returns(Task.FromResult(true));
            _service.Setup(x => x.Get())
                .Returns(data);
            _service.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<int>(id => data.FirstOrDefault(x => x.SentenceTaskId == id));
            _service.Setup(x => x.GetAllCategories()).Returns(data.GroupBy(x => x.Category).Select(x => x.Key));
            _service.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data.Where(x => x.Category.Equals(str)));
            _service.Setup(x => x.GetTask(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((str, arr) => data.FirstOrDefault(x => x.Category.Equals(str)));
            _service.Setup(x => x.VerificationCorrectness(It.IsAny<SentenceTask>()))
                .Returns(true);
            controller = new SentenceTaskController(_service.Object);
            service = _service.Object;
        }

        private IQueryable<SentenceTask> GenerateData()
        {
            var lst = new List<SentenceTask>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new SentenceTask
                {
                    SentenceTaskId = i,
                    Sentence = "Sentence" + i,
                    Category = "Category" + i,
                    Translate = "Translate" + i
                });
            }
            return lst.AsQueryable();
        }

        [TestMethod]
        public void SentenceTaskControllerTest_GetAllCategories()
        {
            var expected = service.GetAllCategories();
            var actual = controller.GetAllCategories() as OkNegotiatedContentResult<IEnumerable<string>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void SentenceTaskControllerTest_GetAllTasks()
        {
            var expected = service.Get();
            var actual = controller.GetAllTasks() as OkNegotiatedContentResult<IEnumerable<SentenceTask>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void SentenceTaskControllerTest_GetByCategory()
        {
            var expected = service.GetByCategory("Category1");
            var actual = controller.GetByCategory("Category1") as OkNegotiatedContentResult<IEnumerable<SentenceTask>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void SentenceTaskControllerTest_GetTaskById()
        {
            var expected = service.GetById(1);
            var actual = controller.GetTaskById(1) as OkNegotiatedContentResult<SentenceTask>;
            Assert.AreEqual(expected.SentenceTaskId, actual.Content.SentenceTaskId);
        }

        [TestMethod]
        public void SentenceTaskControllerTest_EditTask()
        {
            var expected = service.Edit(new SentenceTask()).Result;
            var actual = controller.EditTask(new SentenceTask()).Result as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void SentenceTaskControllerTest_AddTask()
        {
            var expected = service.Add(new SentenceTask());
            var actual = controller.AddTask(new SentenceTask()) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void SentenceTaskControllerTest_DeleteTask()
        {
            var actual = controller.DeleteTask(1).Result as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Delete completed successful", actual.Content);
        }

        [TestMethod]
        public void SentenceTaskControllerTest_GetTask()
        {
            var expected = service.GetTask("Category1", "1");
            var actual = controller.GetTask("Category1", "1") as OkNegotiatedContentResult<SentenceTask>;
            Assert.AreEqual(expected.SentenceTaskId, actual.Content.SentenceTaskId);
        }

        [TestMethod]
        public void SentenceTaskControllerTest_VerificationCorrectness()
        {
            var expected = service.VerificationCorrectness(new SentenceTask());
            var actual = controller.VerificationCorrectness(new SentenceTask()) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }
    }
}
