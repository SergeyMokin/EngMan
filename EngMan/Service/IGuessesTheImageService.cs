using System.Collections.Generic;
using EngMan.Models;

namespace EngMan.Service
{
    public interface IGuessesTheImageService
    {
        //add GuessesTheImage to db
        bool Add(GuessesTheImageToAdd image);

        //edit GuessesTheImage in db
        bool Edit(GuessesTheImageToAdd image);

        //get all GuessesTheImages
        IEnumerable<GuessesTheImageToReturn> GetAll();

        GuessesTheImageToReturn Get(int id);

        //delete GuessesTheImage from db
        string Delete(int id);

        //get all categories of GuessesTheImages
        IEnumerable<string> GetAllCategories();

        //get GuessesTheImage by category
        IEnumerable<GuessesTheImageToReturn> GetByCategory(string category);

        //get GuessesTheImageTask
        GuessesTheImageToReturn GetTask(string category, string indexes);

        //verification correctness of the GuessesTheImageTask
        bool VerificationCorrectness(GuessesTheImageToReturn img);

    }
}