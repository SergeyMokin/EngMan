using System;
using System.Linq;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Collections.Generic;
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
        public IHttpActionResult GetAllCategories()
        {
            var words = service.Get();
            if (words != null)
            {
                var categories = words.GroupBy(x => x.Category).Where(x => x.Count() > 4).Select(x => x.Key).ToList();
                return Ok(categories);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetWord(string category, int id)
        {
            if (category != null)
            {
                var rand = new Random();
                var words = service.Get();
                words = words.Where(x => x.Category == category);
                if (words != null)
                {
                    var indexes = new HashSet<int>();
                    if (words.Count() >= 5)
                    {
                        if (words.Count() - 1 <= id)
                        {
                            return NotFound();
                        }
                        indexes.Add(id + 1);
                        while (indexes.Count() != 5)
                        {
                            indexes.Add(rand.Next(0, words.Count()));
                        }
                        var word = words.ElementAt(id + 1);
                        var translate = new List<string>();
                        foreach (var index in indexes)
                        {
                            translate.Add(words.ElementAt(index).Translate);
                        }
                        if (word != null)
                        {
                            return Ok(new MapWord
                            {
                                WordId = word.WordId,
                                Original = word.Original,
                                Translate = translate.OrderBy(x => x.OrderBy(y => y).ToString()[x.Count() / 2 - 1]).ToList(),
                                Category = word.Category
                            });
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(Word word)
        {
            if (word != null)
            {
                var _word = service.GetById(word.WordId);
                if (_word != null)
                {
                    if (_word.Translate.ToLower().Equals(word.Translate.ToLower()))
                    {
                        return Ok(true);
                    }
                }
            }
            return Ok(false);
        }
    }
}
