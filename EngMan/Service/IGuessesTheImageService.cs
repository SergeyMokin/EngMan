using System.Collections.Generic;
using EngMan.Models;

namespace EngMan.Service
{
    public interface IGuessesTheImageService
    {
        GuessesTheImageToReturn AddGuessesTheImage(GuessesTheImageToAdd image);

        GuessesTheImageToReturn EditGuessesTheImage(GuessesTheImageToAdd image);

        IEnumerable<GuessesTheImageToReturn> GetGuessesTheImages();

        GuessesTheImageToReturn GetGuessesTheImage(int id);

        int DeleteGuessesTheImage(int id);
    }
}