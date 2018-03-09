using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
using System;

namespace EngMan.Controllers
{
    [Authorize]
    public class WordController : ApiController
    {
        private IWordService service;

        public WordController(IWordService _service)
        {
            service = _service;
        }

        //GET api/word/GetAllCategories
        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var tasks = service.GetAllCategories();
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

        //GET api/word/GetByCategory
        [HttpGet]
        public IHttpActionResult GetByCategory(string category)
        {
            try
            {
                var tasks = service.GetByCategory(category);
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

        //GET api/word/GetAllWords
        [HttpGet]
        public IHttpActionResult GetAllWords()
        {
            try
            {
                var words = service.Get();
                if (words == null)
                {
                    return NotFound();
                }
                return Ok(words);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/word/GetWordById
        [HttpGet]
        public IHttpActionResult GetWordById(int id)
        {
            try
            {
                var word = service.GetById(id);
                if (word == null)
                {
                    return NotFound();
                }
                return Ok(word);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT api/word/EditWord
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditWord(Word word)
        {
            try
            {
                return Ok(await service.Edit(word));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/word/AddWord
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult AddWord(Word word)
        {
            try
            {
                return Ok(service.Add(word));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE api/word/DeleteWord
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteWord(int id)
        {
            try
            {
                var _id = await service.Delete(id);
                if (_id != -1)
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

        //GET api/word/GetWordMap
        [HttpGet]
        public IHttpActionResult GetWordMap(string category, string indexes, bool translate) 
        {
            try
            {
                var task = service.GetTask(category, indexes, translate);
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

        //POST api/word/VerificationCorrectness
        [HttpPost]
        public IHttpActionResult VerificationCorrectness(Word word, bool translate)
        {
            try
            {
                return Ok(service.VerificationCorrectness(word, translate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
