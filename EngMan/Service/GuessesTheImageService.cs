using System.Collections.Generic;
using EngMan.Repository;
using EngMan.Models;
using EngMan.Extensions;
using System.Net.Http;

namespace EngMan.Service
{
    public class GuessesTheImageService: IGuessesTheImageService
    {
        IGuessesTheImageRepository rep;

        public GuessesTheImageService(IGuessesTheImageRepository _rep)
        {
            rep = _rep;
        }

        public GuessesTheImageToReturn Add(GuessesTheImageToAdd image)
        {
            if (image.Validate(true))
            {
                try
                {
                    var result = rep.Add(image);
                    return result;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public GuessesTheImageToReturn Edit(GuessesTheImageToAdd image)
        {
            if (image.Validate(false))
            {
                try
                {
                    var result = rep.Edit(image);
                    return result;
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public IEnumerable<GuessesTheImageToReturn> GetAll()
        {
            try
            {
                return rep.GetAll();
            }
            catch (System.Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public GuessesTheImageToReturn Get(int id)
        {
            if(id.Validate())
            {
                try
                {
                    return rep.Get(id);
                }
                catch (System.Exception ex)
                {
                    throw new HttpRequestException(ex.Message);
                }
            }
            throw new HttpRequestException("Invalid model");
        }

        public int Delete(int id)
        {
            if (id.Validate())
            {
                try
                {
                    return rep.Delete(id);
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

        public IEnumerable<GuessesTheImageToReturn> GetByCategory(string category)
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
    }
}