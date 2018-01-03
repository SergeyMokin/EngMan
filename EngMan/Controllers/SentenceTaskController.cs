using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using EngMan.Service;
using EngMan.Models;
namespace EngMan.Controllers
{
    public class SentenceTaskController : ApiController
    {
        private ISentenceTaskService service;

        public SentenceTaskController(ISentenceTaskService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRandomTask()
        {
            var rand = new Random();
            var tasks = await service.Get();
            if (tasks != null)
            {
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
                var index = rand.Next(0, _tasks.Count());
                if (_tasks != null)
                {
                    return Ok(_tasks.ElementAt(index));
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            var rand = new Random();
            var tasks = await service.Get();
            if (tasks != null)
            {
                IEnumerable<SentenceTask> _tasks = tasks.Where(x => x.SentenceTaskId == id).Select(x =>
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
                    return Ok(_tasks.Last());
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IHttpActionResult> VerificationCorrectness(SentenceTask sentence)
        {
            if (sentence != null)
            {
                var task = await service.GetById(sentence.SentenceTaskId);
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
