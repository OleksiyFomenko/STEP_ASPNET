using AutoMapper;
using ClientManagementAPI.Data;
using ClientManagementAPI.DTO;
using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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



        public async Task<PagedResult<ClientResponseDto>> GetClientsAsync(
        ClientQueryParameters parameters, CancellationToken cancellationToken)
        {
            // 1. Перевірка параметрів
            int page = parameters.Page < 1
                ? 1
                : parameters.Page;

            int size = parameters.Size < 1
                ? 10
                : parameters.Size;

            // Обмеження максимальної кількості записів
            size = Math.Min(size, 100);

            // 2. Створення запиту
            IQueryable<Client> query = _context.Clients
                .AsNoTracking();

            // 3. Фільтрація
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                string filter = parameters.Filter.Trim();

                query = query.Where(c =>
                    c.FullName.Contains(filter) ||
                    c.Email.Contains(filter));
            }

            // 4. Підрахунок записів
            int totalCount = await query.CountAsync(cancellationToken);

            // 5. Сортування
            query = ApplySorting(
                query,
                parameters.Sort,
                parameters.Direction);

            // 6. Пагінація
            int skip = (page - 1) * size;           

            var clients = await query
                .Skip(skip)
                .Take(size)
                .ToListAsync(cancellationToken);

            // 7. Перетворення в DTO
            var clientDto = _mapper.Map<List<ClientResponseDto>>(clients);

            // 8. Формування результату
            return PagedResult<ClientResponseDto>.Create(
                clientDto,
                page,
                size,
                totalCount);
        }

        private IQueryable<Client> ApplySorting(
            IQueryable<Client> query,
            string? sort,
            string? direction)
        {
            bool descending = direction?.ToLower() == "desc";

            return sort?.ToLower() switch
            {
                "fullname" => descending
                    ? query.OrderByDescending(c => c.FullName)
                           .ThenByDescending(c => c.Id)
                    : query.OrderBy(c => c.FullName)
                           .ThenBy(c => c.Id),

                "email" => descending
                    ? query.OrderByDescending(c => c.Email)
                           .ThenByDescending(c => c.Id)
                    : query.OrderBy(c => c.Email)
                           .ThenBy(c => c.Id),

                "id" => descending
                    ? query.OrderByDescending(c => c.Id)
                    : query.OrderBy(c => c.Id),

                _ => query.OrderBy(c => c.Id)
            };
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
