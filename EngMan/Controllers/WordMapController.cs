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
                    indexes.Add(rand.Next(1, words.Count() + 1));
                }
                var word = words.ElementAt(indexes.Last() - 1);
                var translate = new List<string>();
                foreach (var index in indexes)
                {
                    translate.Add(words.ElementAt(index-1).Translate);
                }
                if (words != null)
                {
                    return Ok(new MapWord {
                        WordId = word.WordId,
                        Original = word.Original,
                        Translate = translate.OrderBy(x => x.OrderBy(y => y).ToString()[x.Count() / 2 - 1]).ToList(),
                        Category = word.Category
                    });
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
                var rand = new Random();
                var indexes = new HashSet<int> { id };
                var words = await service.Get();
                if (words != null)
                {
                    while (indexes.Count() != 5)
                    {
                        indexes.Add(rand.Next(1, words.Count() + 1));
                    }
                    var translate = new List<string>();
                    foreach (var index in indexes)
                    {
                        translate.Add(words.ElementAt(index-1).Translate);
                    }
                    return Ok(new MapWord {
                        WordId = word.WordId,
                        Original = word.Original,
                        Translate = translate.OrderBy(x => x.OrderBy(y => y).ToString()[x.Count() / 2 - 1]).ToList(),
                        Category = word.Category
                    });
                }
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
