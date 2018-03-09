using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System;
using System.Text.RegularExpressions;

namespace EngMan.Service
{
    public class WordService: IWordService
    {
        private readonly IWordRepository rep;

        public WordService(IWordRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<Word> GetByCategory(string category)
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

        public IEnumerable<Word> Get()
        {
            try
            {
                return rep.Words;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Word GetById(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.Words.FirstOrDefault(x => x.WordId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Edit(Word word)
        {
            if (!word.Validate(true))
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return await rep.SaveWord(word);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Add(Word word)
        {
            if (!word.Validate(true))
            {
                throw new Exception("Invalid model");
            }
            try
            {
                return rep.AddWord(word);
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
                return await rep.DeleteWord(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MapWord GetTask(string category, string indexes, bool translate)
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
                List<Word> tasks;
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
                    throw new Exception("Invalid model");
                }
                if (tasks.Count() < 5)
                {
                    throw new Exception("Few words");
                }
                var rand = new Random();
                var index = rand.Next(0, tasks.Count());
                var indexesOfAnswers = new HashSet<int>()
                            {
                                index
                            };
                while (indexesOfAnswers.Count() != 5)
                {
                    indexesOfAnswers.Add(rand.Next(0, tasks.Count()));
                }
                var word = tasks.ElementAt(index);
                var answers = new List<string>();
                foreach (var i in indexesOfAnswers)
                {
                    if (translate)
                    {
                        answers.Add(tasks.ElementAt(i).Translate);
                    }
                    else
                    {
                        answers.Add(tasks.ElementAt(i).Original);
                    }
                }
                if (word == null)
                {
                    throw new Exception("Invalid model");
                }
                return new MapWord
                {
                    WordId = word.WordId,
                    Word = translate ? word.Original : word.Translate,
                    Answers = answers.OrderBy(x => x.OrderBy(y => y).ToString()[x.Count() / 2 - 1]).ToList(),
                    Category = word.Category
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificationCorrectness(Word task, bool translate)
        {
            if (!task.Validate(false))
            {
                throw new Exception("Invalid model");
            }
            try
            {
                var _task = GetById(task.WordId);
                if (_task == null)
                {
                    throw new Exception("Invalid model");
                }
                Regex rx = new Regex("[^a-zA-Zа-яА-Я0-9]");
                if (translate)
                {
                    if (rx.Replace(task.Translate.ToLower(), "").Equals(rx.Replace(_task.Translate.ToLower(), "")))
                    {
                        return true;
                    }
                }
                else
                {
                    if (rx.Replace(task.Original.ToLower(), "").Equals(rx.Replace(_task.Original.ToLower(), "")))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}