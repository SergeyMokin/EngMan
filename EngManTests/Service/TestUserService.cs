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
    public class TestUserService
    {
        private IUserRepository rep;
        private IUserService service;

        public TestUserService()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            var _rep = new Mock<IUserRepository>();
            _rep.Setup(x => x.AddUser(It.IsAny<User>()))
                .Returns(true);
            _rep.Setup(x => x.ChangePassword(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _rep.Setup(x => x.ChangeRole(It.IsAny<UserView>()))
                .Returns(Task.FromResult(true));
            _rep.Setup(x => x.DeleteUser(It.IsAny<int>()))
                .Returns<int>(x => x);
            _rep.Setup(x => x.SaveUser(It.IsAny<UserView>())).Returns(Task.FromResult(true));
            _rep.Setup(x => x.Users)
                .Returns(data);
            service = new UserService(_rep.Object);
            rep = _rep.Object;
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
                    Password = "AMiGXC + zX4Apg3WyLkvFTfeRyNr8rdRr2n3mHIwY4gchRDX / uQfj / skDh1pLGkZBHw == "
                });
            }
            return lst.AsQueryable();
        }

        [TestMethod]
        public void UserServiceTest_ValidateUser_valid()
        {
            var expected = rep.Users.FirstOrDefault();
            var actual = service.ValidateUser("email1@email.com", "password1");
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void UserServiceTest_ValidateUser_invalid()
        {
            try
            {
                service.ValidateUser(null, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void UserServiceTest_GetUser_valid()
        {
            var expected = rep.Users.FirstOrDefault();
            var actual = service.GetUser(1);
            Assert.AreEqual(expected.Id, actual.Id);
        }

        [TestMethod]
        public void UserServiceTest_GetUser_invalid()
        {
            try
            {
                service.GetUser(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void UserServiceTest_Registration_valid()
        {
            var model = new User
            {
                Id = 0,
                Email = "email@email.email",
                FirstName = "Fname",
                LastName = "Lname",
                Password = "Password9856",
                Role = "user"
            };
            var actual = service.Registration(model);
            Assert.AreNotEqual(actual, null);
        }

        [TestMethod]
        public void UserServiceTest_Registration_invalid()
        {
            try
            {
                service.Registration(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void UserServiceTest_SaveUser_valid()
        {
            var model = new UserView
            {
                Id = 3,
                Email = "email@email.email",
                FirstName = "Fname",
                LastName = "Lname",
                Role = "user"
            };
            var expected = true;
            var actual = service.SaveUser(model).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserServiceTest_SaveUser_invalid()
        {
            try
            {
                service.SaveUser(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void UserServiceTest_DeleteUser_valid()
        {
            var expected = "Delete completed successful";
            var actual = service.DeleteUser(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserServiceTest_DeleteUser_invalid()
        {
            try
            {
                service.DeleteUser(-1);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void UserServiceTest_GetUserList_count()
        {
            var expected = rep.Users.Count();
            var actual = service.GetUserList().Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserServiceTest_ChangePassword_valid()
        {
            var expected = rep.ChangePassword(1, "Password1", "NewPassword9856");
            var actual = service.ChangePassword(1, "Password1", "NewPassword9856");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserServiceTest_ChangePassword_invalid()
        {
            try
            {
                service.ChangePassword(1, null, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }

        [TestMethod]
        public void UserServiceTest_ChangeRole_valid()
        {
            var model = new UserView
            {
                Id = 3,
                Email = "email@email.email",
                FirstName = "Fname",
                LastName = "Lname",
                Role = "admin"
            };
            var expected = rep.ChangeRole(model).Result;
            var actual = service.ChangeRole(model).Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserServiceTest_ChangeRole_invalid()
        {
            try
            {
                service.ChangeRole(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid model", e.Message);
            }
        }
    }
}
