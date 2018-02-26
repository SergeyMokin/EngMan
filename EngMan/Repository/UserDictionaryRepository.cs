using EngMan.Models;
using System.Linq;
using System.Collections.Generic;
namespace EngMan.Repository
{
    public class UserDictionaryRepository : IUserDictionaryRepository
    {
        private readonly EFDbContext context;

        public UserDictionaryRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<string> GetAllCategories(int id)
        {
            return context.Database.SqlQuery<string>(@"
	              SELECT w.Category
	              FROM [EngMan].[dbo].[UserWords] uw
	              JOIN [EngMan].[dbo].[Words] w ON w.WordId = uw.WordId
	              JOIN [EngMan].[dbo].[Users] u ON u.Id = uw.UserId
	              WHERE u.Id = " + id
                  + @"GROUP BY w.Category").ToList();
        }

        public UserDictionary GetByCategory(int id, string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            var result = context.Database.SqlQuery<UserWordSqlScript>(@"
	              SELECT uw.Id [Id]
                  ,u.Id [UserId]
	              ,u.FirstName 
	              ,u.LastName 
	              ,u.Email
	              ,u.Role 
                  ,w.WordId 
                  ,w.Original
                  ,w.Translate 
                  ,w.Category
                  ,w.Transcription
	              FROM [EngMan].[dbo].[UserWords] uw
	              JOIN [EngMan].[dbo].[Words] w ON w.WordId = uw.WordId
	              JOIN [EngMan].[dbo].[Users] u ON u.Id = uw.UserId
	              WHERE u.Id = " + id
                  + "AND w.Category LIKE '" + category + "'").ToList();
            if (result != null)
            {
                var user = result.Select(x => new UserView
                {
                    Id = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role
                }).FirstOrDefault();
                var dictionary = new UserDictionary
                {
                    User = user,
                    Words = result.Select(x => new Word
                    {
                        WordId = x.WordId,
                        Category = x.Category,
                        Original = x.Original,
                        Transcription = x.Transcription,
                        Translate = x.Translate
                    }).ToList()
                };
                return dictionary;
            }
            throw new System.Exception("Not found");
        }

        public UserDictionary GetUserDictionary(int id)
        {
            var result = context.Database.SqlQuery<UserWordSqlScript>(@"
	              SELECT uw.Id [Id]
                  ,u.Id [UserId]
	              ,u.FirstName 
	              ,u.LastName 
	              ,u.Email
	              ,u.Role 
                  ,w.WordId 
                  ,w.Original
                  ,w.Translate 
                  ,w.Category
                  ,w.Transcription
	              FROM [EngMan].[dbo].[UserWords] uw
	              JOIN [EngMan].[dbo].[Words] w ON w.WordId = uw.WordId
	              JOIN [EngMan].[dbo].[Users] u ON u.Id = uw.UserId
	              WHERE u.Id = " + id).ToList();
            if (result != null)
            {
                var user = result.Select(x => new UserView
                {
                    Id = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role
                }).FirstOrDefault();
                var dictionary = new UserDictionary
                {
                    User = user,
                    Words = result.Select(x => new Word
                    {
                        WordId = x.WordId,
                        Category = x.Category,
                        Original = x.Original,
                        Transcription = x.Transcription,
                        Translate = x.Translate
                    }).ToList()
                };
                return dictionary;
            }
            throw new System.Exception("Not found");
        }

        public bool AddWordToDictionary(int id, UserWord word)
        {
            if (word == null)
            {
                throw new System.ArgumentNullException();
            }
            if (word.UserId == id)
            {
                var entity = context.UserWords.Where(x => x.WordId == word.WordId).FirstOrDefault();
                if (entity == null)
                {
                    context.UserWords.Add(word);
                }
                else
                {
                    return false;
                }
            }
            context.SaveChanges();
            return true;
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
            return -1;
        }
    }
}