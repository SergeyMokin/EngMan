using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public class RuleRepository: IRuleRepository
    {
        private EFDbContext context;

        public RuleRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IQueryable<RuleModel> GetAll()
        {
            return context.Rules.Select(x => x);
        }

        public RuleModel Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.RuleId == id);
        }

        public bool Add(RuleModel rule)
        {
            if (rule == null)
            {
                throw new System.ArgumentNullException();
            }
            if (rule.RuleId > 0)
            {
                return false;
            }
            context.Rules.Add(rule);
            context.SaveChanges();
            return true;
        }

        public bool Edit(RuleModel rule)
        {
            if (rule == null)
            {
                throw new System.ArgumentNullException();
            }
            if (rule.RuleId < 1)
            {
                return false;
            }
            var entity = context.Rules.Find(rule.RuleId);
            if (entity == null)
            {
                return false;
            }
            entity.Title = rule.Title;
            entity.Text = rule.Text;
            entity.Category = rule.Category;
            context.SaveChanges();
            return true;
        }

        public int Delete(int id)
        {
            if (id < 1)
            {
                return -1;
            }
            var entity = context.Rules.Find(id);
            if (entity == null)
            {
                return -1;
            }
            context.Rules.Remove(entity);
            context.SaveChanges();
            return id;
        }

        public IQueryable<string> GetAllCategories()
        {
            return GetAll().GroupBy(x => x.Category).Select(x => x.Key);
        }

        public IQueryable<RuleModel> GetByCategory(string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            return GetAll().Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

    }
}