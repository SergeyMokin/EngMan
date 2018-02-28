using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IRuleService
    {
        //get all rules
        IEnumerable<RuleModel> Get();

        //get rule by id
        RuleModel GetById(int id);

        //edit rule in db
        Task<bool> Edit(RuleModel rule);

        //add rule to db
        bool Add(RuleModel rule);

        //delete rule from db
        Task<int> Delete(int id);

        //add images to server
        List<string> AddImages(Image[] images);

        //get all categories of rules
        IEnumerable<string> GetAllCategories();

        //get rules by category
        IEnumerable<RuleModel> GetByCategory(string category);
    }
}
