﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EngMan.Models;
namespace EngMan.Repository
{
    public class WordRepository: IWordRepository
    {
        public IEnumerable<Word> Words { get { return context.Words; } }

        private EFDbContext context;

        public WordRepository(EFDbContext _context)
        {
            context = _context;
        }

        public async Task<Word> SaveWord(Word word)
        {
            if (word.WordId == 0)
            {
                context.Words.Add(word);
                word.WordId = context.Words.Last().WordId;
            }
            else
            {
                var entity = await context.Words.FindAsync(word.WordId);
                if (entity != null)
                {
                    entity.Original = word.Original;
                    entity.Translate = entity.Translate;
                    entity.Category = word.Category;
                }
            }
            return word;
        }

        public async Task<Word> AddWord(Word word)
        {
            return await SaveWord(word);
        }

        public async Task<int> DeleteWord(int id)
        {
            if (id > 0)
            {
                var entity = await context.Words.FindAsync(id);
                if (entity != null)
                {
                    context.Words.Remove(entity);
                }
                return id;
            }
            return -1;
        }
    }
}