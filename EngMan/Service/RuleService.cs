using System.Collections.Generic;
using EngMan.Repository;
using EngMan.Models;
using EngMan.Extensions;
using System;
using System.Linq;

namespace EngMan.Service
{
    public class RuleService: IRuleService
    {
        private readonly IRuleRepository rep;

        public RuleService(IRuleRepository _rep)
        {
            rep = _rep;
        }

        public IQueryable<string> GetAllCategories()
        {
            return rep.GetAllCategories();
        }

        public IQueryable<RuleModel> GetByCategory(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                throw new Exception("Invalid model");
            }
            return rep.GetByCategory(category);
        }

        public IQueryable<RuleModel> GetAll()
        {
            return rep.GetAll();
        }

        public RuleModel Get(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Get(id);
        }

        public bool Edit(RuleModel rule)
        {
            if (!rule.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Edit(rule);
        }

        public bool Add(RuleModel rule)
        {
            if (!rule.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Add(rule);
        }

        public string Delete(int id)
        {
            if (!id.Validate())
            {
                throw new Exception("Invalid model");
            }
            return rep.Delete(id) > 0
                    ? "Delete completed successful"
                    : null;
        }

        public List<string> AddImages(Image[] images)
        {
            if (images == null || images.Length == 0)
            {
                throw new Exception("Invalid model");
            }
            var pathes = new List<string>();
            foreach (var img in images)
            {
                if (img.Validate())
                {
                    pathes.Add(Extensions.Extensions.SaveImage(img));
                }
            }
            return pathes;
        }
    }
}