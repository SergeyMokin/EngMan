using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Collections.Generic;

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

        //GET api/rule/GetAllCategories
        [HttpGet]
        public IEnumerable<string> GetAllCategories()
        {
            return service.GetAllCategories();
        }

        //GET api/rule/GetByCategory
        [HttpGet]
        public IEnumerable<RuleModel> GetByCategory(string category)
        {
            return service.GetByCategory(category);
        }

        //GET api/rule/GetAllRules
        [HttpGet]
        public IEnumerable<RuleModel> GetAllRules()
        {
            return service.Get();
        }

        //GET api/rule/GetRule
        [HttpGet]
        public RuleModel GetRule(int id)
        {
            return service.GetById(id);
        }

        //PUT api/rule/EditRule
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<bool> EditRule(RuleModel rule)
        {
            return await service.Edit(rule);
        }

        //POST api/rule/AddRule
        [HttpPost]
        [Authorize(Roles = "admin")]
        public bool AddRule(RuleModel rule)
        {
            return service.Add(rule);
        }

        //POST api/rule/AddImages
        [HttpPost]
        [Authorize(Roles = "admin")]
        public List<string> AddImages(Image[] images)
        {
            return service.AddImages(images);
        }

        //DELETE api/rule/DeleteRule
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<string> DeleteRule(int id)
        {
            return await service.Delete(id);
        }

    }
}
