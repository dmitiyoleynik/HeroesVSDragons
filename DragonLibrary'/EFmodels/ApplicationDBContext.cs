using Microsoft.EntityFrameworkCore;

namespace DragonLibrary_.EFmodels
{
    public class ApplicationDBContext: DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Hit> Hits { get; set; }
        public DbSet<Dragon> Dragons { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {   }
    }
}
