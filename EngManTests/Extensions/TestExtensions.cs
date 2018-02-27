using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EngMan.Models;
using EngMan.Extensions;
namespace EngManTests.Extensions
{
    [TestClass]
    public class TestExtensions
    {
        [TestMethod]
        public void ExtensionsTest_Validate_Indexes_Array()
        {
            var good = new []{ 1, 2, 3 };
            var bad = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            Assert.AreEqual(true, good.IsCorrect());
            Assert.AreEqual(false, bad.IsCorrect());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_Password()
        {
            var good = "OkeyGood123";
            var bad = " ,123Aswer";
            Assert.AreEqual(true, good.IsCorrectPassword());
            Assert.AreEqual(false, bad.IsCorrectPassword());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_Name()
        {
            var good = "Okey";
            var bad = ",123asd";
            Assert.AreEqual(true, good.IsName());
            Assert.AreEqual(false, bad.IsName());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_Email()
        {
            var good = "email@gmail.com";
            var bad = "thisisemail";
            Assert.AreEqual(true, good.IsEmail());
            Assert.AreEqual(false, bad.IsEmail());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_User()
        {
            var good = new User
            {
                Id = 0,
                Email = "email@email.com",
                FirstName = "fname",
                LastName = "lname",
                Password = "GoodStrongPass123",
                Role = "user"
            };
            var bad = new User
            {
                Id = 1,
                Email = "emailemailcom",
                FirstName = "fname",
                LastName = "lname",
                Password = "Good",
                Role = "user"
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_UserView()
        {
            var good = new UserView
            {
                Id = 0,
                Email = "email@email.com",
                FirstName = "fname",
                LastName = "lname",
                Role = "user"
            };
            var bad = new User
            {
                Id = 1,
                Email = "emailemailcom",
                FirstName = "fname",
                LastName = "lname",
                Role = "user"
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_UserWord()
        {
            var good = new UserWord
            {
                Id = 0,
                WordId = 1,
                UserId = 1
            };
            var bad = new UserWord
            {
                Id = -1,
                WordId = -1,
                UserId = 1
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_SentenceTask()
        {
            var good = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = "Sentence",
                Category = "Category",
                Translate = "Translate"
            };
            var bad = new SentenceTask
            {
                SentenceTaskId = 1,
                Sentence = null,
                Category = "Category",
                Translate = "Translate"
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_RuleModel()
        {
            var good = new RuleModel
            {
                Category = "Category",
                RuleId = 1,
                Text = "Text",
                Title = "Title"
            };
            var bad = new RuleModel
            {
                Category = null,
                RuleId = 1,
                Text = "Text",
                Title = "Title"
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_Message()
        {
            var good = new Message
            {
                MessageId = 1,
                SenderId = 1,
                BeneficiaryId = 2,
                CheckReadMes = false,
                Text = "text",
                Time = DateTime.Now
            };
            var bad = new Message
            {
                MessageId = 1,
                SenderId = 1,
                BeneficiaryId = 2,
                CheckReadMes = false,
                Text = null,
                Time = DateTime.Now
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_GuessesTheImageToReturn()
        {
            var good = new GuessesTheImageToReturn
            {
                Id = 1,
                Path = "path",
                Word = new Word
                {
                    WordId = 1,
                    Category = "Category",
                    Original = "Original",
                    Transcription = "Transcription",
                    Translate = "Translate"
                }
            };
            var bad = new GuessesTheImageToReturn
            {
                Id = 1,
                Path = "path",
                Word = new Word
                {
                    WordId = 1,
                    Category = "Category",
                    Original = "Original",
                    Transcription = "Transcription",
                    Translate = null
                }
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_Word()
        {
            var good = new Word
            {
                WordId = 1,
                Category = "Category",
                Original = "Original",
                Transcription = "Transcription",
                Translate = "Translate"
            };
            var bad = new Word
            {
                WordId = 1,
                Category = "Category",
                Original = "Original",
                Transcription = null,
                Translate = "Translate"
            };
            Assert.AreEqual(true, good.Validate(true));
            Assert.AreEqual(false, bad.Validate(true));
            Assert.AreEqual(true, bad.Validate(false));
        }

        [TestMethod]
        public void ExtensionsTest_Validate_GuessesTheImageToAdd()
        {
            var good = new GuessesTheImageToAdd
            {
                Id = 1,
                WordId = 1,
                Image = new Image
                {
                    Data = "asdfafd",
                    Name = "asdffdsa"
                }
            };
            var bad = new GuessesTheImageToAdd
            {
                Id = 1,
                WordId = 1,
                Image = new Image
                {
                    Data = null,
                    Name = null
                }
            };
            Assert.AreEqual(true, good.Validate(true));
            Assert.AreEqual(false, bad.Validate(true));
            Assert.AreEqual(true, bad.Validate(false));
        }

        [TestMethod]
        public void ExtensionsTest_Validate_Image()
        {
            var good = new Image
            {
                Data = "asdfafd",
                Name = "asdffdsa"
            };
            var bad = new Image
            {
                Data = null,
                Name = null
            };
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_DatabaseId()
        {
            var good = 1;
            var bad = 0;
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
        }

        [TestMethod]
        public void ExtensionsTest_Validate_StringForNull()
        {
            var good = "str";
            var bad = "";
            string nullcheck = null;
            Assert.AreEqual(true, good.Validate());
            Assert.AreEqual(false, bad.Validate());
            Assert.AreEqual(false, nullcheck.Validate());
        }
    }
}
