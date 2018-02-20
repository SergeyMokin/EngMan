using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IGuessesTheImageRepository
    {
        GuessesTheImageToReturn AddGuessesTheImage(GuessesTheImageToAdd image);

        GuessesTheImageToReturn EditGuessesTheImage(GuessesTheImageToAdd image);

        IEnumerable<GuessesTheImageToReturn> GetGuessesTheImages();

        GuessesTheImageToReturn GetGuessesTheImage(int id);

        int DeleteGuessesTheImage(int id);
    }
}
