using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
namespace EngMan.Service
{
    public class SentenceTaskService: ISentenceTaskService
    {
        private readonly ISentenceTaskRepository rep;

        public SentenceTaskService(ISentenceTaskRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<SentenceTask> Get()
        {
            return rep.SentenceTasks;
        }

        public SentenceTask GetById(int id)
        {
            return rep.SentenceTasks.FirstOrDefault(x => x.SentenceTaskId == id);
        }

        public async Task<SentenceTask> Edit(SentenceTask task)
        {
            return await rep.SaveTask(task);
        }

        public async Task<SentenceTask> Add(SentenceTask task)
        {
            return await rep.AddTask(task);
        }

        public async Task<int> Delete(int id)
        {
            return await rep.DeleteTask(id);
        }
    }
}