using EngMan.Models;
using EngMan.Repository;
using System.Collections.Generic;
using EngMan.Extensions;
using System.Net.Http;

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
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }
        public UserWord Add(int id, UserWord word)
        {
            if (id.Validate() && word.Validate())
            {
                try
                {
                    return rep.AddWordToDictionary(id, word);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }
        public int Delete(int userId, int wordId)
        {
            if (userId.Validate() && wordId.Validate())
            {
                try
                {
                    return rep.DeleteWordFromDictionary(userId, wordId);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<string> GetAllCategories(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.GetAllCategories(id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public UserDictionary GetUserDictionaryByCategory(int id, string category)
        {
            if (id.Validate() && category.Validate())
            {
                try
                {
                    return rep.GetUserDictionaryByCategory(id, category);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

    }
}