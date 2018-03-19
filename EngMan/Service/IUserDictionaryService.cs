using EngMan.Models;
using System.Collections.Generic;
namespace EngMan.Service
{
    public interface IUserDictionaryService
    {
        //get user dictionary
        UserDictionary Get(int id);

        //add word to dictionary of user
        bool Add(int id, UserWord word);

        //delete word from dictionary of user
        string Delete(int userId, int wordId);

        //get all categories of user's words
        IEnumerable<string> GetAllCategories(int id);

        //get words of user by category
        UserDictionary GetByCategory(int id, string category);
    }
}