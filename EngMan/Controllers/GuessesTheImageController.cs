using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
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
        public IHttpActionResult GetTask(string category, string indexes)
        {
            try
            {
                var task = service.GetTask(category, indexes);
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
            try
            {
                var task = service.VerificationCorrectness(img);
                return Ok(task);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
