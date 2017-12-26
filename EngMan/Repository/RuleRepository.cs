using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public class RuleRepository: IRuleRepository
    {

        public IEnumerable<Rule> Rules {
            get
            {
                var rules = context.Rules.ToArray();
                var images = context.RulesImages.ToArray();
                var rulesWithImages = rules.Select(x => new Rule() { Id = x.RuleId, RuleModel = x, RulesImages = images.Where(y => y.RuleId == x.RuleId)});
                return rulesWithImages;
            }
        }

        private EFDbContext context;

        public RuleRepository(EFDbContext _context)
        {
            context = _context;
        }

        public async Task<Rule> SaveRule(Rule rule)
        {
            if (rule.RuleModel.RuleId == 0)
            {
                context.Rules.Add(rule.RuleModel);
                rule.RuleModel.RuleId = context.Rules.Last().RuleId;
                foreach (var image in rule.RulesImages)
                {
                    image.RuleId = rule.RuleModel.RuleId;
                    context.RulesImages.Add(image);
                    image.ImageId = context.RulesImages.Last().ImageId;
                }
            }
            else
            {
                var entity = await context.Rules.FindAsync(rule.RuleModel.RuleId);
                if(entity != null)
                {
                    entity.Title = rule.RuleModel.Title;
                    entity.Text = rule.RuleModel.Text;
                    entity.Category = rule.RuleModel.Category;
                }
                foreach (var image in rule.RulesImages)
                {
                    var imageEntity = await context.RulesImages.FindAsync(image.ImageId);
                    if (imageEntity != null)
                    {
                        imageEntity.RuleId = image.RuleId;
                        imageEntity.ImageData = image.ImageData;
                        imageEntity.ImageType = image.ImageType;
                    }
                }
            }
            context.SaveChanges();
            return rule;
        }

        public async Task<Rule> AddRule(Rule rule)
        {
            return await SaveRule(rule);
        }

        public async Task<int> DeleteRule(int id)
        {
            var entity = await context.Rules.FindAsync(id);
            if (entity != null)
            {
                context.Rules.Remove(entity);
            }
            var images = context.RulesImages.Where(x => x.RuleId == id);
            foreach (var image in images)
            {
                if (image != null)
                {
                    context.RulesImages.Remove(image);
                }
            }
            context.SaveChanges();
            return id;
        }

    }
}