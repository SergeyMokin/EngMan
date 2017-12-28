using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    public class RuleController : ApiController
    {
        private IRuleService service;

        public RuleController(IRuleService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRules()
        {
            var rules = await service.Get();
            if (rules != null)
            {
                return Ok(rules);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRule(int id)
        {
            var rule = await service.GetById(id);
            if (rule != null)
            {
                return Ok(rule);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> EditRule(Rule rule)
        {
            var _rule = await service.Edit(rule);
            if (_rule != null)
            {
                return Ok(_rule);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> AddRule(Rule rule)
        {
            var _rule = await service.Add(rule);
            if (_rule != null)
            {
                return Ok(_rule);
            }
            return NotFound();
        }

        [HttpDelete]
        [Authorize]
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
