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
            return rep.GetByCategory(category);
        }

        public IEnumerable<string> GetAllCategories()
        {
            return rep.GetAllCategories();
        }

        public IEnumerable<Word> Get()
        {
            return rep.Words;
        }

        public Word GetById(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Words.FirstOrDefault(x => x.WordId == id);
        }

        public async Task<bool> Edit(Word word)
        {
            if (!word.Validate(true))
            {
                throw new Exception("Invalid model");
            }
            return await rep.SaveWord(word);
        }

        public bool Add(Word word)
        {
            if (!word.Validate(true))
            {
                throw new Exception("Invalid model");
            }
            return rep.AddWord(word);
        }

        public async Task<string> Delete(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return await rep.DeleteWord(id) > 0
                    ? "Delete completed successful"
                    : null;
        }

        public MapWord GetTask(string category, string indexes, bool translate)
        {
            var ParsedIndexes = new List<int>();
            List<Word> tasks;
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
                throw new Exception("Invalid model");
            }

            if (tasks.Count() < 5)
            {
                throw new Exception("Few words");
            }

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

        //translate: check the translation, !translate: check the original word
        public bool VerificationCorrectness(Word task, bool translate)
        {
            const string lettersAndNumbers = "[^a-zA-Zа-яА-Я0-9]";

            Regex rx = new Regex(lettersAndNumbers);

            if (!task.Validate(false))
            {
                throw new Exception("Invalid model");
            }

            var _task = GetById(task.WordId);

            if (_task == null)
            {
                throw new Exception("Task not found");
            }

            if (translate)
            {
                return rx.Replace(task.Translate.ToLower(), "").Equals(rx.Replace(_task.Translate.ToLower(), ""));
            }
            else
            {
                return rx.Replace(task.Original.ToLower(), "").Equals(rx.Replace(_task.Original.ToLower(), ""));
            }
        }
    }
}