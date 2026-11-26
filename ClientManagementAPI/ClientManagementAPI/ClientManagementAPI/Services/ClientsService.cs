using AutoMapper;
using ClientManagementAPI.Data;
using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementAPI.Services
{
    public class ClientsService : IClientsService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientsService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<List<ClientResponseDto>> GetAllAsync()
        {
            var clients = await _context.Clients
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<ClientResponseDto>>(clients);
        }

        public async Task<ClientResponseDto?> GetByIdAsync(int id)
        {
            var client = await _context.Clients.AsNoTracking()
                                                .FirstOrDefaultAsync(c => c.Id == id);
            if (client == null)
            {
                return null;
            }

            return _mapper.Map<ClientResponseDto>(client);
        }

        public async Task<bool> UpdateAsync(
        int id,
        ClientUpdateDto dto)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return false;
            }

            _mapper.Map(dto, client);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ClientResponseDto> CreateAsync(
         ClientCreateDto dto)
        {
            var client = _mapper.Map<Client>(dto);

            client.RegistrationDate = DateTime.UtcNow;

            _context.Clients.Add(client);

            await _context.SaveChangesAsync();

            return _mapper.Map<ClientResponseDto>(client);
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return false;
            }

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return true;
        }


    }
}
