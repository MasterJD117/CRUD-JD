using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD.Api;
using CRUD.Api.Entity;
using Microsoft.Extensions.Logging;
using CRUD.Api.Models;
using AutoMapper;
using CRUD.Api.Data;

namespace CRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly CrudDbContext _context;
        private readonly ILogger<ClientesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICrudRepository _repository;

        public ClientesController(CrudDbContext context, ILogger<ClientesController> logger, 
            IMapper mapper,
            ICrudRepository repository)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Clientes
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Cliente>> GetClientes()
        {
            try
            {
                return Ok(_repository.GetAllClientes());

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }

        // GET: by Id
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cliente>> GetCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT:
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult PutCliente(Guid id, ClienteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = _context.Clientes.Find(id);
                    if (cliente == null)
                    {
                        _logger.LogError($"cliente with id: {id}, hasn't been found in db.");
                        return NotFound();
                    }
                    cliente.Name = model.Name;
                    cliente.LastName = model.LastName;
                    cliente.City = model.City;
                    cliente.Phone = model.Phone;
                    cliente.Email = model.Email;
                    if (_repository.SaveAll())
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new Client: {ex}");
            }
            return BadRequest("Failed to save new Client");
        }

        // POST:
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult PostCliente([FromBody] ClienteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newClient = _mapper.Map<ClienteModel, Cliente>(model);

                    _repository.AddEntity(newClient);
                    if (_repository.SaveAll())
                    {
                        return Ok(_mapper.Map<Cliente, ClienteModel>(newClient));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new Client: {ex}");
            }
            return BadRequest("Failed to save new Client");

        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Cliente>> DeleteCliente(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
