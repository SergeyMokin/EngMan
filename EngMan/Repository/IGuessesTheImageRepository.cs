using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IGuessesTheImageRepository
    {
        GuessesTheImageToReturn Add(GuessesTheImageToAdd image);

        GuessesTheImageToReturn Edit(GuessesTheImageToAdd image);

        IEnumerable<GuessesTheImageToReturn> GetAll();

        GuessesTheImageToReturn Get(int id);

        int Delete(int id);

        IEnumerable<string> GetAllCategories();

        IEnumerable<GuessesTheImageToReturn> GetByCategory(string category);
    }
}
