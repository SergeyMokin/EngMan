using System;
using System.Linq;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    public class GuessesTheImageController : ApiController
    {
        private IGuessesTheImageService service;

        public GuessesTheImageController(IGuessesTheImageService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetTask()
        {
            var rand = new Random();
            var tasks = service.GetGuessesTheImages().ToList();
            if (tasks != null)
            {
                return Ok(tasks.ElementAt(rand.Next(0, tasks.Count())));
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(GuessesTheImage img)
        {
            if (img != null)
            {
                var task = service.GetGuessesTheImage(img.Id);
                if (task != null)
                {
                    if (task.Word.Equals(img.Word))
                    {
                        return Ok(true);
                    }
                }
            }
            return Ok(false);
        }
    }
}
