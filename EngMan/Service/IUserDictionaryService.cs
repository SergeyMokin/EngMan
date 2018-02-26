using EngMan.Models;
using System.Collections.Generic;
namespace EngMan.Service
{
    public interface IUserDictionaryService
    {
        UserDictionary Get(int id);
        bool Add(int id, UserWord word);
        int Delete(int userId, int wordId);
        IEnumerable<string> GetAllCategories(int id);
        UserDictionary GetByCategory(int id, string category);
    }
}