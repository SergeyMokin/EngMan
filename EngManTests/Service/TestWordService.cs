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
    }
}
