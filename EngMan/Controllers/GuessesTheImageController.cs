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
                if (categories != null)
                {
                    return Ok(categories);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/GuessesTheImage/GetByCategory
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/GuessesTheImage/GetAllTasks
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/GuessesTheImage/GetTaskById
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //PUT api/GuessesTheImage/EditTask
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult EditTask(GuessesTheImageToAdd task)
        {
            try
            {
                var _task = service.Edit(task);
                return Ok(_task);
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
                var _task = service.Add(task);
                return Ok(_task);
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
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/GuessesTheImage/GetTask
        [HttpGet]
        public IHttpActionResult GetTask(string category, string indexes)
        {
            try
            {
                var task = service.GetTask(category, indexes);
                if (task != null)
                {
                    return Ok(task);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //POST api/GuessesTheImage/VerificationCorrectness
        [HttpPost]
        public IHttpActionResult VerificationCorrectness(GuessesTheImageToReturn img)
        {
            try
            {
                var task = service.VerificationCorrectness(img);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
