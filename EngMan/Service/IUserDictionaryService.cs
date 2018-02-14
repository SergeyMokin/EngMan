using EngMan.Models;
namespace EngMan.Service
{
    public interface IUserDictionaryService
    {
        UserDictionary Get(int id);
        UserWord Add(int id, UserWord word);
        int Delete(int userId, int wordId);
    }
}