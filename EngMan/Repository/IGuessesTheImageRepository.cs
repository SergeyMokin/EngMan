using System.Collections.Generic;
using EngMan.Models;
namespace EngMan.Repository
{
    public interface IGuessesTheImageRepository
    {
        GuessesTheImage AddGuessesTheImage(GuessesTheImageToAdd image);

        GuessesTheImage EditGuessesTheImage(GuessesTheImage image);

        IEnumerable<GuessesTheImage> GetGuessesTheImages();

        GuessesTheImage GetGuessesTheImage(int id);

        int DeleteGuessesTheImage(int id);
    }
}
