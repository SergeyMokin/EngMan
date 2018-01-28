using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Web;
using System.Linq;

namespace EngMan.Controllers
{
    [Authorize]
    public class RuleController : ApiController
    {
        private IRuleService service;

        public RuleController(IRuleService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetAllRules()
        {
            var rules = service.Get();
            if (rules != null)
            {
                return Ok(rules);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetRule(int id)
        {
            var rule = service.GetById(id);
            if (rule != null)
            {
                return Ok(rule);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> EditRule(RuleModel rule)
        {
            var _rule = await service.Edit(rule);
            if (_rule != null)
            {
                return Ok(_rule);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> AddRule(RuleModel rule)
        {
            var _rule = await service.Add(rule);
            if (_rule != null)
            {
                return Ok(_rule);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IHttpActionResult AddImages(Image[] images)
        {
            var pathes = service.AddImages(images);
            if (pathes != null)
            {
                return Ok(pathes);
            }
            return NotFound();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> DeleteRule(int id)
        {
            var _id = await service.Delete(id);
            if (_id != -1)
            {
                return Ok(_id);
            }
            return NotFound();
        }

    }
}
