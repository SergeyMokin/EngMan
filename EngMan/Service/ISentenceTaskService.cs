using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface ISentenceTaskService
    {
        Task<IEnumerable<SentenceTask>> Get();

        Task<SentenceTask> GetById(int id);

        Task<SentenceTask> Edit(SentenceTask task);

        Task<SentenceTask> Add(SentenceTask task);

        Task<int> Delete(int id);
    }
}
