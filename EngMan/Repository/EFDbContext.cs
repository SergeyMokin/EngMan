using System.Data.Entity;

namespace EngMan.Repository
{
    public class EFDbContext: DbContext
    {
        public DbSet<Rule> Rules { get; set; }
        public DbSet<RulesImage> RulesImages { get; set; }
    }
}