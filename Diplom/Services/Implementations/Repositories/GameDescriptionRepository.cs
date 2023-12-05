using Diplom.AppDbContext;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;

namespace Diplom.Services.Implementations.Repositories
{
    public class GameDescriptionRepository : IBaseRepository<GameDescription>
    {
        private readonly ApplicationDbContext _context;

        public GameDescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(GameDescription entity)
        {
            await _context.GameDescriptions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<GameDescription> GetAll()
        {
            return _context.GameDescriptions;
        }

        public async Task Delete(GameDescription entity)
        {
            _context.GameDescriptions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<GameDescription> Update(GameDescription entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
