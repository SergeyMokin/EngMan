using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;

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
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

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
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditWord(Word word)
        {
            try
            {
                var _word = await service.Edit(word);
                if (_word != null)
                {
                    return Ok(_word);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IHttpActionResult> AddWord(Word word)
        {
            try
            {
                var _word = await service.Add(word);
                if (_word != null)
                {
                    return Ok(_word);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteWord(int id)
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
