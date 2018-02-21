using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Net.Http;

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
            try
            {
                var tasks = service.GetAllCategories();
                if (tasks != null)
                {
                    return Ok(tasks);
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetTask(string category, int id)
        {
            try
            {
                var tasks = service.GetByCategory(category).ToList();
                if (tasks != null)
                {
                    var rand = new Random();
                    if (tasks.Count() - 1 <= id)
                    {
                        return NotFound();
                    }
                    return Ok(tasks.Select(x =>
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
                    }).ElementAt(id + 1));
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult VerificationCorrectness(SentenceTask sentence)
        {
            try
            {
                var task = service.GetById(sentence.SentenceTaskId);
                if (task != null)
                {
                    if (task.Sentence.ToLower().Equals(sentence.Sentence.ToLower()))
                    {
                        return Ok(true);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
    }
}
