using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
namespace EngMan.Service
{
    public class RuleService: IRuleService
    {
        private readonly IRuleRepository rep;

        public RuleService(IRuleRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<RuleModel> Get()
        {
            return rep.Rules;
        }

        public RuleModel GetById(int id)
        {
            return rep.Rules.FirstOrDefault(x => x.RuleId == id);
        }

        public async Task<RuleModel> Edit(RuleModel rule)
        {
            return await rep.SaveRule(rule);
        }

        public async Task<RuleModel> Add(RuleModel rule)
        {
            return await rep.AddRule(rule);
        }

        public async Task<int> Delete(int id)
        {
            return await rep.DeleteRule(id);
        }

        public List<string> AddImages(Image[] images) {
            var pathes = new List<string>();
            if (images != null && images.Length > 0)
            {
                foreach (var img in images)
                {
                    pathes.Add(Extensions.Extensions.SaveImage(img));
                }
            }
            return pathes;
        }
    }
}