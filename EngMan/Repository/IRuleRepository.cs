using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IRuleRepository
    {
        IEnumerable<RuleModel> Rules { get; }

        Task<RuleModel> SaveRule(RuleModel rule);

        Task<RuleModel> AddRule(RuleModel rule);

        Task<int> DeleteRule(int id);

        IEnumerable<string> GetAllCategories();

        IEnumerable<RuleModel> GetByCategory(string category);
    }
}
