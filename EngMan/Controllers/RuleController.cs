using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;
using System;

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
        public IHttpActionResult GetAllCategories()
        {
            try
            {
                var result = service.GetAllCategories();
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/rule/GetByCategory
        [HttpGet]
        public IHttpActionResult GetByCategory(string category)
        {
            try
            {
                var rules = service.GetByCategory(category);
                if (rules != null)
                {
                    return Ok(rules);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/rule/GetAllRules
        [HttpGet]
        public IHttpActionResult GetAllRules()
        {
            try
            {
                var rules = service.Get();
                if (rules != null)
                {
                    return Ok(rules);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //GET api/rule/GetRule
        [HttpGet]
        public IHttpActionResult GetRule(int id)
        {
            try
            {
                var rule = service.GetById(id);
                if (rule != null)
                {
                    return Ok(rule);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //PUT api/rule/EditRule
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> EditRule(RuleModel rule)
        {
            try
            {
                var _rule = await service.Edit(rule);
                return Ok(_rule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/rule/AddRule
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IHttpActionResult AddRule(RuleModel rule)
        {
            try
            {
                var _rule = service.Add(rule);
                return Ok(_rule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/rule/AddImages
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IHttpActionResult AddImages(Image[] images)
        {
            try
            {
                var pathes = service.AddImages(images);
                if (pathes != null)
                {
                    return Ok(pathes);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        //DELETE api/rule/DeleteRule
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> DeleteRule(int id)
        {
            try
            {
                var _id = await service.Delete(id);
                if (_id != -1)
                {
                    return Ok("Delete completed successful");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

    }
}
