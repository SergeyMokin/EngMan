using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
using System;

namespace EngMan.Controllers
{
    [Authorize]
    public class GuessesTheImageController : ApiController
    {
        private IGuessesTheImageService service;

        public GuessesTheImageController(IGuessesTheImageService _service)
        {
            service = _service;
        }

        //GET api/GuessesTheImage/GetAllCategories
        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var categories = service.GetAllCategories();
                if (categories == null)
                {
                    return NotFound();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/GuessesTheImage/GetByCategory
        [HttpGet]
        public IHttpActionResult GetByCategory(string category)
        {
            try
            {
                var task = service.GetByCategory(category);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/GuessesTheImage/GetAllTasks
        [HttpGet]
        public IHttpActionResult GetAllTasks()
        {
            try
            {
                var tasks = service.GetAll();
                if (tasks == null)
                {
                    return NotFound();
                }
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/GuessesTheImage/GetTaskById
        [HttpGet]
        public IHttpActionResult GetTaskById(int id)
        {
            try
            {
                var task = service.Get(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT api/GuessesTheImage/EditTask
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult EditTask(GuessesTheImageToAdd task)
        {
            try
            {
                return Ok(service.Edit(task));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/GuessesTheImage/AddTask
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddTask(GuessesTheImageToAdd task)
        {
            try
            {
                return Ok(service.Add(task));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE api/GuessesTheImage/DeleteTask
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
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/GuessesTheImage/GetTask
        [HttpGet]
        public IHttpActionResult GetTask(string category, string indexes)
        {
            try
            {
                var task = service.GetTask(category, indexes);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/GuessesTheImage/VerificationCorrectness
        [HttpPost]
        public IHttpActionResult VerificationCorrectness(GuessesTheImageToReturn img)
        {
            try
            {
                return Ok(service.VerificationCorrectness(img));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
