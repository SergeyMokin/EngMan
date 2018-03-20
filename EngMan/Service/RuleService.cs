using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System;
namespace EngMan.Service
{
    public class RuleService: IRuleService
    {
        private readonly IRuleRepository rep;

        public RuleService(IRuleRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<string> GetAllCategories()
        {
            return rep.GetAllCategories();
        }

        public IEnumerable<RuleModel> GetByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }
            return rep.GetByCategory(category);
        }

        public IEnumerable<RuleModel> Get()
        {
            return rep.Rules;
        }

        public RuleModel GetById(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Rules.FirstOrDefault(x => x.RuleId == id);
        }

        public async Task<bool> Edit(RuleModel rule)
        {
            if (!rule.Validate())
            {
                throw new Exception("Invalid model");
            }
            return await rep.SaveRule(rule);
        }

        public bool Add(RuleModel rule)
        {
            if (!rule.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.AddRule(rule);
        }

        public async Task<string> Delete(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return await rep.DeleteRule(id) > 0
                    ? "Delete completed successful"
                    : null;
        }

        public List<string> AddImages(Image[] images)
        {
            if (images == null || images.Length == 0)
            {
                throw new Exception("Invalid model");
            }
            var pathes = new List<string>();
            foreach (var img in images)
            {
                if (img.Validate())
                {
                    pathes.Add(Extensions.Extensions.SaveImage(img));
                }
            }
            return pathes;
        }
    }
}