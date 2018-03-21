using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Linq;

namespace EngMan.Controllers
{
    [Authorize]
    public class GuessesTheImageController : ApiController
    {
        private IGuessesTheImageService service;

        public GuessesTheImageController(IGuessesTheImageService _service)
        {
            service = _service;
        }

        //GET api/GuessesTheImage/GetAllCategories
        [HttpGet]
        public IQueryable<string> GetAllCategories()
        {
            return service.GetAllCategories();
        }

        //GET api/GuessesTheImage/GetByCategory
        [HttpGet]
        public IQueryable<GuessesTheImageToReturn> GetByCategory(string category)
        {
            return service.GetByCategory(category);
        }

        //GET api/GuessesTheImage/GetAllTasks
        [HttpGet]
        public IQueryable<GuessesTheImageToReturn> GetAllTasks()
        {
            return service.GetAll();
        }

        //GET api/GuessesTheImage/GetTaskById
        [HttpGet]
        public GuessesTheImageToReturn GetTaskById(int id)
        {
            return service.Get(id);
        }

        //PUT api/GuessesTheImage/EditTask
        [Authorize(Roles = "admin")]
        [HttpPut]
        public bool EditTask(GuessesTheImageToAdd task)
        {
            return service.Edit(task);
        }

        //POST api/GuessesTheImage/AddTask
        [Authorize(Roles = "admin")]
        [HttpPost]
        public bool AddTask(GuessesTheImageToAdd task)
        {
            return service.Add(task);
        }

        //DELETE api/GuessesTheImage/DeleteTask
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public string DeleteTask(int id)
        {
            return service.Delete(id);
        }

        //GET api/GuessesTheImage/GetTask
        [HttpGet]
        public GuessesTheImageToReturn GetTask(string category, string indexes)
        {
            return service.GetTask(category, indexes);
        }

        //POST api/GuessesTheImage/VerificationCorrectness
        [HttpPost]
        public bool VerificationCorrectness(GuessesTheImageToReturn img)
        {
            return service.VerificationCorrectness(img);
        }
    }
}
