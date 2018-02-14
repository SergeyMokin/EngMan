using EngMan.Models;
using System.Linq;
namespace EngMan.Repository
{
    public class UserDictionaryRepository: IUserDictionaryRepository
    {
        private readonly EFDbContext context;

        public UserDictionaryRepository(EFDbContext _context)
        {
            context = _context;
        }

        public UserDictionary GetUserDictionary(int id)
        {
            return context.UserWords.Where(x => x.UserId == id).Select(x => new UserDictionary
            {
                User = context.Users.Select(user => new UserView
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role
                }).FirstOrDefault(),
                Words = context.UserWords.Where(usrwrd => usrwrd.UserId == id).Select(word => context.Words.FirstOrDefault(wrd => wrd.WordId == word.WordId))
            }).FirstOrDefault();
        }

        public UserWord AddWordToDictionary(int id, UserWord word)
        {
            if (word != null)
            {
                if (word.UserId == id)
                {
                    var entity = context.UserWords.Where(x => x.WordId == word.WordId).FirstOrDefault();
                    if (entity == null)
                    {
                        context.UserWords.Add(word);
                    }
                    else
                    {
                        return word;
                    }
                }
                context.SaveChanges();
                word.Id = context.UserWords.ToArray().Last().Id;
            }
            return word;
        }

        public int DeleteWordFromDictionary(int userId, int wordId)
        {
            if (wordId > 0)
            {
                var entity = context.UserWords.Where(x => x.WordId == wordId).FirstOrDefault();
                if (entity != null && entity.UserId == userId)
                {
                    context.UserWords.Remove(entity);
                }
                context.SaveChanges();
                return wordId;
            }
            return 0;
        }
    }
}