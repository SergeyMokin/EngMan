using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IRuleService
    {
        IEnumerable<RuleModel> Get();

        RuleModel GetById(int id);

        Task<bool> Edit(RuleModel rule);

        bool Add(RuleModel rule);

        Task<int> Delete(int id);

        List<string> AddImages(Image[] images);

        IEnumerable<string> GetAllCategories();

        IEnumerable<RuleModel> GetByCategory(string category);
    }
}
