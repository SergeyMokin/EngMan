using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Collections.Generic;
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

        //GET api/rule/GetAllCategories
        [HttpGet]
        public IQueryable<string> GetAllCategories()
        {
            return service.GetAllCategories();
        }

        //GET api/rule/GetByCategory
        [HttpGet]
        public IQueryable<RuleModel> GetByCategory(string category)
        {
            return service.GetByCategory(category);
        }

        //GET api/rule/GetAllRules
        [HttpGet]
        public IQueryable<RuleModel> GetAllRules()
        {
            return service.GetAll();
        }

        //GET api/rule/GetRule
        [HttpGet]
        public RuleModel GetRule(int id)
        {
            return service.Get(id);
        }

        //PUT api/rule/EditRule
        [HttpPut]
        [Authorize(Roles = "admin")]
        public bool EditRule(RuleModel rule)
        {
            return service.Edit(rule);
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
        public string DeleteRule(int id)
        {
            return service.Delete(id);
        }

    }
}
