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
                    Email = "email" + i,
                    FirstName = "fname" + i,
                    LastName = "lname" + i,
                    Role = "user",
                    Password = "password" + i
                });
            }
            return lst.AsQueryable();
        }
    }
}
