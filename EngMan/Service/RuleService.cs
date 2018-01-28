using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using System.IO;
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

        public IEnumerable<RuleModel> Get()
        {
            return rep.Rules;
        }

        public RuleModel GetById(int id)
        {
            return rep.Rules.FirstOrDefault(x => x.RuleId == id);
        }

        public async Task<RuleModel> Edit(RuleModel rule)
        {
            return await rep.SaveRule(rule);
        }

        public async Task<RuleModel> Add(RuleModel rule)
        {
            return await rep.AddRule(rule);
        }

        public async Task<int> Delete(int id)
        {
            return await rep.DeleteRule(id);
        }

        public List<string> AddImages(Image[] images) {
            var pathes = new List<string>();
            var arr = new List<byte[]>();
            if (images != null)
            {
                foreach (var el in images)
                {
                    var bytearr = new List<byte>();
                    foreach (var ch in el.Data)
                    {
                        bytearr.Add(Convert.ToByte(ch));
                    }
                    arr.Add(bytearr.ToArray());
                }
                for (int i = 0; i < arr.Count; i++)
                {
                    var time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    var path = string.Format("C:\\Users\\limon\\Documents\\Tasks\\EngMan\\EngMan\\uploads\\" + time + images[i].Name);
                    File.WriteAllBytes(path, arr[i]);
                    path = string.Format("http://localhost:58099/uploads/" + time + images[i].Name);
                    pathes.Add(path);
                }
            }
            return pathes;
        }
    }
}