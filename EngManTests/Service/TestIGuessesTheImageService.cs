using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using EngMan.Service;
using System.Linq;
using Moq;

namespace EngManTests.Repository
{
    [TestClass]
    public class TestIGuessesTheImageService
    {
        private IGuessesTheImageRepository rep;
        private IGuessesTheImageService service;

        public TestIGuessesTheImageService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var dataWords = GenerateDataWords();
            var _rep = new Mock<IGuessesTheImageRepository>();
            _rep.Setup(x => x.Add(It.IsAny<GuessesTheImageToAdd>()))
                .Returns(true);
            _rep.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns(1);
            _rep.Setup(x => x.Edit(It.IsAny<GuessesTheImageToAdd>()))
                .Returns(true);
            _rep.Setup(x => x.GetAll())
                .Returns(data.Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(y => x.WordId == y.WordId) }).AsEnumerable());
            _rep.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(y => data.Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(word => word.WordId == word.WordId) }).FirstOrDefault(x => x.Id == y));
            _rep.Setup(x => x.GetAllCategories()).Returns(dataWords.Select(x => x.Category));
            _rep.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data
                                        .Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(y => x.WordId == y.WordId) })
                                        .Where(x => x.Word.Category.Equals(str))
                                        .AsEnumerable());
            _rep.Setup(x => x.GetTasks(It.IsAny<string>(), It.IsAny<int[]>()))
                .Returns<string, int[]>((str, arr) => data
                                        .Select(x => new GuessesTheImageToReturn { Id = x.Id, Path = x.Path, Word = dataWords.FirstOrDefault(y => x.WordId == y.WordId) })
                                        .Where(x => x.Word.Category.Equals(str) && x.Id != arr[0])
                                        .AsEnumerable());
            service = new GuessesTheImageService(_rep.Object);
            rep = _rep.Object;
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

        public IQueryable<Word> GenerateDataWords()
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
        public void GuessesTheImageServiceTest_Add_valid()
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
            var expected = rep.Add(model);
            var actual = service.Add(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_Add_invalid()
        {
            var model = new GuessesTheImageToAdd
            {
                Id = 10000,
                Image = new Image
                {
                    Data = "asdfasdfsafasf",
                    Name = "asdf.jpg"
                },
                WordId = 1
            };
            var expected = rep.Add(model);
            var actual = service.Add(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_Add_nullException()
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
        public void GuessesTheImageServiceTest_Edit_valid()
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
            var expected = rep.Edit(model);
            var actual = service.Edit(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_Edit_invalid()
        {
            var model = new GuessesTheImageToAdd
            {
                Id = 10000,
                Image = new Image
                {
                    Data = "asdfasdfsafasf",
                    Name = "asdf.jpg"
                },
                WordId = 1
            };
            var expected = rep.Edit(model);
            var actual = service.Edit(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_Edit_nullException()
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
        public void GuessesTheImageServiceTest_GetAll_count()
        {
            var expected = rep.GetAll().Count();
            var actual = service.GetAll().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_GetById_valid()
        {
            var expected = rep.Get(2);
            var actual = service.Get(2);
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_GetById_invalid()
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
        public void GuessesTheImageServiceTest_Delete_valid()
        {
            var expected = rep.Delete(2);
            var actual = service.Delete(2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_Delete_invalid()
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
        public void GuessesTheImageServiceTest_GetAllCategories_count()
        {
            var expected = rep.GetByCategory("Category2").Count();
            var actual = service.GetByCategory("Category2").Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_GetTask_invalid()
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
        public void GuessesTheImageServiceTest_VerificationCorrectness_valid()
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
            var actual = service.VerificationCorrectness(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_VerificationCorrectness_invalid()
        {
            var model = new GuessesTheImageToReturn
            {
                Id = 1,
                Path = "path1",
                Word = new Word
                {
                    WordId = 1,
                    Category = "Category" + 1,
                    Original = "Original" + 1235,
                    Translate = "Translate" + 1,
                    Transcription = "Transcription" + 1
                }
            };
            var expected = false;
            var actual = service.VerificationCorrectness(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GuessesTheImageServiceTest_VerificationCorrectness_nullException()
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
