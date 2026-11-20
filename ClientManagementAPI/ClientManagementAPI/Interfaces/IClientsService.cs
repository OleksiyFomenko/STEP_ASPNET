using ClientManagementAPI.DTO;
using ClientManagementAPI.DTO.ClientDTO;

namespace ClientManagementAPI.Interfaces
{
    public interface IClientsService
    {
        Task<PagedResult<ClientResponseDto>> GetClientsAsync(
            ClientQueryParameters parameters,
        CancellationToken cancellationToken);

        Task<ClientResponseDto?> GetByIdAsync(int id);

        Task<ClientResponseDto> CreateAsync(
            ClientCreateDto dto);

        Task<bool> UpdateAsync(
            int id,
            ClientUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }

}
