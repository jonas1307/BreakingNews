using BreakingNews.Domain.Entities;
using BreakingNews.Infrastructure.Database.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BreakingNews.Infrastructure.Database
{
    public class BreakingNewsContext : IdentityDbContext
    {
        public BreakingNewsContext(DbContextOptions options)
            : base(options)
        { }

        public BreakingNewsContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=NEWDATABASEEE;Integrated Security=True;MultipleActiveResultSets=true", 
                b => b.MigrationsAssembly("BreakingNews.Infrastructure.Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NewsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<News> News { get; set; }
    }
}
