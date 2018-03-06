using System.Collections.Generic;
using System.Data.SqlClient;
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

        public IEnumerable<string> GetAllCategories()
        {
            return context.Database.SqlQuery<string>(
              @"SELECT [Category]
                FROM [EngMan].[dbo].[GuessesTheImages]
                JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]
                GROUP BY [Category]"
            );
        }

        public IEnumerable<GuessesTheImageToReturn> GetByCategory(string category)
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            category = category.ToLower();
            return context.Database.SqlQuery<GuessesTheImageWithTheQueryBD>(
                  @"SELECT [Id]
                    , [EngMan].[dbo].[GuessesTheImages].[WordId]
                    , [Original]
                    , [Translate]
                    , [Category]
                    , [Transcription]
                    , [Path]
                    FROM [EngMan].[dbo].[GuessesTheImages]
                    JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]
                    WHERE LOWER([Category]) LIKE LOWER(@category)",
                  new SqlParameter("category", category)
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
                });
        }

        public IEnumerable<GuessesTheImageToReturn> GetTasks(string category, IEnumerable<int> indexes = default(int[]))
        {
            if (category == null)
            {
                throw new System.ArgumentNullException();
            }
            var query = @"SELECT [Id]
                    , [EngMan].[dbo].[GuessesTheImages].[WordId]
                    , [Original]
                    , [Translate]
                    , [Category]
                    , [Transcription]
                    , [Path]
                    FROM [EngMan].[dbo].[GuessesTheImages]
                    JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]
                    WHERE LOWER([Category]) LIKE LOWER('" + category + "')";
            foreach (var index in indexes)
            {
                query += (" AND [Id]!=" + index);
            }
            return context.Database.SqlQuery<GuessesTheImageWithTheQueryBD>(query)
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
                });
        }

        public IEnumerable<GuessesTheImageToReturn> GetAll()
        {
            return context.Database.SqlQuery<GuessesTheImageWithTheQueryBD>(
                  @"SELECT [Id]
                    , [EngMan].[dbo].[GuessesTheImages].[WordId]
                    , [Original]
                    , [Translate]
                    , [Category]
                    , [Transcription]
                    , [Path]
                    FROM [EngMan].[dbo].[GuessesTheImages]
                    JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]"
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

        public GuessesTheImageToReturn Get(int id)
        {
            return context.Database.SqlQuery<GuessesTheImageWithTheQueryBD>(
                @"SELECT [Id]
                ,[EngMan].[dbo].[GuessesTheImages].[WordId]
                ,[Original]
                ,[Translate]
                ,[Category]
                ,[Transcription]
                ,[Path]
                FROM [EngMan].[dbo].[GuessesTheImages]
                JOIN [EngMan].[dbo].[Words] ON [EngMan].[dbo].[Words].[WordId] = [EngMan].[dbo].[GuessesTheImages].[WordId]
                WHERE [EngMan].[dbo].[GuessesTheImages].[Id] = " + id
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

        public bool Add(GuessesTheImageToAdd image)
        {
            if (image == null)
            {
                throw new System.ArgumentNullException();
            }
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
                return true;
            }
            return false;
        }

        public bool Edit(GuessesTheImageToAdd image)
        {
            if (image == null)
            {
                throw new System.ArgumentNullException();
            }
            var entity = context.GuessesTheImages.Find(image.Id);
            if (entity != null)
            {
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
            return false;
        }

        public int Delete(int id)
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