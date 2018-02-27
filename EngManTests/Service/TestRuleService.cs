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

        [TestMethod]
        public void RuleServiceTest_GetAllCategories_count()
        {
            var expected = rep.GetAllCategories().Count();
            var actual = service.GetAllCategories().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_GetByCategory_valid()
        {
            var expected = rep.GetByCategory("Category1").Count();
            var actual = service.GetByCategory("Category1").Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_GetByCategory_invalid()
        {
            try
            {
                service.GetByCategory(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void RuleServiceTest_Get_count()
        {
            var expected = rep.Rules.Count();
            var actual = service.Get().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_GetById_valid()
        {
            var expected = rep.Rules.FirstOrDefault(x => x.RuleId == 1);
            var actual = service.GetById(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_GetById_invalid()
        {
            try
            {
                service.GetById(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void RuleServiceTest_Edit_valid()
        {
            var model = new RuleModel
            {
                RuleId = 1,
                Category = "Category",
                Text = "Text",
                Title = "Title"
            };
            var expected = rep.SaveRule(model).Result;
            var actual = service.Edit(model).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_Edit_invalid()
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
        public void RuleServiceTest_Add_valid()
        {
            var model = new RuleModel
            {
                RuleId = 1,
                Category = "Category",
                Text = "Text",
                Title = "Title"
            };
            var expected = rep.AddRule(model);
            var actual = service.Add(model);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_Add_invalid()
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
        public void RuleServiceTest_Delete_valid()
        {
            var expected = rep.DeleteRule(1).Result;
            var actual = service.Delete(1).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RuleServiceTest_Delete_invalid()
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
        public void RuleServiceTest_AddImages_valid()
        {
            var expected = new List<string> { "Can not be written" };
            var actual = service.AddImages(new Image[] { new Image { Data = "asdfadsf", Name = "asdf.jpg" } });
            Assert.AreEqual(expected.ElementAt(0), actual.ElementAt(0));
        }

        [TestMethod]
        public void RuleServiceTest_AddImages_invalid()
        {
            try
            {
                service.AddImages(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }
    }
}