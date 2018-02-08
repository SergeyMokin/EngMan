using System.Collections.Generic;
using EngMan.Models;

namespace EngMan.Service
{
    public interface IGuessesTheImageService
    {
        GuessesTheImage AddGuessesTheImage(GuessesTheImageToAdd image);

        GuessesTheImage EditGuessesTheImage(GuessesTheImage image);

        IEnumerable<GuessesTheImage> GetGuessesTheImages();

        GuessesTheImage GetGuessesTheImage(int id);

        int DeleteGuessesTheImage(int id);
    }
}