using ClientManagementAPI.DTO.OrderDTO;
using ClientManagementAPI.DTOs;

namespace ClientManagementAPI.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrderResponseDto>> GetAllAsync();

        Task<OrderResponseDto?> GetByIdAsync(int id);

        Task<OrderResponseDto?> CreateAsync(
            OrderCreateDto dto);
    }

}
