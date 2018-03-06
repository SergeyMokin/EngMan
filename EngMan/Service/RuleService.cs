using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System;
namespace EngMan.Service
{
    public class RuleService: IRuleService
    {
        private readonly IRuleRepository rep;

        public RuleService(IRuleRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<string> GetAllCategories()
        {
            try
            {
                return rep.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RuleModel> GetByCategory(string category)
        {
            if (!String.IsNullOrEmpty(category))
            {
                try
                {
                    return rep.GetByCategory(category);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public IEnumerable<RuleModel> Get()
        {
            try
            {
                return rep.Rules;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RuleModel GetById(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.Rules.FirstOrDefault(x => x.RuleId == id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public async Task<bool> Edit(RuleModel rule)
        {
            if (rule.Validate())
            {
                try
                {
                    return await rep.SaveRule(rule);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public bool Add(RuleModel rule)
        {
            if (rule.Validate())
            {
                try
                {
                    return rep.AddRule(rule);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public async Task<int> Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return await rep.DeleteRule(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }

        public List<string> AddImages(Image[] images)
        {
            if (images != null && images.Length > 0)
            {
                try
                {
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
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            throw new Exception("Invalid model");
        }
    }
}