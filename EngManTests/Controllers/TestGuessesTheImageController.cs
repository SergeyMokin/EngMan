using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Controllers;
using EngMan.Service;
using System.Linq;
using Moq;
using System.Web.Http.Results;

namespace EngManTests.Service
{
    [TestClass]
    public class TestGuessesTheImageController
    {
        private IGuessesTheImageService service;
        private GuessesTheImageController controller;

        public TestGuessesTheImageController()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var dataWords = GenerateDataWords();
            var _service = new Mock<IGuessesTheImageService>();
            _service.Setup(x => x.VerificationCorrectness(It.IsAny<GuessesTheImageToReturn>()))
               .Returns(true);
            _service.Setup(x => x.Add(It.IsAny<GuessesTheImageToAdd>()))
                .Returns(true);
            _service.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<int>(x => x);
            _service.Setup(x => x.Edit(It.IsAny<GuessesTheImageToAdd>()))
                .Returns(true);
            _service.Setup(x => x.GetAll())
                .Returns(data.Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(y => x.WordId == y.WordId) }).AsEnumerable());
            _service.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(y => data.Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(word => word.WordId == word.WordId) }).FirstOrDefault(x => x.Id == y));
            _service.Setup(x => x.GetAllCategories()).Returns(dataWords.GroupBy(x => x.Category).Select(x => x.Key));
            _service.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data
                                        .Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(y => x.WordId == y.WordId) })
                                        .Where(x => x.Word.Category.Equals(str))
                                        .AsEnumerable());
            _service.Setup(x => x.GetTask(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((str, arr) => data
                                        .Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(y => x.WordId == y.WordId) })
                                        .Where(x => x.Word.Category.Equals(str))
                                        .AsEnumerable().FirstOrDefault());
            controller = new GuessesTheImageController(_service.Object);
            service = _service.Object;
        }

        private IQueryable<GuessesTheImage> GenerateData()
        {
            var lst = new List<GuessesTheImage>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new GuessesTheImage
                {
                    WordId = i,
                    Id = i,
                    Path = "path"+i
                });
            }
            return lst.AsQueryable();
        }

        private IQueryable<Word> GenerateDataWords()
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
        public void GuessesTheImageControllerTest_Add()
        {
            var model = new GuessesTheImageToAdd
            {
                Id = 0,
                Image = new Image
                {
                    Data = "asdfasdfsafasf",
                    Name = "asdf.jpg"
                },
                WordId = 1
            };
            var expected = service.Add(model);
            var actual = controller.AddTask(model) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_Edit()
        {
            var model = new GuessesTheImageToAdd
            {
                Id = 5,
                Image = new Image
                {
                    Data = "asdfasdfsafasf",
                    Name = "asdf.jpg"
                },
                WordId = 1
            };
            var expected = service.Edit(model);
            var actual = controller.EditTask(model) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_GetAll()
        {
            var expected = service.GetAll().Count();
            var result = controller.GetAllTasks() as OkNegotiatedContentResult<IEnumerable<GuessesTheImageToReturn>>;
            var actual = result.Content.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_GetById()
        {
            var expected = service.Get(2);
            var actual = controller.GetTaskById(2) as OkNegotiatedContentResult<GuessesTheImageToReturn>;
            Assert.AreEqual(expected.Id, actual.Content.Id);
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_Delete()
        {
            var expected = "Delete completed successful";
            var actual = controller.DeleteTask(2) as OkNegotiatedContentResult<string>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_GetAllCategories()
        {
            var expected = service.GetAllCategories().Count();
            var actual = controller.GetAllCategories() as OkNegotiatedContentResult<IEnumerable<string>>;
            Assert.AreEqual(expected, actual.Content.Count());
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_GetByCategory()
        {
            var expected = service.GetByCategory("Category2").Count();
            var actual = controller.GetByCategory("Category2") as OkNegotiatedContentResult<IEnumerable<GuessesTheImageToReturn>>;
            Assert.AreEqual(expected, actual.Content.Count());
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_GetTask()
        {
            var expected = service.GetTask("Category2", "");
            var actual = controller.GetTask("Category2", "") as OkNegotiatedContentResult<GuessesTheImageToReturn>;
            Assert.AreEqual(expected.Id, actual.Content.Id);
        }

        [TestMethod]
        public void GuessesTheImageControllerTest_VerificationCorrectness()
        {
            var model = new GuessesTheImageToReturn
            {
                Id = 1,
                Path = "path1",
                Word = new Word
                {
                    WordId = 1,
                    Category = "Category" + 1,
                    Original = "Original" + 1,
                    Translate = "Translate" + 1,
                    Transcription = "Transcription" + 1
                }
            };
            var expected = true;
            var actual = controller.VerificationCorrectness(model) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }
    }
}
