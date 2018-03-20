using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IWordRepository: IRepository<Word, Word>
    {
        IQueryable<Word> GetTasks(string category, IEnumerable<int> indexes = default(int[]));
    }
}
