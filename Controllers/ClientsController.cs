using jycbackend.Data;
using jycbackend.DTOs;
using jycbackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jycbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clients
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ClientGetDto>>>> GetClients()
        {
            var clients = await _context.Clients
                                .Select(c => new ClientGetDto
                                {
                                    Id = c.Id,
                                    ClientDoc = c.ClientDoc,
                                    FirstName = c.FirstName,
                                    LastName = c.LastName,
                                    Email = c.Email,
                                    Phone = c.Phone,
                                    Address = c.Address, 
                                    CreatedAt = c.CreatedAt
                                })
                                .ToListAsync();

            var response = new ApiResponse<List<ClientGetDto>>(true, clients, "Clients retrieved successfully.");
            return Ok(response);
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ClientGetDto>>> GetClient(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                var notFoundResponse = new ApiResponse<ClientGetDto>(
                    success: false,
                    data: null!,
                    message: $"Client with id {id} not found."
                );
                return NotFound(notFoundResponse);
            }

            var clientDto = new ClientGetDto
            {
                Id = client.Id,
                ClientDoc = client.ClientDoc,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address, 
                CreatedAt = client.CreatedAt
            };

            var response = new ApiResponse<ClientGetDto>(
                success: true,
                data: clientDto,
                message: "Client retrieved successfully."
            );
            return Ok(response);
        }


        // POST
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Client>>> CreateClient([FromBody] ClientCreateDto dto)
        {
            // Verificar si ya existe un cliente con el email
            bool emailExists = await _context.Clients.AnyAsync(c => c.Email == dto.Email);
            if (emailExists)
            {
                var errorResponse = new ApiResponse<Client>(false, null!, "Email already in use.");
                return BadRequest(errorResponse);
            }

            var client = new Client
            {
                Id = Guid.NewGuid(),
                ClientDoc = dto.ClientDoc,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address, 
            };

            _context.Clients.Add(client);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var errorResponse = new ApiResponse<Client>(false, null!, "Error saving client.");
                return StatusCode(500, errorResponse);
            }

            var response = new ApiResponse<Client>(true, client, "Client created successfully.");
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, response);
        }


        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> UpdateClient(Guid id, [FromBody] ClientUpdateDto dto)
        {
            var existingClient = await _context.Clients.FindAsync(id);

            if (existingClient == null)
            {
                var notFoundResponse = new ApiResponse<object>(false, null!, "Client not found.");
                return NotFound(notFoundResponse);
            }

            existingClient.FirstName = dto.FirstName;
            existingClient.LastName = dto.LastName;
            existingClient.Email = dto.Email;
            existingClient.Phone = dto.Phone;
            existingClient.Address = dto.Address; 

            try
            {
                await _context.SaveChangesAsync();
                var response = new ApiResponse<object>(true, null!, "Client updated successfully.");
                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    var notFoundResponse = new ApiResponse<object>(false, null!, "Client not found.");
                    return NotFound(notFoundResponse);
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ClientExists(Guid id)
        {
            return _context.Clients.Any(c => c.Id == id);
        }
    }
}
