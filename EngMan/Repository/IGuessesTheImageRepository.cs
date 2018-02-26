using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IGuessesTheImageRepository
    {
        bool Add(GuessesTheImageToAdd image);

        bool Edit(GuessesTheImageToAdd image);

        IEnumerable<GuessesTheImageToReturn> GetAll();

        GuessesTheImageToReturn Get(int id);

        int Delete(int id);

        IEnumerable<string> GetAllCategories();

        IEnumerable<GuessesTheImageToReturn> GetByCategory(string category);

        IEnumerable<GuessesTheImageToReturn> GetTasks(string category, IEnumerable<int> indexes = default(int[]));

    }
}
