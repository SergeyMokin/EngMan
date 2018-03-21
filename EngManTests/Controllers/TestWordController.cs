using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Controllers;
using EngMan.Service;
using System.Linq;
using Moq;

namespace EngManTests.Controllers
{
    [TestClass]
    public class TestWordController
    {
        private IWordService service;
        private WordController controller;

        public TestWordController()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _service = new Mock<IWordService>();
            _service.Setup(x => x.Add(It.IsAny<Word>()))
                .Returns(true);
            _service.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<int>(x => "Delete completed successful");
            _service.Setup(x => x.Edit(It.IsAny<Word>()))
                .Returns(true);
            _service.Setup(x => x.GetAll())
                .Returns(data);
            _service.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(id => data.FirstOrDefault(x => x.WordId == id));
            _service.Setup(x => x.GetAllCategories()).Returns(data.GroupBy(x => x.Category).Select(x => x.Key));
            _service.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data.Where(x => x.Category.Equals(str)));
            _service.Setup(x => x.GetTask(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns<string, string, bool>((str, arr, tr) => new MapWord { WordId = 1 });
            _service.Setup(x => x.VerificationCorrectness(It.IsAny<Word>(), It.IsAny<bool>()))
                .Returns(true);
            controller = new WordController(_service.Object);
            service = _service.Object;
        }

        private IQueryable<Word> GenerateData()
        {
            var lst = new List<Word>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new Word
                {
                    WordId = i,
                    Category = "Category" + i,
                    Original = "Original" + i,
                    Translate = "Translate" + i,
                    Transcription = "Transcription" + i
                });
            }
            return lst.AsQueryable();
        }

        [TestMethod]
        public void WordControllerTest_GetAllCategories()
        {
            var expected = service.GetAllCategories();
            var actual = controller.GetAllCategories();
            Assert.AreEqual(expected.Count(), actual.Count());
        }

        [TestMethod]
        public void WordControllerTest_GetAllTasks()
        {
            var expected = service.GetAll();
            var actual = controller.GetAllWords();
            Assert.AreEqual(expected.Count(), actual.Count());
        }

        [TestMethod]
        public void WordControllerTest_GetByCategory()
        {
            var expected = service.GetByCategory("Category1");
            var actual = controller.GetByCategory("Category1");
            Assert.AreEqual(expected.Count(), actual.Count());
        }

        [TestMethod]
        public void WordControllerTest_GetTaskById()
        {
            var expected = service.Get(1);
            var actual = controller.GetWordById(1);
            Assert.AreEqual(expected.WordId, actual.WordId);
        }

        [TestMethod]
        public void WordControllerTest_EditTask()
        {
            var expected = service.Edit(new Word());
            var actual = controller.EditWord(new Word());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordControllerTest_AddTask()
        {
            var expected = service.Add(new Word());
            var actual = controller.AddWord(new Word());
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordControllerTest_DeleteTask()
        {
            var actual = controller.DeleteWord(1);
            Assert.AreEqual("Delete completed successful", actual);
        }

        [TestMethod]
        public void WordControllerTest_GetTask()
        {
            var expected = service.GetTask("Category1", "1", true);
            var actual = controller.GetWordMap("Category1", "1", true);
            Assert.AreEqual(expected.WordId, actual.WordId);
        }

        [TestMethod]
        public void WordControllerTest_VerificationCorrectness()
        {
            var expected = service.VerificationCorrectness(new Word(), true);
            var actual = controller.VerificationCorrectness(new Word(), true);
            Assert.AreEqual(expected, actual);
        }
    }
}
