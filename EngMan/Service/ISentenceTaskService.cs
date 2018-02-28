using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface ISentenceTaskService
    {
        //get all sentences
        IEnumerable<SentenceTask> Get();

        //get sentence by id
        SentenceTask GetById(int id);

        //edit sentence in db
        Task<bool> Edit(SentenceTask task);

        //add sentence to db
        bool Add(SentenceTask task);

        //delete sentence from db
        Task<int> Delete(int id);

        //get all categories of sentences
        IEnumerable<string> GetAllCategories();

        //get sentences by category
        IEnumerable<SentenceTask> GetByCategory(string category);

        //get sentence task
        SentenceTask GetTask(string category, string indexes);

        //verification correctness of the sentence task
        bool VerificationCorrectness(SentenceTask task);
    }
}
