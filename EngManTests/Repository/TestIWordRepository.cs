using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using Moq;
using System.Data.Entity;

namespace EngManTests.Repository
{
    [TestClass]
    public class TestIWordRepository
    {
        private Mock<EFDbContext> context;
        private IWordRepository rep;

        public TestIWordRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<Word>>();
            mockSet.As<IQueryable<Word>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Word>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Word>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Word>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(async (d) =>
            {
                return await Task.FromResult(data.FirstOrDefault(x => x.WordId == (int)d[0]));
            });
            context.Setup(x => x.Set<Word>()).Returns(mockSet.Object);
            rep = new WordRepository(context.Object);
        }

        public IQueryable<Word> GenerateData()
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
        public void WordRepositoryTest_Words_getTasks_nullException()
        {
            try
            {
                rep.GetTasks(null);
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "Value cannot be null.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void WordRepositoryTest_Words_count()
        {
            var count = context.Object.Words.Count();
            var rep_count = rep.Words.Count();
            Assert.AreEqual(count, rep_count, string.Format(count + " != " + rep_count));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_getAllCategories()
        {
            var countOfCategories = context.Object.Words.GroupBy(x => x.Category).Count();
            var rep_countOfAllCategories = rep.GetAllCategories().Count();
            Assert.AreEqual(countOfCategories, rep_countOfAllCategories, string.Format(countOfCategories + " != " + rep_countOfAllCategories));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_getByCategory()
        {
            var count = context.Object.Words.GroupBy(x => x.Category).Where(x => x.Key.Equals("Category2")).Count();
            var rep_count = rep.GetByCategory("Category2").Count();
            Assert.AreEqual(count, rep_count, string.Format(count + " != " + rep_count));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_getByCategory_nullException()
        {
            try
            {
                rep.GetByCategory(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Value cannot be null.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Save_validModel()
        {
            var model = new Word
            {
                WordId = 1,
                Category = "Category",
                Original = "Original",
                Translate = "Translate",
                Transcription = "Transcription"
            };
            var result = rep.SaveWord(model).Result;
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Save_invalidModel()
        {
            var model = new Word
            {
                WordId = 10000,
                Category = "Category",
                Original = "Original",
                Translate = "Translate",
                Transcription = "Transcription"
            };
            var result = rep.SaveWord(model).Result;
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Save_exceptionModel()
        {
            Word model = null;
            try
            {
                var result = rep.SaveWord(model).Result;
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.InnerException.Message, "Value cannot be null.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Add_validModel()
        {
            var model = new Word
            {
                WordId = 0,
                Category = "Category",
                Original = "Original",
                Translate = "Translate",
                Transcription = "Transcription"
            };
            var result = rep.AddWord(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Add_invalidModel()
        {
            var model = new Word
            {
                WordId = 10000,
                Category = "Category",
                Original = "Original",
                Translate = "Translate",
                Transcription = "Transcription"
            };
            var result = rep.AddWord(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Add_exceptionModel()
        {
            Word model = null;
            try
            {
                var result = rep.AddWord(model);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Value cannot be null.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Delete_validModel()
        {
            var model = new Word
            {
                WordId = 1,
                Category = "Category",
                Original = "Original",
                Translate = "Translate",
                Transcription = "Transcription"
            };
            var result = rep.DeleteWord(model.WordId).Result;
            Assert.AreEqual(model.WordId, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void WordRepositoryTest_Words_Delete_invalidModel()
        {
            var model = new Word
            {
                WordId = 0,
                Category = null,
                Original = null,
                Translate = null,
                Transcription = null
            };
            var result = rep.DeleteWord(model.WordId).Result;
            Assert.AreEqual(-1, result);
        }

    }
}
