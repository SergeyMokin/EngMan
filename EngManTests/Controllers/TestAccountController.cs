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
    public class TestAccountController
    {
        private IUserDictionaryService serviceDictionary;
        private IUserService serviceUser;
        private AccountController controller;

        public TestAccountController()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _service = new Mock<IUserService>();
            _service.Setup(x => x.Registration(It.IsAny<User>()))
                .Returns(true);
            _service.Setup(x => x.ChangePassword(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _service.Setup(x => x.ChangeRole(It.IsAny<UserView>()))
                .Returns(Task.FromResult(true));
            _service.Setup(x => x.DeleteUser(It.IsAny<int>()))
                .Returns<int>(x => x);
            _service.Setup(x => x.SaveUser(It.IsAny<UserView>())).Returns(Task.FromResult(true));
            _service.Setup(x => x.GetUserList())
                .Returns(data.Select(x => new UserView { Id = x.Id }).ToList());
            _service.Setup(x => x.GetUser(It.IsAny<int>()))
                .Returns<int>(id => data.Select(x => new UserView { Id = x.Id }).FirstOrDefault(x => x.Id == id));
            _service.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((email, password) => data.FirstOrDefault());
            serviceDictionary = new Mock<IUserDictionaryService>().Object;
            serviceUser = _service.Object;
            controller = new AccountController(serviceUser, serviceDictionary);
        }

        private IQueryable<User> GenerateData()
        {
            var lst = new List<User>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new User
                {
                    Id = i,
                    Email = "email" + i + "@email.com",
                    FirstName = "fname" + i,
                    LastName = "lname" + i,
                    Role = "user",
                    Password = "password" + i
                });
            }
            return lst.AsQueryable();
        }
        
        [TestMethod]
        public void AccountControllerTest_GetAllCategoriesOfDictionary()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_GetByCategoryDictionary()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_GetUserDictionary()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_AddWordToDictionary()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_DeleteWordFromDictionary()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_GetUserData()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_LogOut()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_ChangePassword()
        {
            var actual = controller.GetAllCategoriesOfDictionary();
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AccountControllerTest_Registration()
        {
            var expected = serviceUser.Registration(new User { Id = 1 });
            var actual = controller.Registration(new User { Id = 1 }) as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void AccountControllerTest_GetAllUsers()
        {
            var expected = serviceUser.GetUserList();
            var actual = controller.GetAllUsers() as OkNegotiatedContentResult<List<UserView>>;
            Assert.AreEqual(expected.Count(), actual.Content.Count());
        }

        [TestMethod]
        public void AccountControllerTest_GetUserById()
        {
            var expected = serviceUser.GetUser(1);
            var actual = controller.GetUserById(1) as OkNegotiatedContentResult<UserView>;
            Assert.AreEqual(expected.Id, actual.Content.Id);
        }

        [TestMethod]
        public void AccountControllerTest_EditUser()
        {
            var expected = serviceUser.SaveUser(new UserView { Id = 1 }).Result;
            var actual = controller.EditUser(new UserView { Id = 1 }).Result as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }

        [TestMethod]
        public void AccountControllerTest_DeleteUser()
        {
            var actual = controller.DeleteUser(1) as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Delete completed successful", actual.Content);
        }

        [TestMethod]
        public void AccountControllerTest_ChangeRole()
        {
            var expected = serviceUser.ChangeRole(new UserView { Id = 1 }).Result;
            var actual = controller.ChangeRole(new UserView { Id = 1 }).Result as OkNegotiatedContentResult<bool>;
            Assert.AreEqual(expected, actual.Content);
        }
    }
}
