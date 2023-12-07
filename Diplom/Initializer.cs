using Diplom.Services.Implementations.Repositories;
using Diplom.Services.Interfaces;
using Diplom.Models.Entity;
using Diplom.Models.Authorization;
using Diplom.Services.Implementations;

namespace Diplom
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
            services.AddScoped<IBaseRepository<Order>, OrderRepository>();
            services.AddScoped<IBaseRepository<Review>,ReviewRepository>();
            services.AddScoped<IBaseRepository<Game>, GameRepository>();
            services.AddScoped<IBaseRepository<GameDescription>, GameDescriptionRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }


    }
}
