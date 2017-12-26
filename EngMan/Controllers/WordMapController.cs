using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    public class WordMapController : ApiController
    {
        private IWordService service;

        public WordMapController(IWordService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRandomWord()
        {
            var rand = new Random();
            var words = await service.Get();
            if (words != null)
            {
                var index = rand.Next(0, words.Count());
                if (words != null)
                {
                    return Ok(words.ElementAt(index));
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetWordById(int id)
        {
            var word = await service.GetById(id);
            if (word != null)
            {
                return Ok(word.Last());
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> VerificationCorrectness(Word word)
        {
            var _word = await service.GetById(word.WordId);
            if (_word != null)
            {
                if (_word.Last().Translate.Equals(word.Translate))
                {
                    return Ok<bool>(true);
                }
            }
            return Ok<bool>(false);
        }
    }
}
