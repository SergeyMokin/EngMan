using System.Collections.Generic;
using System.Linq;
using EngMan.Models;
using System;
using System.IO;

namespace EngMan.Repository
{
    public class GuessesTheImageRepository: IGuessesTheImageRepository
    {
        private EFDbContext context;

        public GuessesTheImageRepository(EFDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<GuessesTheImageToReturn> GetGuessesTheImages() {
            return context.GuessesTheImages.ToList().Select(x => new GuessesTheImageToReturn {
                Id = x.Id,
                Word = context.Words.FirstOrDefault(y => x.WordId == y.WordId),
                Path = x.Path
            });
        }

        public GuessesTheImageToReturn GetGuessesTheImage(int id)
        {
            var ggi = context.GuessesTheImages.Where(x => x.Id == id).FirstOrDefault();
            var ggiToReturn = new GuessesTheImageToReturn
            {
                Id = ggi.Id,
                Word = context.Words.FirstOrDefault(y => ggi.WordId == y.WordId),
                Path = ggi.Path
            };
            return ggiToReturn;
        }

        public GuessesTheImageToReturn AddGuessesTheImage(GuessesTheImageToAdd image)
        {
            GuessesTheImage returnimg = new GuessesTheImage();
            if (image.Image != null)
            {
                var path = saveImage(image.Image);
                returnimg = new GuessesTheImage
                {
                    Id = image.Id,
                    WordId = image.WordId,
                    Path = path
                };
                context.GuessesTheImages.Add(returnimg);
                context.SaveChanges();
                returnimg.Id = context.GuessesTheImages.ToArray().Last().Id;
            }
            return GetGuessesTheImage(returnimg.Id);
        }

        public GuessesTheImageToReturn EditGuessesTheImage(GuessesTheImageToAdd image)
        {
            var entity = context.GuessesTheImages.Find(image.Id);
            if (entity != null)
            {
                entity.WordId = image.WordId;
                if (image.Image != null)
                {
                    if (image.Image.Data.Length > 0 && image.Image.Name.Length > 0)
                    {
                        entity.Path = saveImage(image.Image);
                    }
                }
                context.SaveChanges();
            }
            return GetGuessesTheImage(entity.Id);
        }

        public int DeleteGuessesTheImage(int id)
        {
            if (id > 0)
            {
                var entity = context.GuessesTheImages.Find(id);
                if (entity != null)
                {
                    context.GuessesTheImages.Remove(entity);
                }
                context.SaveChanges();
                return id;
            }
            return -1;
        }

        private string saveImage(Image image) {
            var bytearr = new List<byte>();
            foreach (var ch in image.Data)
            {
                bytearr.Add(Convert.ToByte(ch));
            }
            var time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            //path to folder with project
            var path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/uploads/" + time + image.Name));
            File.WriteAllBytes(path, bytearr.ToArray());
            return string.Format("http://localhost:58099/uploads/" + time + image.Name);
        }
    }
}