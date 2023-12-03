using Diplom.Models;
using Microsoft.EntityFrameworkCore;

namespace Diplom.AppDbContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<MainPageGame> MainPageGames { get; set; }
    }
}
