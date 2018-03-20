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
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.GetUserDictionary(id);
        }
        public bool Add(int id, UserWord word)
        {
            if (!id.Validate() && !word.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.AddWordToDictionary(id, word);
        }
        public string Delete(int userId, int wordId)
        {
            if (!userId.Validate() && !wordId.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.DeleteWordFromDictionary(userId, wordId) > 0
                    ? "Delete completed successful"
                    : null;
        }

        public IEnumerable<string> GetAllCategories(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.GetAllCategories(id);
        }

        public UserDictionary GetByCategory(int id, string category)
        {
            if (!id.Validate() && String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }
            return rep.GetByCategory(id, category);
        }

    }
}