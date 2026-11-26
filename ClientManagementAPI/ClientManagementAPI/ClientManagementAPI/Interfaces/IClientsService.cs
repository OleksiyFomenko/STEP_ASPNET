using ClientManagementAPI.DTO.ClientDTO;

namespace ClientManagementAPI.Interfaces
{
    public interface IClientsService
    {
        Task<List<ClientResponseDto>> GetAllAsync();

        Task<ClientResponseDto?> GetByIdAsync(int id);

        Task<ClientResponseDto> CreateAsync(
            ClientCreateDto dto);

        Task<bool> UpdateAsync(
            int id,
            ClientUpdateDto dto);

        Task<bool> DeleteAsync(int id);


    }

}
