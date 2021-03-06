﻿using System.Data.Entity;
using EngMan.Models;

namespace EngMan.Repository
{
    public class EFDbContext : DbContext
    {
        public DbSet<RuleModel> Rules { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<SentenceTask> SentenceTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GuessesTheImage> GuessesTheImages { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
    }
}