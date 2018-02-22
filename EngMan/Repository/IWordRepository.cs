using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IWordRepository
    {
        IEnumerable<Word> Words { get; }

        Task<Word> SaveWord(Word word);

        Task<Word> AddWord(Word word);

        Task<int> DeleteWord(int id);

        IEnumerable<Word> GetByCategory(string category);

        IEnumerable<string> GetAllCategories();

        IEnumerable<Word> GetTasks(string category, IEnumerable<int> indexes = default(int[]));

    }
}
