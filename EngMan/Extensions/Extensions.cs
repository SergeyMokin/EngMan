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
            if (task == null
                || task.Count() > 9)
            {
                return false;
            }
            return true;
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
        //Validate email
        public static bool IsEmail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        //Validate User
        public static bool Validate(this User task)
        {
            if (task == null
                || task.Id < 0
                || task.FirstName == null
                || task.LastName == null
                || task.Email == null
                || task.Password == null
                || task.Role == null
                || !(task.FirstName.Length > 0)
                || !(task.LastName.Length > 0)
                || !task.Email.IsEmail()
                || !(task.Password.Length > 0)
                || !(task.Role.Length > 0))
            {
                return false;
            }
            return true;
        }
        //Validate UserView
        public static bool Validate(this UserView task)
        {
            if (task == null
                || task.Id < 0
                || task.FirstName == null
                || task.LastName == null
                || task.Email == null
                || task.Role == null
                || !(task.FirstName.Length > 0)
                || !(task.LastName.Length > 0)
                || !task.Email.IsEmail()
                || !(task.Role.Length > 0))
            {
                return false;
            }
            return true;
        }
        //Validate UserWord
        public static bool Validate(this UserWord task)
        {
            if (task == null
                || task.Id < 0
                || !(task.UserId > 0)
                || !(task.WordId > 0))
            {
                return false;
            }
            return true;
        }
        //Validate SentenceTask
        public static bool Validate(this SentenceTask task)
        {
            if (task == null
                || task.SentenceTaskId < 0
                || task.Sentence == null
                || task.Translate == null
                || task.Category == null
                || !(task.Sentence.Length > 0)
                || !(task.Translate.Length > 0)
                || !(task.Category.Length > 0))
            {
                return false;
            }
            return true;
        }
        //Validate RuleModel
        public static bool Validate(this RuleModel task)
        {
            if (task == null
                || task.RuleId < 0
                || task.Text == null
                || task.Title == null
                || task.Category == null
                || !(task.Text.Length > 0)
                || !(task.Title.Length > 0)
                || !(task.Category.Length > 0))
            {
                return false;
            }
            return true;
        }
        //Validate Message
        public static bool Validate(this Message task)
        {
            if (task == null
                    || task.MessageId < 0
                    || !(task.SenderId > 0)
                    || !(task.BeneficiaryId > 0)
                    || task.Time == null
                    || task.Text == null
                    || !(task.Text.Length > 0))
            {
                return false;
            }
            return true;
        }
        //Validate GuessesTheImageToReturn
        public static bool Validate(this GuessesTheImageToReturn task)
        {
            if (task == null
                    || task.Id < 0
                    || !task.Word.Validate(true)
                    || task.Path == null
                    || !(task.Path.Length > 0))
            {
                return false;
            }
            return true;
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
            if(task == null
                || task.Name == null
                || task.Data == null
                || !(task.Name.Length > 0)
                || !(task.Data.Length > 0))
            {
                return false;
            }
            return true;
        }

        //Validate id
        public static bool Validate(this int id)
        {
            if (!(id > 0))
            {
                return false;
            }
            return true;
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
                throw new ArgumentNullException("password");
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