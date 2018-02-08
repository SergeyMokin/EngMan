using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    public class AdminGuessesTheImageController : ApiController
    {
        private IGuessesTheImageService service;

        public AdminGuessesTheImageController(IGuessesTheImageService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            var tasks = service.GetGuessesTheImages();
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
                var task = service.GetGuessesTheImage(id);
                if (task != null)
                {
                    return Ok(task);
                }
            }
            return NotFound();
        }

        [HttpPut]
        public IHttpActionResult EditTask(GuessesTheImage task)
        {
            if (task != null)
            {
                var _task = service.EditGuessesTheImage(task);
                if (_task != null)
                {
                    return Ok(_task);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult AddTask(GuessesTheImageToAdd task)
        {
            if (task != null)
            {
                var _task = service.AddGuessesTheImage(task);
                if (_task != null)
                {
                    return Ok(_task);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        public IHttpActionResult DeleteTask(int id)
        {
            if (id > 0)
            {
                var _id = service.DeleteGuessesTheImage(id);
                if (_id != -1)
                {
                    return Ok(_id);
                }
            }
            return NotFound();
        }
    }
}
