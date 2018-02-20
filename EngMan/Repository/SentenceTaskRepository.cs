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
            return context.SentenceTasks.Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public IEnumerable<string> GetAllCategories()
        {
            return context.SentenceTasks.GroupBy(x => x.Category).Select(x => x.Key);
        }

        public async Task<SentenceTask> SaveTask(SentenceTask task)
        {
            if (task.SentenceTaskId == 0)
            {
                context.SentenceTasks.Add(task);
                context.SaveChanges();
                task.SentenceTaskId = context.SentenceTasks.ToArray().Last().SentenceTaskId;
            }
            else
            {
                var entity = await context.SentenceTasks.FindAsync(task.SentenceTaskId);
                if (entity != null)
                {
                    entity.Sentence = task.Sentence;
                    entity.Category = task.Category;
                    entity.Translate = task.Translate;
                }
            }
            context.SaveChanges();
            return task;
        }

        public async Task<SentenceTask> AddTask(SentenceTask task)
        {
            return await SaveTask(task);
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
    }
}