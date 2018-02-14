using EngMan.Models;
namespace EngMan.Repository
{
    public interface IUserDictionaryRepository
    {
        UserDictionary GetUserDictionary(int id);
        UserWord AddWordToDictionary(int id, UserWord word);
        int DeleteWordFromDictionary(int userId, int wordId);
    }
}