using Diplom.AppDbContext;
using Diplom.Services.Interfaces;
using Diplom.Models.Entity;

namespace Diplom.Services.Implementations.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders;
        }

        public async Task Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }



        public async Task<Order> Update(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
