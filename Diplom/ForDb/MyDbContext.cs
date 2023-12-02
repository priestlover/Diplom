using Diplom.Models;
using Microsoft.EntityFrameworkCore;

namespace Diplom.ForDb
{
    public class MyDbContext:DbContext
    {
        public MyDbContext() 
        { 
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
        :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MainPageDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        public DbSet<MainPageGame> MainPageGames { get; set; }



    }
}
