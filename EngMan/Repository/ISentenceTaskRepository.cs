using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface ISentenceTaskRepository
    {
        IEnumerable<SentenceTask> SentenceTasks { get; }

        Task<SentenceTask> SaveTask(SentenceTask task);

        Task<SentenceTask> AddTask(SentenceTask task);

        Task<int> DeleteTask(int id);

        IEnumerable<string> GetAllCategories();

        IEnumerable<SentenceTask> GetByCategory(string category);
    }
}
