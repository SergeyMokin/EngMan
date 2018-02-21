using EngMan.Models;
using System.Collections.Generic;
namespace EngMan.Repository
{
    public interface IUserDictionaryRepository
    {
        UserDictionary GetUserDictionary(int id);
        UserWord AddWordToDictionary(int id, UserWord word);
        int DeleteWordFromDictionary(int userId, int wordId);
        IEnumerable<string> GetAllCategories(int id);
        UserDictionary GetUserDictionaryByCategory(int id, string category);
    }
}