using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public class SentenceTaskRepository: ISentenceTaskRepository
    {
        public IEnumerable<SentenceTask> SentenceTasks { get { return context.SentenceTasks; } }

        private EFDbContext context;

        public SentenceTaskRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<SentenceTask> GetByCategory(string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            return context.SentenceTasks.Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public IEnumerable<string> GetAllCategories()
        {
            return context.SentenceTasks.GroupBy(x => x.Category).Select(x => x.Key);
        }

        public async Task<bool> SaveTask(SentenceTask task)
        {
            if (task == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = await context.SentenceTasks.FindAsync(task.SentenceTaskId);
            if (entity != null)
            {
                entity.Sentence = task.Sentence;
                entity.Category = task.Category;
                entity.Translate = task.Translate;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddTask(SentenceTask task)
        {
            if (task == null)
            {
                throw new System.ArgumentNullException();
            }
            if (task.SentenceTaskId == 0)
            {
                context.SentenceTasks.Add(task);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<int> DeleteTask(int id)
        {
            if (id > 0)
            {
                var entity = await context.SentenceTasks.FindAsync(id);
                if (entity != null)
                {
                    context.SentenceTasks.Remove(entity);
                }
                context.SaveChanges();
                return id;
            }
            return -1;
        }

        public IEnumerable<SentenceTask> GetTasks(string category, IEnumerable<int> indexes = default(int[]))
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            var query = @"
                    SELECT [SentenceTaskId]
						  ,[Sentence]
						  ,[Category]
						  ,[Translate]
					FROM [dbo].[SentenceTasks]
                    WHERE LOWER([Category]) LIKE LOWER('" + category + "')";
            foreach (var index in indexes)
            {
                query += (" AND [SentenceTaskId]!=" + index);
            }
            return context.Database.SqlQuery<SentenceTask>(query);
        }
    }
}