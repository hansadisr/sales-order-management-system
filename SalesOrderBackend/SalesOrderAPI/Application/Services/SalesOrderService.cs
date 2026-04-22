using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrderAPI.Domain.Entities;
using SalesOrderAPI.API.Models;
using SalesOrderAPI.Infrastructure.Data;
using SalesOrderAPI.Application.Interfaces;

namespace SalesOrderAPI.Application.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _repository;
        private readonly IMapper _mapper;

        public SalesOrderService(ISalesOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SalesOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllWithDetailsAsync();
            return _mapper.Map<IEnumerable<SalesOrderDto>>(orders);
        }

        public async Task<SalesOrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _repository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<SalesOrderDto>(order);
        }

        public async Task<SalesOrderDto> CreateOrderAsync(SalesOrderDto orderDto)
        {
            var order = _mapper.Map<SalesOrder>(orderDto);
            order.OrderDate = DateTime.Now;

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            return await GetOrderByIdAsync(order.OrderId);
        }

        public async Task<SalesOrderDto> UpdateOrderAsync(int id, SalesOrderDto orderDto)
        {
            var existingOrder = await _repository.GetByIdWithDetailsAsync(id);
            if (existingOrder == null) return null;

            // Map updated properties
            _mapper.Map(orderDto, existingOrder);
            existingOrder.OrderId = id; // Ensure ID remains same


            
            await _repository.UpdateAsync(existingOrder);
            await _repository.SaveChangesAsync();
            
            return await GetOrderByIdAsync(id);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var existingOrder = await _repository.GetByIdWithDetailsAsync(id);
            if (existingOrder == null) return false;

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _repository.GetAllClientsAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            var items = await _repository.GetAllItemsAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }
    }
}
