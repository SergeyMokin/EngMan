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

        public IEnumerable<GuessesTheImage> GetGuessesTheImages() {
            return context.GuessesTheImages;
        }

        public GuessesTheImage GetGuessesTheImage(int id)
        {
            return context.GuessesTheImages.Where(x => x.Id == id).FirstOrDefault();
        }

        public GuessesTheImage AddGuessesTheImage(GuessesTheImageToAdd image)
        {
            GuessesTheImage returnimg = new GuessesTheImage();
            if (image.Image != null)
            {
                var bytearr = new List<byte>();
                foreach (var ch in image.Image.Data)
                {
                    bytearr.Add(Convert.ToByte(ch));
                }
                var time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                //path to folder with project
                var path = string.Format("C:\\Users\\Siarhei_Mokin\\Documents\\git\\myself\\EngMan\\EngMan\\uploads\\" + time + image.Image.Name);
                File.WriteAllBytes(path, bytearr.ToArray());
                path = string.Format("http://localhost:58099/uploads/" + time + image.Image.Name);
                returnimg = new GuessesTheImage
                {
                    Id = image.Id,
                    Word = image.Word,
                    Path = path
                };
                context.GuessesTheImages.Add(returnimg);
                context.SaveChanges();
                returnimg.Id = context.GuessesTheImages.ToArray().Last().Id;
            }
            return returnimg;
        }

        public GuessesTheImage EditGuessesTheImage(GuessesTheImage image)
        {
            var entity = context.GuessesTheImages.Find(image.Id);
            if (entity != null)
            {
                entity.Word = image.Word;
                context.SaveChanges();
            }
            return entity;
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
    }
}