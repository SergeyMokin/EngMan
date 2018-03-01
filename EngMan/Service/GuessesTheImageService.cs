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
            if (image.Validate(true))
            {
                try
                {
                    var result = rep.Add(image);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public bool Edit(GuessesTheImageToAdd image)
        {
            if (image.Validate(false))
            {
                try
                {
                    var result = rep.Edit(image);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public IEnumerable<GuessesTheImageToReturn> GetAll()
        {
            try
            {
                return rep.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GuessesTheImageToReturn Get(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.Get(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public int Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.Delete(id);
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

        public IEnumerable<GuessesTheImageToReturn> GetByCategory(string category)
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

        public GuessesTheImageToReturn GetTask(string category, string indexes)
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
                    List<GuessesTheImageToReturn> tasks;
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
                        if (tasks.Count() >= 1)
                        {
                            var rand = new System.Random();
                            var index = rand.Next(0, tasks.Count());
                            return tasks.ElementAt(index);
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

        public bool VerificationCorrectness(GuessesTheImageToReturn img)
        {
            if (img.Validate())
            {
                try
                {
                    var task = rep.Get(img.Id);
                    if (task != null)
                    {
                        Regex rx = new Regex("[^a-zA-Zа-яА-Я0-9]");
                        if (rx.Replace(task.Word.Original.ToLower(), "").Equals(rx.Replace(img.Word.Original.ToLower(), "")))
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