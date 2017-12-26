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
        public async Task<IHttpActionResult> GetAllWords()
        {
            var words = await service.Get();
            if (words != null)
            {
                return Ok(words);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetWordById(int id)
        {
            var words = await service.GetById(id);
            if (words != null)
            {
                return Ok(words);
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> EditWord(Word word)
        {
            var _word = await service.Edit(word);
            if (_word != null)
            {
                return Ok(_word);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddWord(Word word)
        {
            var _word = await service.Add(word);
            if (_word != null)
            {
                return Ok(_word);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteWord(int id)
        {
            var _id = await service.Delete(id);
            if (_id != -1)
            {
                return Ok(_id);
            }
            return NotFound();
        }
    }
}
