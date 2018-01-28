using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public class RuleRepository: IRuleRepository
    {
        public IEnumerable<RuleModel> Rules {
            get
            {
                return context.Rules;
            }
        }

        private EFDbContext context;

        public RuleRepository(EFDbContext _context)
        {
            context = _context;
        }

        public async Task<RuleModel> SaveRule(RuleModel rule)
        {
            if (rule.RuleId == 0)
            {
                context.Rules.Add(rule);
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