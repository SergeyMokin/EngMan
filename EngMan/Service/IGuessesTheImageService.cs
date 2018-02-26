using System.Collections.Generic;
using EngMan.Models;

namespace EngMan.Service
{
    public interface IGuessesTheImageService
    {
        bool Add(GuessesTheImageToAdd image);

        bool Edit(GuessesTheImageToAdd image);

        IEnumerable<GuessesTheImageToReturn> GetAll();

        GuessesTheImageToReturn Get(int id);

        int Delete(int id);

        IEnumerable<string> GetAllCategories();

        IEnumerable<GuessesTheImageToReturn> GetByCategory(string category);

        GuessesTheImageToReturn GetTask(string category, string indexes);

        bool VerificationCorrectness(GuessesTheImageToReturn img);

    }
}