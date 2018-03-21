using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Repository;
using System.Linq;
using Moq;
using System.Data.Entity;
using EngMan.Extensions;

namespace EngManTests.Repository
{
    [TestClass]
    public class TestUserRepository
    {
        private Mock<EFDbContext> context;
        private IUserRepository rep;

        public TestUserRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(async (d) =>
            {
                return await Task.FromResult(data.FirstOrDefault(x => x.Id == (int)d[0]));
            });
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>((d) =>
            {
                return data.FirstOrDefault(x => x.Id == (int)d[0]);
            });
            context.Setup(x => x.Set<User>()).Returns(mockSet.Object);
            rep = new UserRepository(context.Object);
        }

        private IQueryable<User> GenerateData()
        {
            var lst = new List<User>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new User
                {
                    Id = i,
                    Email = "email" + i,
                    FirstName = "firstname" + i,
                    LastName = "lastname" + i,
                    Password = ("password" + i).HashPassword(),
                    Role = "user"
                });
            }
            return lst.AsQueryable();
        }

        [TestMethod]
        public void UserTest_Users_getAllUsers_count()
        {
            var result = rep.GetAll().Count();
            Assert.AreEqual(context.Object.Users.Count(), result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_saveUser_valid()
        {
            var model = new UserView
            {
                Id = 10,
                Email = "email",
                FirstName = "fname",
                LastName = "lname",
                Role = "user"
            };
            var result = rep.Edit(new User(model));
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_saveUser_invalid()
        {
            var model = new UserView
            {
                Id = 0,
                Email = "email",
                FirstName = "fname",
                LastName = "lname",
                Role = "user"
            };
            var result = rep.Edit(new User(model));
            Assert.AreNotEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_saveUser_nullException()
        {
            try
            {
                rep.Edit(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void UserTest_Users_changePassword_valid()
        {
            var result = rep.ChangePassword(10, "password10", "password2");
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_changePassword_invalid()
        {
            var result = rep.ChangePassword(10, "password2", "password1");
            Assert.AreNotEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_changePassword_nullException()
        {
            try
            {
                rep.ChangePassword(1, null, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void UserTest_Users_changeRole_valid()
        {
            var model = new UserView
            {
                Id = 1,
                Email = "email",
                FirstName = "fname",
                LastName = "lname",
                Role = "admin"
            };
            var result = rep.ChangeRole(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_changeRole_invalid()
        {
            var model = new UserView
            {
                Id = 0,
                Email = "email",
                FirstName = "fname",
                LastName = "lname",
                Role = "admin"
            };
            var result = rep.ChangeRole(model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_changeRole_nullException()
        {
            try
            {
                rep.ChangeRole(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void UserTest_Users_addUser_valid()
        {
            var model = new User
            {
                Id = 0,
                Email = "email",
                FirstName = "fname",
                LastName = "lname",
                Role = "admin",
                Password = "password"
            };
            var result = rep.Add(model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_addUser_invalid()
        {
            var model = new User
            {
                Id = 10000,
                Email = "email",
                FirstName = "fname",
                LastName = "lname",
                Role = "admin",
                Password = "password"
            };
            var result = rep.Add(model);
            Assert.AreNotEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_addUser_nullException()
        {
            try
            {
                rep.Add(null);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Value cannot be null.", e.Message, string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void UserTest_Users_deleteUser_valid()
        {
            var result = rep.Delete(1);
            Assert.AreEqual(1, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserTest_Users_deleteUser_invalid()
        {
            var result = rep.Delete(0);
            Assert.AreEqual(-1, result, string.Format("result != expected"));
        }
    }
}
