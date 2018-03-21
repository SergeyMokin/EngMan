using System.Collections.Generic;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public class GuessesTheImageRepository: IGuessesTheImageRepository
    {
        private EFDbContext context;

        public GuessesTheImageRepository(EFDbContext _context)
        {
            context = _context;
        }
        
        public IQueryable<GuessesTheImageToReturn> GetAll()
        {
            return context.GuessesTheImages
                .Join(context.Words,
                GTI => GTI.WordId,
                word => word.WordId,
                (GTI, word) => new { GuessesTheImage = GTI, Word = word })
                .Select(x => new GuessesTheImageToReturn
                {
                    Id = x.GuessesTheImage.Id,
                    Word = x.Word,
                    Path = x.GuessesTheImage.Path
                });
        }

        public GuessesTheImageToReturn Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Add(GuessesTheImageToAdd image)
        {
            if (image == null)
            {
                throw new System.ArgumentNullException();
            }
            GuessesTheImage returnimg = new GuessesTheImage();
            if (image.Image == null)
            {
                return false;
            }
            var path = Extensions.Extensions.SaveImage(image.Image);
            returnimg = new GuessesTheImage
            {
                Id = image.Id,
                WordId = image.WordId,
                Path = path
            };
            context.GuessesTheImages.Add(returnimg);
            context.SaveChanges();
            return true;
        }

        public bool Edit(GuessesTheImageToAdd image)
        {
            if (image == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.GuessesTheImages.Find(image.Id);
            if (entity == null)
            {
                return false;
            }
            entity.WordId = image.WordId;
            if (image.Image != null)
            {
                if (image.Image.Data != null && image.Image.Name != null)
                {
                    entity.Path = Extensions.Extensions.SaveImage(image.Image);
                }
            }
            context.SaveChanges();
            return true;
        }

        public int Delete(int id)
        {
            if (id < 0)
            {
                return -1;
            }
            var entity = context.GuessesTheImages.Find(id);
            if (entity == null)
            {
                return -1;
            }
            context.GuessesTheImages.Remove(entity);
            context.SaveChanges();
            return id;
        }

        public IQueryable<string> GetAllCategories()
        {
            return GetAll().GroupBy(x => x.Word.Category).Select(x => x.Key);
        }

        public IQueryable<GuessesTheImageToReturn> GetByCategory(string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }

            return GetAll().Where(x => x.Word.Category.ToLower().Equals(category.ToLower()));
        }

        public IQueryable<GuessesTheImageToReturn> GetTasks(string category, IEnumerable<int> indexes = null)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }

            var query = GetByCategory(category);
            var length = indexes == null ? 0 : indexes.Count();

            for (var i = 0; i < length; i++)
            {
                var index = indexes.ElementAt(i);
                query = query.Where(x => x.Id != index);
            }

            return query;
        }
    }
}