using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    [Authorize]
    public class SentenceTaskController : ApiController
    {
        private ISentenceTaskService service;

        public SentenceTaskController(ISentenceTaskService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            var tasks = service.Get();
            if (tasks != null)
            {
                var categories = tasks.GroupBy(x => x.Category).Select(x => x.Key).ToList();
                return Ok(categories);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetTask(string category, int id)
        {
            if (id > -1 && category != null)
            {
                var rand = new Random();
                var tasks = service.Get();
                tasks = tasks.Where(x => x.Category == category);
                if (tasks != null)
                {
                    if (tasks.Count() - 1 <= id)
                    {
                        return NotFound();
                    }
                    IEnumerable<SentenceTask> _tasks = tasks.Select(x =>
                    {
                        var arr = x.Sentence.Split(new[] { ' ' });
                        var set = new HashSet<int>();
                        while (set.Count() != arr.Length)
                        {
                            set.Add(rand.Next(0, arr.Length));
                        }
                        var returnArr = new string[arr.Length];
                        var i = 0;
                        foreach (var ind in set)
                        {
                            returnArr[i] = arr[ind];
                            i++;
                        }
                        return new SentenceTask { SentenceTaskId = x.SentenceTaskId, Sentence = string.Join(" ", returnArr), Category = x.Category };
                    });
                    if (_tasks != null)
                    {
                        return Ok(_tasks.ElementAt(id + 1));
                    }
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(SentenceTask sentence)
        {
            if (sentence != null)
            {
                var task = service.GetById(sentence.SentenceTaskId);
                if (task != null)
                {
                    if (task.Sentence.Equals(sentence.Sentence))
                    {
                        return Ok<bool>(true);
                    }
                }
            }
            return Ok<bool>(false);
        }
    }
}
