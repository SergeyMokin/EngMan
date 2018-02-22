using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
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
            return context.Words.Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public IEnumerable<string> GetAllCategories()
        {
            return context.Words.GroupBy(x => x.Category).Select(x => x.Key);
        }

        public async Task<Word> SaveWord(Word word)
        {
            if (word.WordId == 0)
            {
                context.Words.Add(word);
                context.SaveChanges();
                word.WordId = context.Words.ToArray().Last().WordId;
            }
            else
            {
                var entity = await context.Words.FindAsync(word.WordId);
                if (entity != null)
                {
                    entity.Original = word.Original;
                    entity.Translate = word.Translate;
                    entity.Category = word.Category;
                    entity.Transcription = word.Transcription;
                }
            }
            context.SaveChanges();
            return word;
        }

        public async Task<Word> AddWord(Word word)
        {
            return await SaveWord(word);
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
            var query = @"
                    SELECT [WordId]
						  ,[Original]
						  ,[Translate]
						  ,[Category]
						  ,[Transcription]
					FROM [dbo].[Words]
                    WHERE LOWER([Category]) LIKE LOWER('" + category + "')";
            foreach (var index in indexes)
            {
                query += (" AND [WordId]!=" + index);
            }
            return context.Database.SqlQuery<Word>(query);
        }
    }
}