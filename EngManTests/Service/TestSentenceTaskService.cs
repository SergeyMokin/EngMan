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
            _rep.Setup(x => x.AddTask(It.IsAny<SentenceTask>()))
                .Returns(true);
            _rep.Setup(x => x.DeleteTask(It.IsAny<int>()))
                .Returns<int>(x => Task.FromResult(x));
            _rep.Setup(x => x.SaveTask(It.IsAny<SentenceTask>()))
                .Returns(Task.FromResult(true));
            _rep.Setup(x => x.SentenceTasks)
                .Returns(data);
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
    }
}
