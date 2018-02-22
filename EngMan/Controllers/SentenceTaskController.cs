using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;

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
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(SentenceTask sentence)
        {
            try
            {
                var task = service.VerificationCorrectness(sentence);
                return Ok(task);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
