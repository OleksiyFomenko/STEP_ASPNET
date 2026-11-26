using ClientManagementAPI.Data;
using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Exceptions;
using ClientManagementAPI.Filters;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementAPI.Controllers
{
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
        public async Task<ActionResult<List<ClientResponseDto>>> GetAll()
        {
            var clients = await _clientsService.GetAllAsync();

            return Ok(clients);
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
        [ServiceFilter(typeof(ValidationFilter<ClientCreateDto>))]
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

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("This is a test exeption");
        }

        [HttpGet("{id}/test-not-found")]
        public IActionResult TestNotFound(int id)
        {
            throw new ClientNotFoundException(id);
        }

    }

}