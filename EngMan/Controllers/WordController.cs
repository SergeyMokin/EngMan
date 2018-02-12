using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
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
        public IHttpActionResult GetAllWords()
        {
            var words = service.Get();
            if (words != null)
            {
                return Ok(words);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetWordById(int id)
        {
            if (id > 0)
            {
                var word = service.GetById(id);
                if (word != null)
                {
                    return Ok(word);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IHttpActionResult> EditWord(Word word)
        {
            if (word != null)
            {
                var _word = await service.Edit(word);
                if (_word != null)
                {
                    return Ok(_word);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IHttpActionResult> AddWord(Word word)
        {
            if (word != null)
            {
                var _word = await service.Add(word);
                if (_word != null)
                {
                    return Ok(_word);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteWord(int id)
        {
            if (id > 0)
            {
                var _id = await service.Delete(id);
                if (_id != -1)
                {
                    return Ok(_id);
                }
            }
            return NotFound();
        }
    }
}
