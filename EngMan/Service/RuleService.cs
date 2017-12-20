using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;

namespace EngMan.Service
{
    public class RuleService: IService
    {
        private IRepository rep;

        public RuleService(IRepository _rep)
        {
            rep = _rep;
        }

        public async Task<IEnumerable<RuleWithImages>> Get()
        {
            var task = new Task<IEnumerable<RuleWithImages>>(delegate () { return rep.Rules; });
            task.Start();
            return await task;
        }

        public async Task<IEnumerable<RuleWithImages>> GetById(int id)
        {
            var task = new Task<IEnumerable<RuleWithImages>>(delegate() { return rep.Rules.Where(x => x.Id == id); });
            task.Start();
            return await task;
        }

        public async Task<RuleWithImages> Edit(RuleWithImages rule)
        {
            return await rep.SaveRule(rule);
        }

        public async Task<RuleWithImages> Add(RuleWithImages rule)
        {
            return await rep.AddRule(rule);
        }

        public async Task<int> Delete(int id)
        {
            return await rep.DeleteRule(id);
        }
    }
}