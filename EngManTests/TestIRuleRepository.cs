using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Controllers;
using EngMan.Models;
using EngMan.Service;
using EngMan.Repository;
using System.Web.Http;
using System.Linq;
using Moq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace EngManTests
{
    [TestClass]
    public class TestIRuleRepository
    {
        private Mock<EFDbContext> context;
        private IRuleRepository rep;
        private IRuleService service;

        public TestIRuleRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateRuleData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<RuleModel>>();
            mockSet.As<IQueryable<RuleModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<RuleModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<RuleModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<RuleModel>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            context.Setup(x => x.Set<RuleModel>()).Returns(mockSet.Object);
            rep = new RuleRepository(context.Object);
            service = new RuleService(rep);
        }

        public IQueryable<RuleModel> GenerateRuleData()
        {
            var lst = new List<RuleModel>();
            for (int i = 0; i < 100; i++)
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

        [TestMethod]
        public void RuleRepositoryTest_Rules_count()
        {
            var countOfRules = context.Object.Rules.Count();
            var rep_countOfAllRules= rep.Rules.Count();
            Assert.AreEqual(countOfRules, rep_countOfAllRules, string.Format(countOfRules + " != " + rep_countOfAllRules));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_getAllCategories()
        {
            var countOfCategories = context.Object.Rules.GroupBy(x => x.Category).Count();
            var rep_countOfAllCategories= rep.GetAllCategories().Count();
            Assert.AreEqual(countOfCategories, rep_countOfAllCategories, string.Format(countOfCategories + " != " + rep_countOfAllCategories));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_getByCategory()
        {
            var countOfRules = context.Object.Rules.GroupBy(x => x.Category).Where(x => x.Key.Equals("Category2")).Count();
            var rep_countOfAllRules = rep.GetByCategory("Category2").Count();
            Assert.AreEqual(countOfRules, rep_countOfAllRules, string.Format(countOfRules + " != " + rep_countOfAllRules));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_SaveRule_validModel()
        {
            var model = new RuleModel
            {
                RuleId = 99,
                Category = "newcategory",
                Text = "textnew",
                Title = "newtitle"
            };
            var result = rep.SaveRule(model).Result;
            Assert.AreEqual(model, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_SaveRule_invalidModel()
        {
            var model = new RuleModel
            {
                RuleId = 1000000,
                Category = "newcategory",
                Text = "textnew",
                Title = "newtitle"
            };
            try
            {
                var result = rep.SaveRule(model).Result;
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "One or more errors occurred.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_SaveRule_exceptionModel()
        {
            RuleModel model = null;
            try
            {
                var result = rep.SaveRule(model).Result;
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "One or more errors occurred.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_AddRule_validModel()
        {
            var model = new RuleModel
            {
                RuleId = 0,
                Category = "newcategory",
                Text = "textnew",
                Title = "newtitle"
            };
            var result = rep.AddRule(model).Result;
            Assert.AreEqual(model.RuleId, result.RuleId, string.Format("result != expected"));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_AddRule_invalidModel()
        {
            var model = new RuleModel
            {
                RuleId = 0,
                Category = null,
                Text = null,
                Title = null
            };
            try
            {
                var result = rep.AddRule(model).Result;
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "One or more errors occurred.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_AddRule_exceptionModel()
        {
            RuleModel model = null;
            try
            {
                var result = rep.AddRule(model).Result;
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "One or more errors occurred.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_DeleteRule_validModel()
        {
            var model = new RuleModel
            {
                RuleId = 99,
                Category = "newcategory",
                Text = "textnew",
                Title = "newtitle"
            };
            var result = rep.DeleteRule(model.RuleId).Result;
            Assert.AreEqual(model.RuleId, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_Deleteule_invalidModel()
        {
            var model = new RuleModel
            {
                RuleId = 0,
                Category = null,
                Text = null,
                Title = null
            };
            var result = rep.DeleteRule(model.RuleId).Result;
            Assert.AreEqual(-1, result);
        }

    }
}