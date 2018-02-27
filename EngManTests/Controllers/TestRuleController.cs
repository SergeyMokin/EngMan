using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Controllers;
using EngMan.Service;
using System.Linq;
using Moq;
using System.Web.Http.Results;
using System.Threading.Tasks;
namespace EngManTests.Controllers
{
    [TestClass]
    public class TestRuleController
    {
        private IRuleService service;
        private RuleController controller;

        public TestRuleController()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _service = new Mock<IRuleService>();
            _service.Setup(x => x.Add(It.IsAny<RuleModel>()))
                .Returns(true);
            _service.Setup(x => x.Delete(It.IsAny<int>()))
                .Returns<int>(x => Task.FromResult(x));
            _service.Setup(x => x.Edit(It.IsAny<RuleModel>()))
                .Returns(Task.FromResult(true));
            _service.Setup(x => x.Get())
                .Returns(data);
            _service.Setup(x => x.GetAllCategories()).Returns(data.GroupBy(x => x.Category).Select(x => x.Key));
            _service.Setup(x => x.GetByCategory(It.IsAny<string>()))
                .Returns<string>(str => data.Where(x => x.Category.Equals(str)));
            _service.Setup(x => x.GetById(It.IsAny<int>()))
               .Returns<int>(id => data.FirstOrDefault(x => x.RuleId == id));
            _service.Setup(x => x.AddImages(It.IsAny<Image[]>()))
                .Returns<Image[]>(img => new List<string> { "Ok" });
            controller = new RuleController(_service.Object);
            service = _service.Object;
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
        public void RuleControllerTest_GetAllCategories()
        {
            var expected = service.GetAllCategories();
            var actual = controller.GetAllCategories() as OkNegotiatedContentResult<IEnumerable<string>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void RuleControllerTest_GetAllRules()
        {
            var expected = service.Get();
            var actual = controller.GetAllRules() as OkNegotiatedContentResult<IEnumerable<RuleModel>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void RuleControllerTest_GetByCategory()
        {
            var expected = service.GetByCategory("Category1");
            var actual = controller.GetByCategory("Category1") as OkNegotiatedContentResult<IEnumerable<RuleModel>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void RuleControllerTest_GetRule()
        {
            var expected = service.GetById(1);
            var actual = controller.GetRule(1) as OkNegotiatedContentResult<RuleModel>;
            Assert.AreEqual(expected.RuleId, actual.Content.RuleId);
        }

        [TestMethod]
        public void RuleControllerTest_EditRule()
        {
            var expected = service.Edit(new RuleModel()).Result;
            var actual = controller.EditRule(new RuleModel()).Result as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void RuleControllerTest_AddRule()
        {
            var expected = service.Add(new RuleModel());
            var actual = controller.AddRule(new RuleModel()) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void RuleControllerTest_AddImages()
        {
            var expected = service.AddImages(new Image[] { });
            var actual = controller.AddImages(new Image[] { }) as OkNegotiatedContentResult<List<string>>;
            Assert.AreEqual(expected.ElementAt(0), actual.Content.ElementAt(0));
        }

        [TestMethod]
        public void RuleControllerTest_DeleteRule()
        {
            var actual = controller.DeleteRule(1).Result as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Delete completed successful", actual.Content);
        }
    }
}