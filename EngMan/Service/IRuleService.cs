using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;

namespace EngMan.Service
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> Get();

        Task<IEnumerable<Rule>> GetById(int id);

        Task<Rule> Edit(Rule rule);

        Task<Rule> Add(Rule rule);

        Task<int> Delete(int id);
    }
}
