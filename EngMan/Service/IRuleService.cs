using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IRuleService
    {
        IEnumerable<RuleModel> Get();

        RuleModel GetById(int id);

        Task<RuleModel> Edit(RuleModel rule);

        Task<RuleModel> Add(RuleModel rule);

        Task<int> Delete(int id);

        List<string> AddImages(Image[] images);

        IEnumerable<string> GetAllCategories();

        IEnumerable<RuleModel> GetByCategory(string category);
    }
}
