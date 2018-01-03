using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Collections.Generic;
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
                var indexes = new HashSet<int>();
                while (indexes.Count() != 5)
                {
                    indexes.Add(rand.Next(0, words.Count()));
                }
                var returnedWord = words.ElementAt(indexes.First());
                var translate = new List<string>();
                foreach (var index in indexes.OrderBy(x => x))
                {
                    translate.Add(words.ElementAt(index).Translate);
                }
                if (words != null)
                {
                    return Ok(new MapWord { WordId = returnedWord.WordId, Original = returnedWord.Original, Translate = translate, Category = returnedWord.Category });
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
                return Ok(word);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> VerificationCorrectness(Word word)
        {
            var _word = await service.GetById(word.WordId);
            if (_word != null)
            {
                if (_word.Translate.Equals(word.Translate))
                {
                    return Ok<bool>(true);
                }
            }
            return Ok<bool>(false);
        }
    }
}
