using INFOION.Models;
using Microsoft.EntityFrameworkCore;

namespace INFOION.Data
{
    public class InfoionDbContext : DbContext
    {
        public InfoionDbContext(DbContextOptions<InfoionDbContext> options) : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
