using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EngMan.Controllers
{
    [Authorize]
    public class SentenceTaskController : ApiController
    {
        private ISentenceTaskService service;

        public SentenceTaskController(ISentenceTaskService _service)
        {
            service = _service;
        }

        //GET api/SentenceTask/GetAllCategories
        [HttpGet]
        public IEnumerable<string> GetAllCategories()
        {
            return service.GetAllCategories();
        }

        //GET api/SentenceTask/GetByCategory
        [HttpGet]
        public IEnumerable<SentenceTask> GetByCategory(string category)
        {
            return service.GetByCategory(category);
        }

        //GET api/SentenceTask/GetAllTasks
        [HttpGet]
        public IEnumerable<SentenceTask> GetAllTasks()
        {
            return service.Get();
        }

        //GET api/SentenceTask/GetTaskById
        [HttpGet]
        public SentenceTask GetTaskById(int id)
        {
            return service.GetById(id);
        }

        //PUT api/SentenceTask/EditTask
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<bool> EditTask(SentenceTask task)
        {
            return await service.Edit(task);
        }

        //POST api/SentenceTask/AddTask
        [Authorize(Roles = "admin")]
        [HttpPost]
        public bool AddTask(SentenceTask task)
        {
            return service.Add(task);
        }

        //DELETE api/SentenceTask/DeleteTask
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<string> DeleteTask(int id)
        {
            return await service.Delete(id);
        }

        //GET api/SentenceTask/GetTask
        [HttpGet]
        public SentenceTask GetTask(string category, string indexes)
        {
            return service.GetTask(category, indexes);
        }

        //POST api/SentenceTask/VerificationCorrectness
        [HttpPost]
        public bool VerificationCorrectness(SentenceTask sentence)
        {
            return service.VerificationCorrectness(sentence);
        }
    }
}
