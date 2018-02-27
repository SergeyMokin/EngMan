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
    public class TestUserDictionaryService
    {
        private IUserDictionaryRepository rep;
        private IUserDictionaryService service;

        public TestUserDictionaryService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var dataWords = GenerateDataWords();
            var dataUserWords = GenerateDataUserWords();
            var _rep = new Mock<IUserDictionaryRepository>();
            _rep.Setup(x => x.AddWordToDictionary(It.IsAny<int>(), It.IsAny<UserWord>()))
                .Returns(true);
            _rep.Setup(x => x.DeleteWordFromDictionary(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int>(x => x);
            _rep.Setup(x => x.GetUserDictionary(It.IsAny<int>()))
                .Returns<int>(id => new UserDictionary
                {
                    User = data.FirstOrDefault(x => x.Id == id),
                    Words = dataWords.Where(x => x.WordId == dataUserWords.FirstOrDefault(y => y.WordId == x.WordId && y.UserId == id).WordId)
                });
            _rep.Setup(x => x.GetAllCategories(It.IsAny<int>())).Returns(dataWords.GroupBy(x => x.Category).Select(x => x.Key));
            _rep.Setup(x => x.GetByCategory(It.IsAny<int>(), It.IsAny<string>()))
                .Returns<int, string>((id, str) => new UserDictionary
                {
                    User = data.FirstOrDefault(x => x.Id == id),
                    Words = dataWords.Where(x => x.WordId == dataUserWords.FirstOrDefault(y => y.WordId == x.WordId && y.UserId == id).WordId && x.Category.Equals(str))
                });
            service = new UserDictionaryService(_rep.Object);
            rep = _rep.Object;
        }

        private IQueryable<UserView> GenerateData()
        {
            var lst = new List<UserView>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new UserView
                {
                    Id = i,
                    FirstName = "fname" + i,
                    LastName = "lname" + i,
                    Email = "email" + i,
                    Role = "user"
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

        private IQueryable<UserWord> GenerateDataUserWords()
        {
            var lst = new List<UserWord>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new UserWord
                {
                    Id = i,
                    UserId = i,
                    WordId = i
                });
            }
            return lst.AsQueryable();
        }
    }
}
