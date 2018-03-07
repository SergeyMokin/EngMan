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
            try
            {
                return rep.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<SentenceTask> GetByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.GetByCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<SentenceTask> Get()
        {
            try
            {
                return rep.SentenceTasks;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SentenceTask GetById(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.SentenceTasks.FirstOrDefault(x => x.SentenceTaskId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Edit(SentenceTask task)
        {
            if (!task.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return await rep.SaveTask(task);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Add(SentenceTask task)
        {
            if (!task.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.AddTask(task);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return await rep.DeleteTask(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SentenceTask GetTask(string category, string indexes)
        {
            try
            {
                var ParsedIndexes = new List<int>();
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
                List<SentenceTask> tasks;
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
                var rand = new Random();
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificationCorrectness(SentenceTask task)
        {
            if (!task.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                var _task = GetById(task.SentenceTaskId);
                if (_task == null)
                {
                    throw new Exception("Invalid model");
                }
                Regex rx = new Regex("[^a-zA-Zа-яА-Я0-9]");
                if (rx.Replace(task.Sentence.ToLower(), "").Equals(rx.Replace(_task.Sentence.ToLower(), "")))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
    }
}