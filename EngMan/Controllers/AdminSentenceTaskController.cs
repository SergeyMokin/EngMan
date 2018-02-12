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
        public IHttpActionResult GetAllTasks()
        {
            var tasks = service.Get();
            if (tasks != null)
            {
                return Ok(tasks);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetTaskById(int id)
        {
            if (id > 0)
            {
                var task = service.GetById(id);
                if (task != null)
                {
                    return Ok(task);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditTask(SentenceTask task)
        {
            if (task != null)
            {
                var _task = await service.Edit(task);
                if (_task != null)
                {
                    return Ok(_task);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IHttpActionResult> AddTask(SentenceTask task)
        {
            if (task != null)
            {
                var _task = await service.Add(task);
                if (_task != null)
                {
                    return Ok(_task);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            if (id > 0)
            {
                var _id = await service.Delete(id);
                if (_id != -1)
                {
                    return Ok(_id);
                }
            }
            return NotFound();
        }
    }
}
