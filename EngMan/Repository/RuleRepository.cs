using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public class RuleRepository: IRuleRepository
    {
        public IEnumerable<RuleModel> Rules
        {
            get
            {
                return context.Rules.Select(x => x);
            }
        }

        private EFDbContext context;

        public RuleRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<string> GetAllCategories()
        {
            return context.Rules.GroupBy(x => x.Category).Select(x => x.Key);
        }

        public IEnumerable<RuleModel> GetByCategory(string category)
        {
            return context.Rules.Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public async Task<RuleModel> SaveRule(RuleModel rule)
        {
            if (rule.RuleId == 0)
            {
                context.Rules.Add(rule);
                context.SaveChanges();
                rule.RuleId = context.Rules.ToArray().Last().RuleId;
            }
            else
            {
                var entity = await context.Rules.FindAsync(rule.RuleId);
                if(entity != null)
                {
                    entity.Title = rule.Title;
                    entity.Text = rule.Text;
                    entity.Category = rule.Category;
                }
            }
            context.SaveChanges();
            return rule;
        }

        public async Task<RuleModel> AddRule(RuleModel rule)
        {
            return await SaveRule(rule);
        }

        public async Task<int> DeleteRule(int id)
        {
            if (id > 0)
            {
                var entity = await context.Rules.FindAsync(id);
                if (entity != null)
                {
                    context.Rules.Remove(entity);
                }
                context.SaveChanges();
                return id;
            }
            return -1;
        }

    }
}