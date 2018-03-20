using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System.Text.RegularExpressions;
using System;
namespace EngMan.Service
{
    public class SentenceTaskService: ISentenceTaskService
    {
        private readonly ISentenceTaskRepository rep;

        public SentenceTaskService(ISentenceTaskRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<string> GetAllCategories()
        {
            return rep.GetAllCategories();
        }

        public IEnumerable<SentenceTask> GetByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }
            return rep.GetByCategory(category);
        }

        public IEnumerable<SentenceTask> Get()
        {
            return rep.GetAll();
        }

        public SentenceTask GetById(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Get(id);
        }

        public async Task<bool> Edit(SentenceTask task)
        {
            if (!task.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Edit(task);
        }

        public bool Add(SentenceTask task)
        {
            if (!task.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Add(task);
        }

        public async Task<string> Delete(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Delete(id) > 0
                    ? "Delete completed successful"
                    : null;
        }

        public SentenceTask GetTask(string category, string indexes)
        {
            var ParsedIndexes = new List<int>();
            List<SentenceTask> tasks;
            var rand = new Random();

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

            if (ParsedIndexes.IsCorrect())
            {
                tasks = rep.GetTasks(category, ParsedIndexes).ToList();
            }
            else
            {
                tasks = rep.GetTasks(category).ToList();
            }

            if (tasks == null)
            {
                throw new Exception("Has no tasks");
            }

            if (tasks.Count() == 0)
            {
                throw new Exception("Has no tasks");
            }

            var index = rand.Next(0, tasks.Count());

            return tasks.Select(x =>
            {
                var arr = x.Sentence.Split(new[] { ' ' });
                var set = new HashSet<int>();
                while (set.Count() != arr.Length)
                {
                    set.Add(rand.Next(0, arr.Length));
                }
                var returnArr = new string[arr.Length];
                var i = 0;
                foreach (var ind in set)
                {
                    returnArr[i] = arr[ind];
                    i++;
                }
                return new SentenceTask { SentenceTaskId = x.SentenceTaskId, Sentence = string.Join(" ", returnArr), Category = x.Category, Translate = x.Translate };
            }).ElementAt(index);
        }

        public bool VerificationCorrectness(SentenceTask task)
        {
            const string lettersAndNumbers = "[^a-zA-Zа-яА-Я0-9]";

            Regex rx = new Regex(lettersAndNumbers);

            if (!task.Validate())
            {
                throw new Exception("Invalid model");
            }

            var _task = GetById(task.SentenceTaskId);

            if (_task == null)
            {
                throw new Exception("Invalid model");
            }

            return rx.Replace(task.Sentence.ToLower(), "").Equals(rx.Replace(_task.Sentence.ToLower(), ""));
        }
    }
}