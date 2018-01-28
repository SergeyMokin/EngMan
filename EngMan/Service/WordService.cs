using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
namespace EngMan.Service
{
    public class WordService: IWordService
    {
        private readonly IWordRepository rep;

        public WordService(IWordRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<Word> Get()
        {
            return rep.Words;
        }

        public Word GetById(int id)
        {
            return rep.Words.FirstOrDefault(x => x.WordId == id);
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