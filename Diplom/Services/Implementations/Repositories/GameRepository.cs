using Diplom.AppDbContext;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;

namespace Diplom.Services.Implementations.Repositories
{
    public class GameRepository : IBaseRepository<Game>
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Game entity)
        {
            await _context.Games.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Game> GetAll()
        {
            return _context.Games;
        }

        public async Task Delete(Game entity)
        {
            _context.Games.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Game> Update(Game entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
