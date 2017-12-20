using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;

namespace EngMan.Controllers
{
    public class RuleController : ApiController
    {
        private IRuleService Service;

        public RuleController(IRuleService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRules()
        {
            var rules = await Service.Get();
            if (rules != null)
            {
                return Ok(rules);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRule(int id)
        {
            var rules = await Service.GetById(id);
            if (rules != null)
            {
                return Ok(rules);
            }
            return NotFound();
        }

    }
}
