using EngMan.Models;
using System.Collections.Generic;
namespace EngMan.Repository
{
    public interface IUserDictionaryRepository
    {
        UserDictionary GetUserDictionary(int id);

        bool AddWordToDictionary(int id, UserWord word);

        int DeleteWordFromDictionary(int userId, int wordId);

        IEnumerable<string> GetAllCategories(int id);

        UserDictionary GetByCategory(int id, string category);
    }
}