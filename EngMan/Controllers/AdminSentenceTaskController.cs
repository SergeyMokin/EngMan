using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;

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
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var tasks = service.GetAllCategories();
                if (tasks != null)
                {
                    return Ok(tasks);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetByCategory(string category)
        {
            try
            {
                var tasks = service.GetByCategory(category);
                if (tasks != null)
                {
                    return Ok(tasks);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            try
            {
                var tasks = service.Get();
                if (tasks != null)
                {
                    return Ok(tasks);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetTaskById(int id)
        {
            try
            {
                var task = service.GetById(id);
                if (task != null)
                {
                    return Ok(task);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditTask(SentenceTask task)
        {
            try
            {
                var _task = await service.Edit(task);
                return Ok(_task);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddTask(SentenceTask task)
        {
            try
            {
                var _task = service.Add(task);
                return Ok(_task);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            try
            {
                var _id = await service.Delete(id);
                if (_id != -1)
                {
                    return Ok(_id);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
    }
}
