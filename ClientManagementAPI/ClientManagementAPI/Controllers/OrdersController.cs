using ClientManagementAPI.DTOs;
using ClientManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _ordersService.GetAllAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _ordersService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var order = await _ordersService.CreateAsync(dto);

            if (order == null)
            {
                return BadRequest("Client does not exist");
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.Id },
                order
            );
        }
    }

}
