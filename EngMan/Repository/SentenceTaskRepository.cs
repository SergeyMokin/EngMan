using System.Collections.Generic;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public class SentenceTaskRepository: ISentenceTaskRepository
    {
        private EFDbContext context;

        public SentenceTaskRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IQueryable<SentenceTask> GetAll()
        {
            return context.SentenceTasks;
        }

        public SentenceTask Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.SentenceTaskId == id);
        }

        public bool Add(SentenceTask task)
        {
            if (task == null)
            {
                throw new System.ArgumentNullException();
            }
            if (task.SentenceTaskId > 0)
            {
                return false;
            }
            context.SentenceTasks.Add(task);
            context.SaveChanges();
            return true;
        }

        public bool Edit(SentenceTask task)
        {
            if (task == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.SentenceTasks.Find(task.SentenceTaskId);
            if (entity == null)
            {
                return false;
            }
            entity.Sentence = task.Sentence;
            entity.Category = task.Category;
            entity.Translate = task.Translate;
            context.SaveChanges();
            return true;
        }

        public int Delete(int id)
        {
            if (id <= 0)
            {
                return -1;
            }
            var entity = context.SentenceTasks.Find(id);
            if (entity == null)
            {
                return -1;
            }
            context.SentenceTasks.Remove(entity);
            context.SaveChanges();
            return id;
        }

        public IQueryable<SentenceTask> GetByCategory(string category)
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

        public IQueryable<SentenceTask> GetTasks(string category, IEnumerable<int> indexes = null)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }

            var query = GetByCategory(category);
            var length = indexes == null ? 0 : indexes.Count();

            for (var i = 0; i < length; i++)
            {
                query = query.Where(x => x.SentenceTaskId != indexes.ElementAt(i));
            }
            return query;
        }
    }
}