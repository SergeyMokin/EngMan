using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System.Net.Http;

namespace EngMan.Service
{
    public class SentenceTaskService: ISentenceTaskService
    {
        private readonly ISentenceTaskRepository rep;

        public SentenceTaskService(ISentenceTaskRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<string> GetAllCategories()
        {
            try
            {
                return rep.GetAllCategories();
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public IEnumerable<SentenceTask> GetByCategory(string category)
        {
            if (category.Validate())
            {
                try
                {
                    return rep.GetByCategory(category);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<SentenceTask> Get()
        {
            try
            {
                return rep.SentenceTasks;
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public SentenceTask GetById(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.SentenceTasks.FirstOrDefault(x => x.SentenceTaskId == id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<SentenceTask> Edit(SentenceTask task)
        {
            if (task.Validate())
            {
                try
                {
                    return await rep.SaveTask(task);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<SentenceTask> Add(SentenceTask task)
        {
            if (task.Validate())
            {
                try
                {
                    return await rep.AddTask(task);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<int> Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return await rep.DeleteTask(id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }
    }
}