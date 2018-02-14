using EngMan.Models;
using EngMan.Repository;
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
            return rep.GetUserDictionary(id);
        }
        public UserWord Add(int id, UserWord word)
        {
            return rep.AddWordToDictionary(id, word);
        }
        public int Delete(int userId, int wordId)
        {
            return rep.DeleteWordFromDictionary(userId, wordId);
        }
    }
}