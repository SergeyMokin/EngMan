﻿using System.Collections.Generic;
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
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            return context.Rules.Where(x => x.Category.ToLower().Equals(category.ToLower()));
        }

        public async Task<bool> SaveRule(RuleModel rule)
        {
            if (rule == null)
            {
                throw new System.ArgumentNullException();
            }
            if (rule.RuleId < 1)
            {
                return false;
            }
            var entity = await context.Rules.FindAsync(rule.RuleId);
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

        public bool AddRule(RuleModel rule)
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

        public async Task<int> DeleteRule(int id)
        {
            if (id < 1)
            {
                return -1;
            }
            var entity = await context.Rules.FindAsync(id);
            if (entity == null)
            {
                return -1;
            }
            context.Rules.Remove(entity);
            context.SaveChanges();
            return id;
        }

    }
}