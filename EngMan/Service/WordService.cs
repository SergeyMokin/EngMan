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
            if (category.Validate())
            {
                try
                {
                    return rep.GetByCategory(category);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
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
            if (id.Validate())
            {
                try
                {
                    return rep.Words.FirstOrDefault(x => x.WordId == id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public async Task<bool> Edit(Word word)
        {
            if (word.Validate(true))
            {
                try
                {
                    return await rep.SaveWord(word);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public bool Add(Word word)
        {
            if (word.Validate(true))
            {
                try
                {
                    return rep.AddWord(word);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public async Task<int> Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return await rep.DeleteWord(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public MapWord GetTask(string category, string indexes)
        {
            try
            {
                var ParsedIndexes = new List<int>();
                if (indexes.Validate())
                {
                    foreach (var ch in indexes.Split(','))
                    {
                        if (int.TryParse(ch, out int i))
                        {
                            ParsedIndexes.Add(i);
                        }
                    }
                }
                if (category.Validate())
                {
                    List<Word> tasks;
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
                        if (tasks.Count() >= (5 - ParsedIndexes.Count()))
                        {
                            var rand = new Random();
                            var index = rand.Next(0, tasks.Count());
                            var indexesTranslate = new HashSet<int>()
                            {
                                index
                            };
                            while (indexesTranslate.Count() != 5)
                            {
                                indexesTranslate.Add(rand.Next(0, tasks.Count()));
                            }
                            var word = tasks.ElementAt(index);
                            var translate = new List<string>();
                            foreach (var i in indexesTranslate)
                            {
                                translate.Add(tasks.ElementAt(i).Translate);
                            }
                            if (word != null)
                            {
                                return new MapWord
                                {
                                    WordId = word.WordId,
                                    Original = word.Original,
                                    Translate = translate.OrderBy(x => x.OrderBy(y => y).ToString()[x.Count() / 2 - 1]).ToList(),
                                    Category = word.Category
                                };
                            }
                        }
                        else
                        {
                            throw new Exception("Few words");
                        }
                    }
                }
                throw new Exception("Invalid model");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificationCorrectness(Word task)
        {
            if (task.Validate(false))
            {
                try
                {
                    var _task = GetById(task.WordId);
                    if (_task != null)
                    {
                        Regex rx = new Regex("[^a-zA-Zа-яА-Я0-9]");
                        if (rx.Replace(task.Translate.ToLower(), "").Equals(rx.Replace(_task.Translate.ToLower(), "")))
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return false;
            }
            throw new Exception("Invalid model");
        }
    }
}