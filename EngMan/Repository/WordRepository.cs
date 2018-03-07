using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
using System.Data.SqlClient;

namespace EngMan.Repository
{
    public class WordRepository : IWordRepository
    {
        public IEnumerable<Word> Words { get { return context.Words; } }

        private EFDbContext context;

        public WordRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Word> GetByCategory(string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            return context.Words.Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public IEnumerable<string> GetAllCategories()
        {
            return context.Words.GroupBy(x => x.Category).Select(x => x.Key);
        }

        public async Task<bool> SaveWord(Word word)
        {
            if (word == null)
            {
                throw new System.ArgumentNullException();
            }
            if (word.WordId >= 1)
            {
                var entity = await context.Words.FindAsync(word.WordId);
                if (entity != null)
                {
                    entity.Original = word.Original;
                    entity.Translate = word.Translate;
                    entity.Category = word.Category;
                    entity.Transcription = word.Transcription;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            context.SaveChanges();
            return true;
        }

        public bool AddWord(Word word)
        {
            if (word == null)
            {
                throw new System.ArgumentNullException();
            }
            if (word.WordId <= 0)
            {
                context.Words.Add(word);
                context.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        public async Task<int> DeleteWord(int id)
        {
            if (id > 0)
            {
                var entity = await context.Words.FindAsync(id);
                if (entity != null)
                {
                    context.Words.Remove(entity);
                }
                context.SaveChanges();
                return id;
            }
            return -1;
        }

        public IEnumerable<Word> GetTasks(string category, IEnumerable<int> indexes = default(int[]))
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            var parameters = new object[indexes.Count() + 1];
            parameters[0] = new SqlParameter("category", category);
            var query = @"
                    SELECT [WordId]
						  ,[Original]
						  ,[Translate]
						  ,[Category]
						  ,[Transcription]
					FROM [dbo].[Words]
                    WHERE LOWER([Category]) LIKE LOWER(@category)";
            for (var i = 0; i < indexes.Count(); i++)
            {
                query += (" AND [WordId] != @index" + i);
                parameters[i + 1] = new SqlParameter(("index" + i), indexes.ElementAt(i));
            }
            return context.Database.SqlQuery<Word>(query, parameters);
        }
    }
}