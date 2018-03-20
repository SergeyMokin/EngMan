using System.Collections.Generic;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public class WordRepository : IWordRepository
    {
        private EFDbContext context;

        public WordRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Word> GetAll()
        {
            return context.Words;
        }

        public Word Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.WordId == id);
        }

        public bool Add(Word word)
        {
            if (word == null)
            {
                throw new System.ArgumentNullException();
            }
            if (word.WordId > 0)
            {
                return false;
            }
            context.Words.Add(word);
            context.SaveChanges();
            return true;
        }

        public bool Edit(Word word)
        {
            if (word == null)
            {
                throw new System.ArgumentNullException();
            }
            if (word.WordId < 1)
            {
                return false;
            }
            var entity = context.Words.Find(word.WordId);
            if (entity == null)
            {
                return false;
            }
            entity.Original = word.Original;
            entity.Translate = word.Translate;
            entity.Category = word.Category;
            entity.Transcription = word.Transcription;
            context.SaveChanges();
            return true;
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                return -1;
            }
            var entity = context.Words.Find(id);
            if (entity == null)
            {
                return -1;
            }
            context.Words.Remove(entity);
            context.SaveChanges();
            return id;
        }

        public IQueryable<Word> GetByCategory(string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            return GetAll().Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public IQueryable<string> GetAllCategories()
        {
            return GetAll().GroupBy(x => x.Category).Select(x => x.Key);
        }

        public IQueryable<Word> GetTasks(string category, IEnumerable<int> indexes = null)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }

            var query = GetByCategory(category);
            var length = indexes == null ? 0 : indexes.Count();

            for (var i = 0; i < length; i++)
            {
                query = query.Where(x => x.WordId != indexes.ElementAt(i));
            }
            return query;
        }
    }
}