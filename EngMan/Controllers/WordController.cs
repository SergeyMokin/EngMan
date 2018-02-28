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

        //GET api/word/GetByCategory
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

        //GET api/word/GetAllWords
        [HttpGet]
        public IHttpActionResult GetAllWords()
        {
            try
            {
                var words = service.Get();
                if (words != null)
                {
                    return Ok(words);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/word/GetWordById
        [HttpGet]
        public IHttpActionResult GetWordById(int id)
        {
            try
            {
                var word = service.GetById(id);
                if (word != null)
                {
                    return Ok(word);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //PUT api/word/EditWord
        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditWord(Word word)
        {
            try
            {
                var _word = await service.Edit(word);
                return Ok(_word);
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
                var _word = service.Add(word);
                return Ok(_word);
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
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/word/GetWordMap
        [HttpGet]
        public IHttpActionResult GetWordMap(string category, string indexes)
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

        //POST api/word/VerificationCorrectness
        [HttpPost]
        public IHttpActionResult VerificationCorrectness(Word word)
        {
            try
            {
                var task = service.VerificationCorrectness(word);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
