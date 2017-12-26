using System.Data.Entity;
using EngMan.Models;

namespace EngMan.Repository
{
    public class EFDbContext: DbContext
    {
        public DbSet<RuleModel> Rules { get; set; }
        public DbSet<RuleImage> RulesImages { get; set; }
    }
}