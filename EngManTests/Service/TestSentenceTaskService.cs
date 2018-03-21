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
    public class TestSentenceTaskService
    {
        private ISentenceTaskRepository rep;
        private ISentenceTaskService service;

        public TestSentenceTaskService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _rep = new Mock<ISentenceTaskRepository>();
            _rep.Setup(x => x.Add(It.IsAny<SentenceTask>()))
                .Returns(true);
            _rep.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<int>(x => x);
            _rep.Setup(x => x.Edit(It.IsAny<SentenceTask>()))
                .Returns(true);
            _rep.Setup(x => x.GetAll())
                .Returns(data);
            _rep.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(x => data.FirstOrDefault());
            _rep.Setup(x => x.GetAllCategories()).Returns(data.GroupBy(x => x.Category).Select(x => x.Key));
            _rep.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data.Where(x => x.Category.Equals(str)));
            _rep.Setup(x => x.GetTasks(It.IsAny<string>(), It.IsAny<int[]>()))
                .Returns<string, int[]>((str, arr) => data.Where(x => x.Category.Equals(str) && x.SentenceTaskId != arr[0]));
            service = new SentenceTaskService(_rep.Object);
            rep = _rep.Object;
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
        public void SentenceTaskServiceTest_GetAllCategories_count()
        {
            var expected = rep.GetAllCategories().Count();
            var actual = service.GetAllCategories().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_GetByCategory_valid()
        {
            var expected = rep.GetByCategory("Category1").Count();
            var actual = service.GetByCategory("Category1").Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_GetByCategory_invalid()
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
        public void SentenceTaskServiceTest_Get_count()
        {
            var expected = rep.GetAll().Count();
            var actual = service.GetAll().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_GetById_valid()
        {
            var expected = rep.Get(1);
            var actual = service.Get(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_GetById_invalid()
        {
            try
            {
                service.Get(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void SentenceTaskServiceTest_Edit_valid()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var expected = rep.Edit(model);
            var actual = service.Edit(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_Edit_invalid()
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
        public void SentenceTaskServiceTest_Add_valid()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var expected = rep.Add(model);
            var actual = service.Add(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_Add_invalid()
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
        public void SentenceTaskServiceTest_Delete_valid()
        {
            var expected = "Delete completed successful";
            var actual = service.Delete(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_Delete_invalid()
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
        public void SentenceTaskServiceTest_GetTask_invalid()
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
        public void SentenceTaskServiceTest_VerificationCorrectness_valid()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = "Sentence1",
                Category = "Category1",
                Translate = "Translate1"
            };
            var expected = true;
            var actual = service.VerificationCorrectness(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SentenceTaskServiceTest_VerificationCorrectness_invalid()
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
