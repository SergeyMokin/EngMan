using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface ISentenceTaskService
    {
        IEnumerable<SentenceTask> Get();

        SentenceTask GetById(int id);

        Task<SentenceTask> Edit(SentenceTask task);

        Task<SentenceTask> Add(SentenceTask task);

        Task<int> Delete(int id);

        IEnumerable<string> GetAllCategories();

        IEnumerable<SentenceTask> GetByCategory(string category);

        SentenceTask GetTask(string category, string indexes);

        bool VerificationCorrectness(SentenceTask task);
    }
}
