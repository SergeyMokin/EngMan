using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface ISentenceTaskRepository: IRepository<SentenceTask, SentenceTask>
    {
        IQueryable<SentenceTask> GetTasks(string category, IEnumerable<int> indexes = default(int[]));
    }
}
