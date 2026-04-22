using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.API.Models;
using SalesOrderAPI.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesOrderAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ISalesOrderService _orderService;

        public OrdersController(ISalesOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<SalesOrderDto>> CreateOrder(SalesOrderDto orderDto)
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, SalesOrderDto orderDto)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderDto);
            if (updatedOrder == null) return NotFound();
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("clients")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            var clients = await _orderService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _orderService.GetAllItemsAsync();
            return Ok(items);
        }
    }
}
