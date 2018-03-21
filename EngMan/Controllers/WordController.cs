using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Linq;

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
        public IQueryable<string> GetAllCategories()
        {
            return service.GetAllCategories();
        }

        //GET api/word/GetByCategory
        [HttpGet]
        public IQueryable<Word> GetByCategory(string category)
        {
            return service.GetByCategory(category);
        }

        //GET api/word/GetAllWords
        [HttpGet]
        public IQueryable<Word> GetAllWords()
        {
            return service.GetAll();
        }

        //GET api/word/GetWordById
        [HttpGet]
        public Word GetWordById(int id)
        {
            return service.Get(id);
        }

        //PUT api/word/EditWord
        [Authorize(Roles = "admin")]
        [HttpPut]
        public bool EditWord(Word word)
        {
            return service.Edit(word);
        }

        //POST api/word/AddWord
        [Authorize(Roles = "admin")]
        [HttpPost]
        public bool AddWord(Word word)
        {
            return service.Add(word);
        }

        //DELETE api/word/DeleteWord
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public string DeleteWord(int id)
        {
            return service.Delete(id);
        }

        //GET api/word/GetWordMap
        [HttpGet]
        public MapWord GetWordMap(string category, string indexes, bool translate) 
        {
            return service.GetTask(category, indexes, translate);
        }

        //POST api/word/VerificationCorrectness
        [HttpPost]
        public bool VerificationCorrectness(Word word, bool translate)
        {
            return service.VerificationCorrectness(word, translate);
        }
    }
}
