using System;
using System.Linq;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace EngMan.Controllers
{
    [Authorize]
    public class WordMapController : ApiController
    {
        private IWordService service;

        public WordMapController(IWordService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetWord(string category, string indexes)
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
        public IHttpActionResult VerificationCorrectness(Word word)
        {
            try
            {
                var task = service.VerificationCorrectness(word);
                return Ok(task);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
