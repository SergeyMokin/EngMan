using System;
using System.Collections.Generic;
using System.Linq;
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
            const int maxCountOfGettingTasks = 9;
            return task != null && task.Count() < maxCountOfGettingTasks;
        }

        //Validate password
        public static bool IsCorrectPassword(this string password)
        {
            const int minLength = 8;
            const int maxLength = 25;

            if (password == null)
            {
                return false;
            }
            if (password.Length < minLength && password.Length > maxLength)
            {
                return false;
            }

            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;
            bool hasNoSpace = true;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCaseLetter = true;
                else if (char.IsLower(c)) hasLowerCaseLetter = true;
                else if (char.IsDigit(c)) hasDecimalDigit = true;
                else if (char.IsWhiteSpace(c)) hasNoSpace = false;
            }
            bool isValid = hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit
                        && hasNoSpace
                        ;

            return isValid;
        }

        //Validate name
        public static bool IsName(this string name)
        {
            return name.FirstOrDefault(x => !char.IsLetter(x)) == default(char) ? true : false;
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

        //Validate User
        public static bool Validate(this User task, bool checkPassword)
        {
            return task != null
                && task.Id.Validate()
                && !String.IsNullOrEmpty(task.FirstName)
                && !String.IsNullOrEmpty(task.LastName)
                && task.Email.IsEmail()
                && !String.IsNullOrEmpty(task.Role)
                && (checkPassword ? task.Password.IsCorrectPassword() : true);
        }

        //Validate Word
        public static bool Validate(this Word task, bool checkTranscription)
        {
            return task != null
                && task.WordId.Validate()
                && !String.IsNullOrEmpty(task.Original)
                && !String.IsNullOrEmpty(task.Translate)
                && !String.IsNullOrEmpty(task.Category)
                && (checkTranscription ? !String.IsNullOrEmpty(task.Transcription) : true);
        }

        //Validate GuessesTheImageToAdd, CheckTheImage -> (validate)/(not validate) image
        public static bool Validate(this GuessesTheImageToAdd task, bool ValidateTheImage)
        {
            return task != null
                && task.Id.Validate()
                && task.WordId.Validate()
                && (ValidateTheImage ? task.Image.Validate() : true);
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

        //saving image to server
        public static string SaveImage(Image image)
        {
            if (!image.Validate()) throw new ArgumentException("Invalid model");
            var bytearr = image.Data.Select(x => Convert.ToByte(x)).ToArray();
            var time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            //path to folder with project
            var path = System.Web.Hosting.HostingEnvironment.MapPath(Path.Combine("~/uploads/", (time + image.Name)));
            if (path == null)
            {
                return string.Format("Can not be written");
            }
            File.WriteAllBytes(path, bytearr);
            return string.Format("http://ecsc00a01a18/uploads/" + time + image.Name);
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
            return buffer3.SequenceEqual(buffer4);
        }
    }
}