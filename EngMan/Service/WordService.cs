using System.Collections.Generic;
using System.Threading.Tasks;
using EngMan.Repository;
using System.Linq;
using EngMan.Models;
using EngMan.Extensions;
using System.Net.Http;
namespace EngMan.Service
{
    public class WordService: IWordService
    {
        private readonly IWordRepository rep;

        public WordService(IWordRepository _rep)
        {
            rep = _rep;
        }

        public IEnumerable<Word> GetByCategory(string category)
        {
            if (category.Validate())
            {
                try
                {
                    return rep.GetByCategory(category);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<string> GetAllCategories()
        {
            try
            {
                return rep.GetAllCategories();
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public IEnumerable<Word> Get()
        {
            try
            {
                return rep.Words;
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public Word GetById(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.Words.FirstOrDefault(x => x.WordId == id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<Word> Edit(Word word)
        {
            if (word.Validate())
            {
                try
                {
                    return await rep.SaveWord(word);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<Word> Add(Word word)
        {
            if (word.Validate())
            {
                try
                {
                    return await rep.AddWord(word);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public async Task<int> Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return await rep.DeleteWord(id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }
    }
}