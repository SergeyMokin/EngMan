using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System;

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/SentenceTask/GetByCategory
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/SentenceTask/GetAllTasks
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/SentenceTask/GetTaskById
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //PUT api/SentenceTask/EditTask
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditTask(SentenceTask task)
        {
            try
            {
                var _task = await service.Edit(task);
                return Ok(_task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/SentenceTask/AddTask
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddTask(SentenceTask task)
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

        //DELETE api/SentenceTask/DeleteTask
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            try
            {
                var _id = await service.Delete(id);
                if (_id != -1)
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

        //GET api/SentenceTask/GetTask
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

        //POST api/SentenceTask/VerificationCorrectness
        [HttpPost]
        public IHttpActionResult VerificationCorrectness(SentenceTask sentence)
        {
            try
            {
                var task = service.VerificationCorrectness(sentence);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
