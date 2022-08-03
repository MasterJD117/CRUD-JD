using CRUD.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Api.Data
{
    public interface ICrudRepository
    {
        IEnumerable<Cliente> GetAllClientes();
        bool SaveAll();
        void AddEntity(object model);
    }
}
