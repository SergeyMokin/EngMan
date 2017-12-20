using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Repository;

namespace EngMan.Controllers
{
    [Authorize]
    public class AdminController : ApiController
    {
        private IRuleService Service;

        public AdminController(IRuleService service)
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

        [HttpPut]
        public async Task<IHttpActionResult> EditRule(Rule rule)
        {
            var _rule = await Service.Edit(rule);
            if (_rule != null)
            {
                return Ok(_rule);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddRule(Rule rule)
        {
            var _rule = await Service.Add(rule);
            if (_rule != null)
            {
                return Ok(_rule);
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRule(int id)
        {
            var _id = await Service.Delete(id);
            if (_id != default(int))
            {
                return Ok(_id);
            }
            return NotFound();
        }
    }
}
