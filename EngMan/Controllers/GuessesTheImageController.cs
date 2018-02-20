using System.Linq;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
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

        [HttpGet]
        public IHttpActionResult GetTask(int id)
        {
            var tasks = service.GetGuessesTheImages().ToList();
            if (tasks != null)
            {
                if (tasks.Count() > 0 && tasks.Count() - 1 >= id)
                {
                    return Ok(tasks.ElementAt(id));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(GuessesTheImageToReturn img)
        {
            if (img != null)
            {
                var task = service.GetGuessesTheImage(img.Id);
                if (task != null)
                {
                    if (task.Word.Original.ToLower().Equals(img.Word.Original.ToLower()))
                    {
                        return Ok(true);
                    }
                }
            }
            return Ok(false);
        }
    }
}
