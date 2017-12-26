using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
namespace EngMan.Service
{
    public class RuleService: IRuleService
    {
        private IRuleRepository rep;

        public RuleService(IRuleRepository _rep)
        {
            rep = _rep;
        }

        public async Task<IEnumerable<Rule>> Get()
        {
            var task = new Task<IEnumerable<Rule>>(delegate () { return rep.Rules; });
            task.Start();
            return await task;
        }

        public async Task<IEnumerable<Rule>> GetById(int id)
        {
            var task = new Task<IEnumerable<Rule>>(delegate() { return rep.Rules.Where(x => x.Id == id); });
            task.Start();
            return await task;
        }

        public async Task<Rule> Edit(Rule rule)
        {
            return await rep.SaveRule(rule);
        }

        public async Task<Rule> Add(Rule rule)
        {
            return await rep.AddRule(rule);
        }

        public async Task<int> Delete(int id)
        {
            return await rep.DeleteRule(id);
        }
    }
}