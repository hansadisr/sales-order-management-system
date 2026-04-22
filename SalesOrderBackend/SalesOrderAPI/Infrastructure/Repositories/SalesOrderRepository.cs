using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesOrderAPI.Application.Interfaces;
using SalesOrderAPI.Domain.Entities;
using SalesOrderAPI.Infrastructure.Data;

namespace SalesOrderAPI.Infrastructure.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly SalesDbContext _context;

        public SalesOrderRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllWithDetailsAsync()
        {
            return await _context.SalesOrders
                .Include(o => o.Client)
                .Include(o => o.SalesOrderDetails)
                .ThenInclude(d => d.Item)
                .ToListAsync();
        }

        public async Task<SalesOrder> GetByIdWithDetailsAsync(int id)
        {
            return await _context.SalesOrders
                .Include(o => o.Client)
                .Include(o => o.SalesOrderDetails)
                .ThenInclude(d => d.Item)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task AddAsync(SalesOrder order)
        {
            await _context.SalesOrders.AddAsync(order);
        }

        public async Task UpdateAsync(SalesOrder order)
        {
            // In EF Core, if the entity is tracked, it's already updated.
            // But we handle details replacement in the service for now as it's a simple approach.
            _context.Entry(order).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.SalesOrders.FindAsync(id);
            if (order != null)
            {
                var details = _context.SalesOrderDetails.Where(d => d.OrderId == id);
                _context.SalesOrderDetails.RemoveRange(details);
                _context.SalesOrders.Remove(order);
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
