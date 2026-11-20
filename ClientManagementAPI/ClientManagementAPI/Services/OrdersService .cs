using AutoMapper;
using ClientManagementAPI.Data;
using ClientManagementAPI.DTO.OrderDTO;
using ClientManagementAPI.DTOs;
using ClientManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementAPI.Services
{
    public class OrdersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderResponseDto>> GetAllAsync()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Client)
                .ToListAsync();

            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto?> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Client)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return null;
            }

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<OrderResponseDto?> CreateAsync(
            CreateOrderDto dto)
        {
            var client = await _context.Clients
                .FindAsync(dto.ClientId);

            if (client == null)
            {
                return null;
            }

            var order = new Order
            {
                ClientId = dto.ClientId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = dto.TotalAmount
            };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return new OrderResponseDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                ClientId = client.Id,
                ClientName = client.FullName
            };
        }
    }

}
