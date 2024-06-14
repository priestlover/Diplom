using Diplom.AppDbContext;
using Diplom.Services.Interfaces;
using Diplom.Models.Entity;

namespace Diplom.Services.Implementations.Repositories
{
    public class TagRepository : IBaseRepository<Tag>
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Tag entity)
        {
            await _context.Tags.AddAsync(entity);    
        }

        public async Task Delete(Tag entity)
        {
            _context.Tags.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Tag> GetAll()
        {
            return _context.Tags;
        }

        public async Task<Tag> Update(Tag entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
