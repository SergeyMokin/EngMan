using EngMan.Models;
using System.Linq;

namespace EngMan.Repository
{
    public class UserDictionaryRepository : IUserDictionaryRepository
    {
        private readonly EFDbContext context;

        public UserDictionaryRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IQueryable<string> GetAllCategories(int id)
        {
            return context.UserWords
                .Join(context.Words,
                    uw => uw.WordId,
                    word => word.WordId,
                    (uw, word) => new { UserWord = uw, Word = word })
                .Join(context.Users,
                    uw => uw.UserWord.UserId,
                    us => us.Id,
                    (uw, us) => new { uw.UserWord, uw.Word, User = us })
                .Where(x => x.User.Id == id)
                .GroupBy(x => x.Word.Category)
                .Select(x => x.Key);
        }

        public UserDictionary GetByCategory(int id, string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            var result = context.UserWords
                .Join(context.Words,
                    uw => uw.WordId,
                    word => word.WordId,
                    (uw, word) => new { UserWord = uw, Word = word })
                .Join(context.Users,
                    uw => uw.UserWord.UserId,
                    us => us.Id,
                    (uw, us) => new { uw.UserWord, uw.Word, User = us })
                .Where(x => x.User.Id == id && x.Word.Category.Equals(category)).ToList();

            if (result == null)
            {
                throw new System.Exception("Not found");
            }

            var user = result.Select(x => new UserView
            {
                Id = x.User.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                Role = x.User.Role
            }).FirstOrDefault();

            return new UserDictionary
            {
                User = user,
                Words = result.Select(x => new Word
                {
                    WordId = x.Word.WordId,
                    Category = x.Word.Category,
                    Original = x.Word.Original,
                    Transcription = x.Word.Transcription,
                    Translate = x.Word.Translate
                }).ToList()
            }; ;
        }

        public UserDictionary GetUserDictionary(int id)
        {
            var result = context.UserWords
                .Join(context.Words,
                    uw => uw.WordId,
                    word => word.WordId,
                    (uw, word) => new { UserWord = uw, Word = word })
                .Join(context.Users,
                    uw => uw.UserWord.UserId,
                    us => us.Id,
                    (uw, us) => new { uw.UserWord, uw.Word, User = us })
                .Where(x => x.User.Id == id).ToList();

            if (result == null)
            {
                throw new System.Exception("Not found");
            }

            var user = result.Select(x => new UserView
            {
                Id = x.User.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                Email = x.User.Email,
                Role = x.User.Role
            }).FirstOrDefault();

            return new UserDictionary
            {
                User = user,
                Words = result.Select(x => new Word
                {
                    WordId = x.Word.WordId,
                    Category = x.Word.Category,
                    Original = x.Word.Original,
                    Transcription = x.Word.Transcription,
                    Translate = x.Word.Translate
                }).ToList()
            }; 
        }

        public bool Add(int id, UserWord word)
        {
            if (word == null)
            {
                throw new System.ArgumentNullException();
            }
            if (word.UserId != id)
            {
                return false;
            }
            var entity = context.UserWords.Where(x => x.WordId == word.WordId).FirstOrDefault();
            if (entity != null)
            {
                return false;
            }
            context.UserWords.Add(word);
            context.SaveChanges();
            return true;
        }

        public int Delete(int userId, int wordId)
        {
            if (wordId < 1)
            {
                return -1;
            }
            var entity = context.UserWords.Where(x => x.WordId == wordId).FirstOrDefault();
            if (entity == null && entity.UserId != userId)
            {
                return -1;
            }
            context.UserWords.Remove(entity);
            context.SaveChanges();
            return wordId;
        }
    }
}