using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EngMan.Repository
{
    public class RuleRepository: IRepository
    {

        public IEnumerable<RuleWithImages> Rules {
            get
            {
                var rules = context.Rules.ToArray();
                var images = context.RulesImages.ToArray();
                var rulesWithImages = rules.Select(x => new RuleWithImages() { Id = x.RuleId, Rule = x, RulesImages = images.Where(y => y.RuleId == x.RuleId)});
                return rulesWithImages;
            }
        }

        private EFDbContext context;

        public RuleRepository(EFDbContext _context)
        {
            context = _context;
        }

        public async Task<RuleWithImages> SaveRule(RuleWithImages rule)
        {
            if (rule.Rule.RuleId == 0)
            {
                context.Rules.Add(rule.Rule);
                rule.Rule.RuleId = context.Rules.Last().RuleId;
                foreach (var image in rule.RulesImages)
                {
                    image.RuleId = rule.Rule.RuleId;
                    context.RulesImages.Add(image);
                    image.ImageId = context.RulesImages.Last().ImageId;
                }
            }
            else
            {
                var entity = await context.Rules.FindAsync(rule.Rule.RuleId);
                if(entity != null)
                {
                    entity.Title = rule.Rule.Title;
                    entity.Text = rule.Rule.Text;
                    entity.Category = rule.Rule.Category;
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

        public async Task<RuleWithImages> AddRule(RuleWithImages rule)
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