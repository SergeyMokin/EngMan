using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using Moq;
using System.Data.Entity;

namespace EngManTests.Service
{
    [TestClass]
    public class TestIGuessesTheImageRepository
    {
        private Mock<EFDbContext> context;
        private IGuessesTheImageRepository rep;

        public TestIGuessesTheImageRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<GuessesTheImage>>();
            mockSet.As<IQueryable<GuessesTheImage>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<GuessesTheImage>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<GuessesTheImage>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<GuessesTheImage>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>((d) =>
            {
                return data.FirstOrDefault(x => x.Id == (int)d[0]);
            });
            context.Setup(x => x.Set<GuessesTheImage>()).Returns(mockSet.Object);
            rep = new GuessesTheImageRepository(context.Object);
        }

        public IQueryable<GuessesTheImage> GenerateData()
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

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_getTasks_nullException()
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
        public void GuessesTheImageTest_GuessesTheImages_getByCategory_nullException()
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
        public void GuessesTheImageTest_GuessesTheImages_Save_validModel()
        {
            var model = new GuessesTheImageToAdd
            {
                WordId = 1,
                Id = 1,
                Image = null
            };
            var result = rep.Edit(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Save_invalidModel()
        {
            var model = new GuessesTheImageToAdd
            {
                WordId = 1,
                Id = 100000,
                Image = null
            };
            var result = rep.Edit(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Save_exceptionModel()
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
        public void GuessesTheImageTest_GuessesTheImages_Add_validModel()
        {
            var model = new GuessesTheImageToAdd
            {
                WordId = 1,
                Id = 0,
                Image = new Image
                {
                    Data = "asdfasdfasdfasdfasdfasdfasdfasdf",
                    Name = "asdf.jpg"
                }
            };
            var result = rep.Add(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Add_invalidModel()
        {
            var model = new GuessesTheImageToAdd
            {
                WordId = 1,
                Id = 10000,
                Image = null
            };
            var result = rep.Add(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Add_exceptionModel()
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
        public void GuessesTheImageTest_GuessesTheImages_Delete_validModel()
        {
            var result = rep.Delete(1);
            Assert.AreEqual(1, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void GuessesTheImageTest_GuessesTheImages_Delete_invalidModel()
        {
            var result = rep.Delete(0);
            Assert.AreEqual(-1, result);
        }
    }
}
