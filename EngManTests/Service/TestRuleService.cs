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
    public class TestRuleService
    {
        private IRuleRepository rep;
        private IRuleService service;

        public TestRuleService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _rep = new Mock<IRuleRepository>();
            _rep.Setup(x => x.AddRule(It.IsAny<RuleModel>()))
                .Returns(true);
            _rep.Setup(x => x.DeleteRule(It.IsAny<int>()))
                .Returns<int>(x => Task.FromResult(x));
            _rep.Setup(x => x.SaveRule(It.IsAny<RuleModel>()))
                .Returns(Task.FromResult(true));
            _rep.Setup(x => x.Rules)
                .Returns(data);
            _rep.Setup(x => x.GetAllCategories()).Returns(data.GroupBy(x => x.Category).Select(x => x.Key));
            _rep.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data.Where(x => x.Category.Equals(str)));
            service = new RuleService(_rep.Object);
            rep = _rep.Object;
        }

        private IQueryable<RuleModel> GenerateData()
        {
            var lst = new List<RuleModel>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new RuleModel
                {
                    RuleId = i,
                    Category = "Category" + i,
                    Text = "Text" + i,
                    Title = "Title" + i
                });
            }
            return lst.AsQueryable();
        }
    }
}