using ClientManagementAPI.Data;
using ClientManagementAPI.DTO;
using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Model;
using ClientManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementAPI.Controllers
{
    /*
     * Тепер контролеру більше не потрібен ApplicationDbContext.
Він працюватиме тільки через інтерфейс сервісу.

     */

    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;
        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }


        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<PagedResult<ClientResponseDto>>> GetAll(
            [FromQuery]ClientQueryParameters parameters,
    CancellationToken cancellationToken)
        {
            var result = await _clientsService.GetClientsAsync(
                parameters,
                cancellationToken);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResponseDto>> GetById(int id)
        {
            var client = await _clientsService.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<ClientResponseDto>> Create(
        ClientCreateDto dto)
        {
            var client = await _clientsService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = client.Id },
                client
            );
        }


        // PUT: api/clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            ClientUpdateDto dto)
        {
            var result = await _clientsService.UpdateAsync(id, dto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clientsService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}