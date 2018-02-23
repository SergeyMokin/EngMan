using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IWordService
    {
        IEnumerable<Word> Get();

        Word GetById(int id);

        Task<bool> Edit(Word word);

        bool Add(Word word);

        Task<int> Delete(int id);

        IEnumerable<Word> GetByCategory(string category);

        IEnumerable<string> GetAllCategories();

        MapWord GetTask(string category, string indexes);

        bool VerificationCorrectness(Word task);
    }
}
