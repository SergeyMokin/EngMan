﻿using System;
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

namespace EngManTests.Repository
{
    [TestClass]
    public class TestIUserDictionaryRepository
    {
        private Mock<EFDbContext> context;
        private IUserDictionaryRepository rep;

        public TestIUserDictionaryRepository()
        {
            CreateCorrectTestData();
        }

        private void CreateCorrectTestData()
        {
            var data = GenerateData();
            context = new Mock<EFDbContext>();
            var mockSet = new Mock<DbSet<UserWord>>();
            mockSet.As<IQueryable<UserWord>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserWord>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserWord>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserWord>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            context.Setup(x => x.Set<UserWord>()).Returns(mockSet.Object);
            rep = new UserDictionaryRepository(context.Object);
        }

        public IQueryable<UserWord> GenerateData()
        {
            var lst = new List<UserWord>();
            for (int i = 1; i < 101; i++)
            {
                lst.Add(new UserWord
                {
                    Id = i,
                    UserId = i,
                    WordId = i
                });
            }
            return lst.AsQueryable();
        }

        [TestMethod]
        public void UserDictionaryRepositoryTest_UserWords_getByCategory_nullException()
        {
            try
            {
                rep.GetByCategory(1, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Value cannot be null.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void UserDictionaryRepositoryTest_UserWords_Add_validModel()
        {
            var model = new UserWord
            {
                Id = 0,
                UserId = 1,
                WordId = 105
            };
            var result = rep.AddWordToDictionary(1, model);
            Assert.AreEqual(true, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserDictionaryRepositoryTest_UserWords_Add_invalidModel()
        {
            var model = new UserWord
            {
                Id = 0,
                UserId = 1,
                WordId = 55
            };
            var result = rep.AddWordToDictionary(1, model);
            Assert.AreEqual(false, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserDictionaryRepositoryTest_UserWords_Add_exceptionModel()
        {
            try
            {
                var result = rep.AddWordToDictionary(1, null);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Value cannot be null.", string.Format("result != expected"));
            }
        }

        [TestMethod]
        public void UserDictionaryRepositoryTest_UserWords_Delete_validModel()
        {
            var model = new UserWord
            {
                Id = 1,
                UserId = 1,
                WordId = 105
            };
            var result = rep.DeleteWordFromDictionary(1, model.Id);
            Assert.AreEqual(model.Id, result, string.Format("result != expected"));
        }

        [TestMethod]
        public void UserDictionaryRepositoryTest_UserWords_Delete_invalidModel()
        {
            var model = new UserWord
            {
                Id = 0,
                UserId = 1,
                WordId = 105
            };
            var result = rep.DeleteWordFromDictionary(1, model.Id);
            Assert.AreEqual(-1, result, string.Format("result != expected"));
        }
    }
}
