using System.Collections.Generic;
using System.Threading.Tasks;
using SalesOrderAPI.API.Models;

namespace SalesOrderAPI.Application.Interfaces
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<SalesOrderDto>> GetAllOrdersAsync();
        Task<SalesOrderDto> GetOrderByIdAsync(int id);
        Task<SalesOrderDto> CreateOrderAsync(SalesOrderDto orderDto);
        Task<SalesOrderDto> UpdateOrderAsync(int id, SalesOrderDto orderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
    }
}
