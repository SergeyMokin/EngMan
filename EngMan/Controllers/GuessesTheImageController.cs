using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
using EngMan.Extensions;
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
            try
            {
                var task = service.Get(id);
                if (task != null)
                {
                    return Ok(task);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(GuessesTheImageToReturn img)
        {
            if (img.Validate())
            {
                try
                {
                    var task = service.Get(img.Id);
                    if (task != null)
                    {
                        if (task.Word.Original.ToLower().Equals(img.Word.Original.ToLower()))
                        {
                            return Ok(true);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok(false);
        }
    }
}
