using CRUD.Api.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Api
{
    public class CrudDbContext: DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

    }
}
