using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
namespace EngMan.Service
{
    public class SentenceTaskService: ISentenceTaskService
    {
        ISentenceTaskRepository rep;

        public SentenceTaskService(ISentenceTaskRepository _rep)
        {
            rep = _rep;
        }

        public async Task<IEnumerable<SentenceTask>> Get()
        {
            var task = new Task<IEnumerable<SentenceTask>>(() => rep.SentenceTasks);
            task.Start();
            return await task;
        }

        public async Task<SentenceTask> GetById(int id)
        {
            var task = new Task<SentenceTask>(() => rep.SentenceTasks.FirstOrDefault(x => x.SentenceTaskId == id));
            task.Start();
            return await task;
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