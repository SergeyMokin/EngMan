using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IRuleRepository
    {
        IEnumerable<Rule> Rules { get; }

        Task<Rule> SaveRule(Rule rule);

        Task<Rule> AddRule(Rule rule);

        Task<int> DeleteRule(int id);
    }
}
