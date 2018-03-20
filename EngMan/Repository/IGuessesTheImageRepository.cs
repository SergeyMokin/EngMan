using System.Collections.Generic;
using EngMan.Models;
using System.Linq;
namespace EngMan.Repository
{
    public interface IGuessesTheImageRepository: IRepository<GuessesTheImageToReturn, GuessesTheImageToAdd>
    {
        IQueryable<GuessesTheImageToReturn> GetTasks(string category, IEnumerable<int> indexes = default(int[]));
    }
}
