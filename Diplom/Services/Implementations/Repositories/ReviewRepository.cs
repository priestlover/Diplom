using Diplom.AppDbContext;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;

namespace Diplom.Services.Implementations.Repositories
{
    public class ReviewRepository : IBaseRepository<Review>
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Review> GetAll()
        {
            return _context.Reviews;
        }

        public async Task Delete(Review entity)
        {
            _context.Reviews.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> Update(Review entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
