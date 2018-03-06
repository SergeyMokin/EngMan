using EngMan.Models;
using EngMan.Repository;
using System.Collections.Generic;
using EngMan.Extensions;
using System;

namespace EngMan.Service
{
    public class UserDictionaryService: IUserDictionaryService
    {
        private readonly IUserDictionaryRepository rep;

        public UserDictionaryService(IUserDictionaryRepository _rep)
        {
            rep = _rep;
        }

        public UserDictionary Get(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.GetUserDictionary(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }
        public bool Add(int id, UserWord word)
        {
            if (id.Validate() && word.Validate())
            {
                try
                {
                    return rep.AddWordToDictionary(id, word);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }
        public int Delete(int userId, int wordId)
        {
            if (userId.Validate() && wordId.Validate())
            {
                try
                {
                    return rep.DeleteWordFromDictionary(userId, wordId);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public IEnumerable<string> GetAllCategories(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.GetAllCategories(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public UserDictionary GetByCategory(int id, string category)
        {
            if (id.Validate() && !String.IsNullOrEmpty(category))
            {
                try
                {
                    return rep.GetByCategory(id, category);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

    }
}