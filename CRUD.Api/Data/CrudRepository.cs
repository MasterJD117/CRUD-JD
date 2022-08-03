using CRUD.Api.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Api.Data
{
    public class CrudRepository: ICrudRepository
    {
        private readonly CrudDbContext _context;
        private readonly ILogger _logger;

        public CrudRepository(CrudDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Cliente> GetAllClientes()
        {
            try
            {
                _logger.LogInformation("GetAllClientes call");

                return _context.Clientes
                    .OrderBy(client => client.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fail: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
