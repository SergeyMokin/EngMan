using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
namespace EngMan.Service
{
    public class WordService: IWordService
    {
        IWordRepository rep;

        public WordService(IWordRepository _rep)
        {
            rep = _rep;
        }

        public async Task<IEnumerable<Word>> Get()
        {
            var task = new Task<IEnumerable<Word>>(() => rep.Words);
            task.Start();
            return await task;
        }

        public async Task<Word> GetById(int id)
        {
            var task = new Task<Word>(() => rep.Words.FirstOrDefault(x => x.WordId == id));
            task.Start();
            return await task;
        }

        public async Task<Word> Edit(Word word)
        {
            return await rep.SaveWord(word);
        }

        public async Task<Word> Add(Word word)
        {
            return await rep.AddWord(word);
        }

        public async Task<int> Delete(int id)
        {
            return await rep.DeleteWord(id);
        }
    }
}