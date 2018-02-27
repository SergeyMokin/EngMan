using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using EngMan.Service;
using System.Linq;
using Moq;
using System.Threading.Tasks;

namespace EngManTests.Service
{
    [TestClass]
    public class TestWordService
    {
        private IWordRepository rep;
        private IWordService service;

        public TestWordService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _rep = new Mock<IWordRepository>();
            _rep.Setup(x => x.AddWord(It.IsAny<Word>()))
                .Returns(true);
            _rep.Setup(x => x.GetAllCategories())
                .Returns(data.GroupBy(x => x.Category).Select(x => x.Key));
            _rep.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data.Where(x => x.Category.Equals(str)));
            _rep.Setup(x => x.DeleteWord(It.IsAny<int>()))
                .Returns<int>(x => Task.FromResult(x));
            _rep.Setup(x => x.SaveWord(It.IsAny<Word>())).Returns(Task.FromResult(true));
            _rep.Setup(x => x.GetTasks(It.IsAny<string>(), It.IsAny<int[]>()))
                .Returns<string, int[]>((str, id) => data.Where(x => x.Category.Equals(str) && x.WordId != id[0]));
            _rep.Setup(x => x.Words)
                .Returns(data);
            service = new WordService(_rep.Object);
            rep = _rep.Object;
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
        public void WordServiceTest_GetAllCategories_count()
        {
            var expected = rep.GetAllCategories().Count();
            var actual = service.GetAllCategories().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_GetByCategory_valid()
        {
            var expected = rep.GetByCategory("Category1").Count();
            var actual = service.GetByCategory("Category1").Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_GetByCategory_invalid()
        {
            try
            {
                service.GetByCategory(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void WordServiceTest_Get_count()
        {
            var expected = rep.Words.Count();
            var actual = service.Get().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_GetById_valid()
        {
            var expected = rep.Words.FirstOrDefault(x => x.WordId == 1);
            var actual = service.GetById(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_GetById_invalid()
        {
            try
            {
                service.GetById(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void WordServiceTest_Edit_valid()
        {
            var model = new Word
            {
                WordId = 1,
                Category = "Category" + 1,
                Original = "Original" + 1,
                Translate = "Translate" + 1,
                Transcription = "Transcription" + 1
            };
            var expected = rep.SaveWord(model).Result;
            var actual = service.Edit(model).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_Edit_invalid()
        {
            try
            {
                service.Edit(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void WordServiceTest_Add_valid()
        {
            var model = new Word
            {
                WordId = 1,
                Category = "Category" + 1,
                Original = "Original" + 1,
                Translate = "Translate" + 1,
                Transcription = "Transcription" + 1
            };
            var expected = rep.AddWord(model);
            var actual = service.Add(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_Add_invalid()
        {
            try
            {
                service.Add(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void WordServiceTest_Delete_valid()
        {
            var expected = rep.DeleteWord(1).Result;
            var actual = service.Delete(1).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_Delete_invalid()
        {
            try
            {
                service.Delete(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void WordServiceTest_GetTask_invalid()
        {
            try
            {
                service.GetTask(null, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void WordServiceTest_VerificationCorrectness_valid()
        {
            var model = new Word
            {
                WordId = 1,
                Category = "Category" + 1,
                Original = "Original" + 1,
                Translate = "Translate" + 1,
                Transcription = "Transcription" + 1
            };
            var expected = true;
            var actual = service.VerificationCorrectness(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WordServiceTest_VerificationCorrectness_invalid()
        {
            try
            {
                service.VerificationCorrectness(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }
    }
}
