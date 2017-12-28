using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IWordService
    {
        Task<IEnumerable<Word>> Get();

        Task<Word> GetById(int id);

        Task<Word> Edit(Word word);

        Task<Word> Add(Word word);

        Task<int> Delete(int id);
    }
}
