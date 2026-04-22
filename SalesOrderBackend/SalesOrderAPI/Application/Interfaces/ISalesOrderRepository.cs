using System.Collections.Generic;
using System.Threading.Tasks;
using SalesOrderAPI.Domain.Entities;

namespace SalesOrderAPI.Application.Interfaces
{
    public interface ISalesOrderRepository
    {
        Task<IEnumerable<SalesOrder>> GetAllWithDetailsAsync();
        Task<SalesOrder> GetByIdWithDetailsAsync(int id);
        Task AddAsync(SalesOrder order);
        Task UpdateAsync(SalesOrder order);
        Task DeleteAsync(int id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task SaveChangesAsync();
    }
}
