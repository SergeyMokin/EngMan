using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EngMan.Models;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;

namespace EngMan.Extensions
{
    public static class Extensions
    {
        //Validate indexes array
        public static bool IsCorrect(this IEnumerable<int> task)
        {
            const int maxCountOfTasks = 10;
            return task != null && task.Count() <= maxCountOfTasks;
        }
        //Validate password
        public static bool IsCorrectPassword(this string password)
        {
            const int minLength = 8;
            const int maxLength = 25;

            if (password == null) return false;

            bool meetsLengthRequirements = password.Length >= minLength && password.Length <= maxLength;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;
            bool hasNoSpace = true;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                    else if (char.IsWhiteSpace(c)) hasNoSpace = false;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        && hasNoSpace
                        ;
            return isValid;
        }
        //Validate name
        public static bool IsName(this string name)
        {
            bool isName = true;
            name.Select(x =>
            {
                if (!char.IsLetter(x))
                {
                    isName = false;
                }
                return x;
            }).ToList();
            if (!isName)
            {
                return false;
            }
            return true;
        }
        //Validate email
        public static bool IsEmail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Validate User
        public static bool Validate(this User task)
        {
            return task != null
                && task.Id.Validate()
                && !String.IsNullOrEmpty(task.FirstName)
                && !String.IsNullOrEmpty(task.LastName)
                && task.Email.IsEmail()
                && task.Password.IsCorrectPassword()
                && !String.IsNullOrEmpty(task.Role);
        }

        //Validate UserView
        public static bool Validate(this UserView task)
        {
            return task != null
                && task.Id.Validate()
                && !String.IsNullOrEmpty(task.FirstName)
                && !String.IsNullOrEmpty(task.LastName)
                && task.Email.IsEmail()
                && !String.IsNullOrEmpty(task.Role);
        }

        //Validate UserWord
        public static bool Validate(this UserWord task)
        {
            return task != null
                && task.Id.Validate()
                && task.WordId.Validate()
                && task.UserId.Validate();
        }

        //Validate SentenceTask
        public static bool Validate(this SentenceTask task)
        {
            return task != null
                && task.SentenceTaskId.Validate()
                && !String.IsNullOrEmpty(task.Sentence)
                && !String.IsNullOrEmpty(task.Translate)
                && !String.IsNullOrEmpty(task.Category);
        }

        //Validate RuleModel
        public static bool Validate(this RuleModel task)
        {
            return task != null
                && task.RuleId.Validate()
                && !String.IsNullOrEmpty(task.Text)
                && !String.IsNullOrEmpty(task.Title)
                && !String.IsNullOrEmpty(task.Category);
        }

        //Validate Message
        public static bool Validate(this Message task)
        {
            return task != null
                && task.MessageId.Validate()
                && task.SenderId.Validate()
                && task.BeneficiaryId.Validate()
                && task.Time != null
                && !String.IsNullOrEmpty(task.Text);
        }

        //Validate GuessesTheImageToReturn
        public static bool Validate(this GuessesTheImageToReturn task)
        {
            return task != null
                && task.Id.Validate()
                && task.Word.Validate(true)
                && !String.IsNullOrEmpty(task.Path);
        }
        //Validate Word
        public static bool Validate(this Word task, bool checkTranscription)
        {
            if (checkTranscription)
            {
                if (task == null
                        || task.WordId < 0
                        || task.Original == null
                        || task.Translate == null
                        || task.Transcription == null
                        || task.Category == null
                        || !(task.Original.Length > 0)
                        || !(task.Translate.Length > 0)
                        || !(task.Transcription.Length > 0)
                        || !(task.Category.Length > 0))
                {
                    return false;
                }
            }
            else {
                if (task == null
                        || task.WordId < 0
                        || task.Original == null
                        || task.Translate == null
                        || task.Category == null
                        || !(task.Original.Length > 0)
                        || !(task.Translate.Length > 0)
                        || !(task.Category.Length > 0))
                {
                    return false;
                }
            }
            return true;
        }
        //Validate GuessesTheImageToAdd, CheckTheImage -> (validate)/(not validate) image
        public static bool Validate(this GuessesTheImageToAdd task, bool ValidateTheImage)
        {
            if (ValidateTheImage)
            {
                if (task == null
                    || task.Id < 0
                    || task.WordId < 0
                    || !task.Image.Validate())
                {
                    return false;
                }
            }
            else
            {
                if (task == null
                    || task.Id < 0
                    || task.WordId < 0)
                {
                    return false;
                }
            }
            return true;
        }

        //Validate image
        public static bool Validate(this Image task)
        {
            return task != null
                && !String.IsNullOrEmpty(task.Name)
                && !String.IsNullOrEmpty(task.Data);
        }

        //Validate id
        public static bool Validate(this int id)
        {
            return id >= 0;
        }

        //Validate request string
        public static bool Validate(this string str)
        {
            if (str == null
                || !(str.Length > 0))
            {
                return false;
            }
            return true;
        }

        //saving image to server
        public static string SaveImage(Image image)
        {
            if (image.Validate())
            {
                var bytearr = new List<byte>();
                foreach (var ch in image.Data)
                {
                    bytearr.Add(Convert.ToByte(ch));
                }
                var time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                //path to folder with project
                var path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/uploads/" + time + image.Name));
                if (path == null)
                {
                    return string.Format("Can not be written");
                }
                File.WriteAllBytes(path, bytearr.ToArray());
                return string.Format("http://localhost:58099/uploads/" + time + image.Name);
            }
            throw new System.Net.Http.HttpRequestException("Invalid model");
        }

        //creating hashpassword
        public static string HashPassword(this string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("Invalid password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        //verificating password
        public static bool VerifyHashedPassword(this string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return buffer3.IsEqual(buffer4);
        }

        //comparison of byte arrays
        private static bool IsEqual(this byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }
    }
}