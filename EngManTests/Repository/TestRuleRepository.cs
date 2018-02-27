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
    public class TestRuleRepository
    {
        private Mock<EFDbContext> context;
        private IRuleRepository rep;

        public TestRuleRepository()
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
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(async (d) =>
            {
                return await Task.FromResult(data.FirstOrDefault(x => x.RuleId == (int)d[0]));
            });
            context.Setup(x => x.Set<RuleModel>()).Returns(mockSet.Object);
            rep = new RuleRepository(context.Object);
        }

        private IQueryable<RuleModel> GenerateRuleData()
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
            Assert.AreEqual(true, result, string.Format("result != expected"));
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
            var result = rep.SaveRule(model).Result;
            Assert.AreEqual(false, result, string.Format("result != expected"));
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
                Assert.AreEqual("Value cannot be null.", e.InnerException.Message, string.Format("result != expected"));
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
            var result = rep.AddRule(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_AddRule_invalidModel()
        {
            var model = new RuleModel
            {
                RuleId = 15,
                Category = null,
                Text = null,
                Title = null
            };
            var result = rep.AddRule(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void RuleRepositoryTest_Rules_AddRule_exceptionModel()
        {
            RuleModel model = null;
            try
            {
                var result = rep.AddRule(model);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
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
        public void RuleRepositoryTest_Rules_DeleteRule_invalidModel()
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