using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;

namespace EngMan.Service
{
    public interface IService
    {
        Task<IEnumerable<RuleWithImages>> Get();

        Task<IEnumerable<RuleWithImages>> GetById(int id);

        Task<RuleWithImages> Edit(RuleWithImages rule);

        Task<RuleWithImages> Add(RuleWithImages rule);

        Task<int> Delete(int id);
    }
}
