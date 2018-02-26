using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
namespace EngMan.Controllers
{
    [Authorize]
    public class AdminGuessesTheImageController : ApiController
    {
        private IGuessesTheImageService service;

        public AdminGuessesTheImageController(IGuessesTheImageService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var categories = service.GetAllCategories();
                if (categories != null)
                {
                    return Ok(categories);
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
                var task = service.GetByCategory(category);
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

        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            try
            {
                var tasks = service.GetAll();
                if (tasks != null)
                {
                    return Ok(tasks);
                }
            }
            catch(HttpRequestException ex)
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
                var task = service.Get(id);
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
        public IHttpActionResult EditTask(GuessesTheImageToAdd task)
        {
            try
            {
                var _task = service.Edit(task);
                return Ok(_task);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddTask(GuessesTheImageToAdd task)
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
        public IHttpActionResult DeleteTask(int id)
        {
            try
            {
                var _task = service.Delete(id);
                if (_task > 0)
                {
                    return Ok("Delete completed successful");
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
