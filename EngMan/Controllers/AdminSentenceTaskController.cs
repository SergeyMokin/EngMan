using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    [Authorize]
    public class AdminSentenceTaskController : ApiController
    {
        private ISentenceTaskService service;

        public AdminSentenceTaskController(ISentenceTaskService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllTasks()
        {
            var tasks = await service.Get();
            if (tasks != null)
            {
                return Ok(tasks);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTaskById(int id)
        {
            var task = await service.GetById(id);
            if (task != null)
            {
                return Ok(task);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditTask(SentenceTask task)
        {
            var _task = await service.Edit(task);
            if (_task != null)
            {
                return Ok(_task);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddTask(SentenceTask task)
        {
            var _task = await service.Add(task);
            if (_task != null)
            {
                return Ok(_task);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            var _id = await service.Delete(id);
            if (_id != -1)
            {
                return Ok(_id);
            }
            return NotFound();
        }
    }
}
