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
        public IHttpActionResult GetWord(string category, int id)
        {
            try
            {
                var tasks = service.GetByCategory(category).ToList();
                var rand = new Random();
                if (tasks != null)
                {
                    var indexes = new HashSet<int>();
                    if (tasks.Count() >= 5)
                    {
                        if (tasks.Count() - 1 <= id)
                        {
                            return NotFound();
                        }
                        indexes.Add(id + 1);
                        while (indexes.Count() != 5)
                        {
                            indexes.Add(rand.Next(0, tasks.Count()));
                        }
                        var word = tasks.ElementAt(id + 1);
                        var translate = new List<string>();
                        foreach (var index in indexes)
                        {
                            translate.Add(tasks.ElementAt(index).Translate);
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
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
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
                var _word = service.GetById(word.WordId);
                if (_word != null)
                {
                    if (_word.Translate.ToLower().Equals(word.Translate.ToLower()))
                    {
                        return Ok(true);
                    }
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
