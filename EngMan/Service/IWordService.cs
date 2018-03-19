using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Models;
namespace EngMan.Service
{
    public interface IWordService
    {
        //get all words
        IEnumerable<Word> Get();

        //get word by id
        Word GetById(int id);

        //edit word im db
        Task<bool> Edit(Word word);

        //add word to db
        bool Add(Word word);

        //delete word from db
        Task<string> Delete(int id);

        //get word by category
        IEnumerable<Word> GetByCategory(string category);

        //get all categories of words
        IEnumerable<string> GetAllCategories();

        //translate - return original and them translations, !translate - return translate and them origins
        //get WordMap task
        MapWord GetTask(string category, string indexes, bool translate);

        //translate - verification correctness of translate, !translate - verification correctness of original
        //verifivation correctness of the WordMap taks
        bool VerificationCorrectness(Word task, bool translate);
    }
}
