using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
using System.Data.SqlClient;

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

        public bool AddTask(SentenceTask task)
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

        public async Task<int> DeleteTask(int id)
        {
            if (id <= 0)
            {
                return -1;
            }
            var entity = await context.SentenceTasks.FindAsync(id);
            if (entity == null)
            {
                return -1;
            }
            context.SentenceTasks.Remove(entity);
            context.SaveChanges();
            return id;
        }

        public IEnumerable<SentenceTask> GetTasks(string category, IEnumerable<int> indexes = default(int[]))
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            var parameters = new object[indexes.Count() + 1];
            parameters[0] = new SqlParameter("category", category);
            var query = @"
                    SELECT [SentenceTaskId]
						  ,[Sentence]
						  ,[Category]
						  ,[Translate]
					FROM [dbo].[SentenceTasks]
                    WHERE LOWER([Category]) LIKE LOWER(@category)";
            for (var i = 0; i < indexes.Count(); i++)
            {
                query += (" AND [SentenceTaskId] != @index" + i);
                parameters[i + 1] = new SqlParameter(("index" + i), indexes.ElementAt(i));
            }
            return context.Database.SqlQuery<SentenceTask>(query, parameters);
        }
    }
}