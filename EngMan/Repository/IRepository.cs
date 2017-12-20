using System.Collections.Generic;
using System.Threading.Tasks;

namespace EngMan.Repository
{
    public interface IRepository
    {
        IEnumerable<RuleWithImages> Rules { get; }

        Task<RuleWithImages> SaveRule(RuleWithImages rule);

        Task<RuleWithImages> AddRule(RuleWithImages rule);

        Task<int> DeleteRule(int id);
    }
}
