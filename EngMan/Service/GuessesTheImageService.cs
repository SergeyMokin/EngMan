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

        public GuessesTheImage AddGuessesTheImage(GuessesTheImageToAdd image)
        {
            return rep.AddGuessesTheImage(image);
        }

        public GuessesTheImage EditGuessesTheImage(GuessesTheImage image)
        {
            return rep.EditGuessesTheImage(image);
        }

        public IEnumerable<GuessesTheImage> GetGuessesTheImages()
        {
            return rep.GetGuessesTheImages();
        }

        public GuessesTheImage GetGuessesTheImage(int id)
        {
            return rep.GetGuessesTheImage(id);
        }

        public int DeleteGuessesTheImage(int id)
        {
            return rep.DeleteGuessesTheImage(id);
        }
    }
}