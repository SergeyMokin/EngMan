using System.Collections.Generic;
using EngMan.Repository;
using EngMan.Models;
using EngMan.Extensions;
using System.Net.Http;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace EngMan.Service
{
    public class GuessesTheImageService : IGuessesTheImageService
    {
        IGuessesTheImageRepository rep;

        public GuessesTheImageService(IGuessesTheImageRepository _rep)
        {
            rep = _rep;
        }
        
        public bool Add(GuessesTheImageToAdd image)
        {
            if (!image.Validate(true))
            {
                throw new Exception("Invalid model");
            }
            return rep.Add(image);
        }

        public bool Edit(GuessesTheImageToAdd image)
        {
            if (!image.Validate(false))
            {
                throw new Exception("Invalid model");
            }
            return rep.Edit(image);
        }

        public IQueryable<GuessesTheImageToReturn> GetAll()
        {
            return rep.GetAll();
        }

        public GuessesTheImageToReturn Get(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Get(id);
        }

        public string Delete(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Delete(id) > 0
                    ? "Delete completed successful"
                    : null; ;
        }


        public IQueryable<string> GetAllCategories()
        {
            return rep.GetAllCategories();
        }

        public IQueryable<GuessesTheImageToReturn> GetByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }
            return rep.GetByCategory(category);
        }

        public GuessesTheImageToReturn GetTask(string category, string indexes)
        {
            List<GuessesTheImageToReturn> tasks;
            var ParsedIndexes = new List<int>();
            var rand = new System.Random();

            if (!String.IsNullOrEmpty(indexes))
            {
                foreach (var ch in indexes.Split(','))
                {
                    if (int.TryParse(ch, out int i))
                    {
                        ParsedIndexes.Add(i);
                    }
                }
            }

            if (String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }

            if (!ParsedIndexes.IsCorrect())
            {
                throw new Exception("End of tasks");
            }

            tasks = rep.GetTasks(category, ParsedIndexes).ToList();

            if (tasks == null || tasks.Count() < 1)
            {
                throw new Exception("Invalid model");
            }

            var index = rand.Next(0, tasks.Count());

            return tasks.ElementAt(index);
        }

        public bool VerificationCorrectness(GuessesTheImageToReturn img)
        {
            const string lettersAndNumbers = "[^a-zA-Zа-яА-Я0-9]";

            Regex rx = new Regex(lettersAndNumbers);

            if (!img.Validate())
            {
                throw new HttpRequestException("Invalid model");
            }

            var task = rep.Get(img.Id);

            if (task == null)
            {
                return false;
            }

            return rx.Replace(task.Word.Original.ToLower(), "").Equals(rx.Replace(img.Word.Original.ToLower(), ""));
        }
    }
}