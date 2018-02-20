using System.Collections.Generic;
using EngMan.Repository;
using EngMan.Models;
namespace EngMan.Service
{
    public class GuessesTheImageService: IGuessesTheImageService
    {
        IGuessesTheImageRepository rep;

        public GuessesTheImageService(IGuessesTheImageRepository _rep)
        {
            rep = _rep;
        }

        public GuessesTheImageToReturn AddGuessesTheImage(GuessesTheImageToAdd image)
        {
            return rep.AddGuessesTheImage(image);
        }

        public GuessesTheImageToReturn EditGuessesTheImage(GuessesTheImageToAdd image)
        {
            return rep.EditGuessesTheImage(image);
        }

        public IEnumerable<GuessesTheImageToReturn> GetGuessesTheImages()
        {
            return rep.GetGuessesTheImages();
        }

        public GuessesTheImageToReturn GetGuessesTheImage(int id)
        {
            return rep.GetGuessesTheImage(id);
        }

        public int DeleteGuessesTheImage(int id)
        {
            return rep.DeleteGuessesTheImage(id);
        }
    }
}