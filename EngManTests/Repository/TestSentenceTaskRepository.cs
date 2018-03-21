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
    public class TestSentenceTaskRepository
    {
        private Mock<EFDbContext> context;
        private ISentenceTaskRepository rep;

        public TestSentenceTaskRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<SentenceTask>>();
            mockSet.As<IQueryable<SentenceTask>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<SentenceTask>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SentenceTask>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SentenceTask>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>((d) =>
            {
                return data.FirstOrDefault(x => x.SentenceTaskId == (int)d[0]);
            });
            context.Setup(x => x.Set<SentenceTask>()).Returns(mockSet.Object);
            rep = new SentenceTaskRepository(context.Object);
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
        public void SentenceTaskRepositoryTest_SentenceTasks_getTasks_nullException()
        {
            try
            {
                rep.GetTasks(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_count()
        {
            var count = context.Object.SentenceTasks.Count();
            var rep_count = rep.GetAll().Count();
            Assert.AreEqual(count, rep_count, string.Format(count + " != " + rep_count));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_getAllCategories()
        {
            var countOfCategories = context.Object.SentenceTasks.GroupBy(x => x.Category).Count();
            var rep_countOfAllCategories = rep.GetAllCategories().Count();
            Assert.AreEqual(countOfCategories, rep_countOfAllCategories, string.Format(countOfCategories + " != " + rep_countOfAllCategories));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_getByCategory()
        {
            var count = context.Object.SentenceTasks.GroupBy(x => x.Category).Where(x => x.Key.Equals("Category2")).Count();
            var rep_count = rep.GetByCategory("Category2").Count();
            Assert.AreEqual(count, rep_count, string.Format(count + " != " + rep_count));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_getByCategory_nullException()
        {
            try
            {
                rep.GetByCategory(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Save_validModel()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var result = rep.Edit(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Save_invalidModel()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 0,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var result = rep.Edit(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Save_exceptionModel()
        {
            try
            {
                var result = rep.Edit(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Add_validModel()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 0,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var result = rep.Add(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Add_invalidModel()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 10000,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var result = rep.Add(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Add_exceptionModel()
        {
            try
            {
                var result = rep.Add(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Delete_validModel()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var result = rep.Delete(model.SentenceTaskId);
            Assert.AreEqual(model.SentenceTaskId, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void SentenceTaskRepositoryTest_SentenceTasks_Delete_invalidModel()
        {
            var model = new SentenceTask
            {
                SentenceTaskId = 0,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var result = rep.Delete(model.SentenceTaskId);
            Assert.AreEqual(-1, result);
        }
    }
}
