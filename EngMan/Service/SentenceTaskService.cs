using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System.Net.Http;
using System.Text.RegularExpressions;
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
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public IEnumerable<SentenceTask> GetByCategory(string category)
        {
            if (category.Validate())
            {
                try
                {
                    return rep.GetByCategory(category);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<SentenceTask> Get()
        {
            try
            {
                return rep.SentenceTasks;
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public SentenceTask GetById(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.SentenceTasks.FirstOrDefault(x => x.SentenceTaskId == id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<SentenceTask> Edit(SentenceTask task)
        {
            if (task.Validate())
            {
                try
                {
                    return await rep.SaveTask(task);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<SentenceTask> Add(SentenceTask task)
        {
            if (task.Validate())
            {
                try
                {
                    return await rep.AddTask(task);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<int> Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return await rep.DeleteTask(id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public SentenceTask GetTask(string category, string indexes)
        {
            try
            {
                var ParsedIndexes = new List<int>();
                if (indexes.Validate())
                {
                    foreach (var ch in indexes.Split(','))
                    {
                        int i;
                        if (int.TryParse(ch, out i))
                        {
                            ParsedIndexes.Add(i);
                        }
                    }
                }
                if (category.Validate())
                {
                    List<SentenceTask> tasks;
                    if (ParsedIndexes.IsCorrect())
                    {
                        tasks = rep.GetTasks(category, ParsedIndexes).ToList();
                    }
                    else
                    {
                        tasks = rep.GetTasks(category).ToList();
                    }
                    if (tasks != null)
                    {
                        if (tasks.Count() >= (10 - ParsedIndexes.Count()))
                        {
                            var rand = new System.Random();
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
                    }
                }
                throw new HttpRequestException("Invalid model");
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public bool VerificationCorrectness(SentenceTask task)
        {
            if (task.Validate())
            {
                try
                {
                    var _task = GetById(task.SentenceTaskId);
                    if (_task != null)
                    {
                        Regex rx = new Regex("[^a-zA-Zа-яА-Я0-9]");
                        if (rx.Replace(task.Sentence.ToLower(), "").Equals(rx.Replace(_task.Sentence.ToLower(), "")))
                        {
                            return true;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
                return false;
            }
            throw new HttpRequestException("Invalid model");
        }
    }
}