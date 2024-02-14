using Diplom.AppDbContext;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;

namespace Diplom.Services.Implementations.Repositories
{
    public class GameReviewRepository : IBaseRepository<GameReview>
    {
        private readonly ApplicationDbContext _context;

        public GameReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(GameReview entity)
        {
            await _context.GameReviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<GameReview> GetAll()
        {
            return _context.GameReviews;
        }

        public async Task Delete(GameReview entity)
        {
            _context.GameReviews.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<GameReview> Update(GameReview entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
