using Diplom.Helpers;
using Diplom.Models.Authorization;
using Diplom.Models.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Diplom.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<GameDescription> GameDescriptions { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);
                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "admin",
                        Password = HashPasswordHelper.HashPassword("bebra"),
                        Role = Role.Admin,
                        Email = $"roman-maslov423@yandex.ru"
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "testUser",
                        Password = HashPasswordHelper.HashPassword("12345"),
                        Role = Role.User,
                        Email = $"roman-maslov423@yandex.ru"
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
                

                builder.HasOne(x => x.Basket)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);
                builder.HasData(new Basket
                {
                    Id = 1,
                    UserId = 1
                });
            });


            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(x => x.Basket)
                .WithMany(y => y.Orders)
                .HasForeignKey(x => x.BasketId);
            });


            modelBuilder.Entity<Game>(builder =>
            {
                builder.ToTable("Games").HasKey(x => x.Id);

                builder.HasOne(x => x.GameDescription)
                .WithOne(x => x.Game)
                .HasPrincipalKey<Game>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(x => x.Tags)
                .WithMany(p => p.Games);

                builder.HasMany(x => x.gameReviews)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId).IsRequired();
              
            }
            );


            modelBuilder.Entity<GameDescription>(builder =>
            {
                builder.ToTable("GameDescriptions").HasKey(x => x.Id);
            });

            modelBuilder.Entity<Tag>(builder =>
            {
                builder.ToTable("Tags").HasKey(x => x.TagId);
            });

            modelBuilder.Entity<GameReview>(builder =>
            {
                builder.ToTable("GameReviews").HasKey(x => x.Id);
            });
        }
    }
}
