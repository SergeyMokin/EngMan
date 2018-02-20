﻿using System.Collections.Generic;
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

        public IEnumerable<GuessesTheImageToReturn> GetGuessesTheImages() {
            return context.Database.SqlQuery<GuessesTheImageWithTheQueryBD>(
                    $"SELECT [Id]" +
                    $",[EngMan].[dbo].[GuessesTheImages].[WordId]" +
                    $",[Original]" +
                    $",[Translate]" +
                    $",[Category]" +
                    $",[Transcription]" +
                    $",[Path]" +
                    $"FROM [EngMan].[dbo].[GuessesTheImages]" +
                    $"JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]"
                )
                .Select(x => new GuessesTheImageToReturn {
                    Id = x.Id,
                    Word = new Word {
                        WordId = x.WordId,
                        Category = x.Category,
                        Original = x.Original,
                        Transcription = x.Transcription,
                        Translate = x.Translate
                    },
                    Path = x.Path
                });
        }

        public GuessesTheImageToReturn GetGuessesTheImage(int id)
        {
            return context.Database.SqlQuery<GuessesTheImageWithTheQueryBD>(
                $"SELECT [Id]" +
                $",[EngMan].[dbo].[GuessesTheImages].[WordId]" +
                $",[Original]" +
                $",[Translate]" +
                $",[Category]" +
                $",[Transcription]" +
                $",[Path]" +
                $"FROM [EngMan].[dbo].[GuessesTheImages]" +
                $"JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]" +
                $"WHERE [EngMan].[dbo].[GuessesTheImages].[Id] = " + id
            )
            .Select(x => new GuessesTheImageToReturn
            {
                Id = x.Id,
                Word = new Word
                {
                    WordId = x.WordId,
                    Category = x.Category,
                    Original = x.Original,
                    Transcription = x.Transcription,
                    Translate = x.Translate
                },
                Path = x.Path
            }).FirstOrDefault();
        }

        public GuessesTheImageToReturn AddGuessesTheImage(GuessesTheImageToAdd image)
        {
            GuessesTheImage returnimg = new GuessesTheImage();
            if (image.Image != null)
            {
                var path = Extensions.Extensions.SaveImage(image.Image);
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
                        entity.Path = Extensions.Extensions.SaveImage(image.Image);
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
    }
}