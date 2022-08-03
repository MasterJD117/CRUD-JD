using AutoMapper;
using CRUD.Api.Entity;
using CRUD.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Api.Data
{
    public class CrudMapping : Profile
    {
        public CrudMapping()
        {
            CreateMap<ClienteModel, Cliente>();

            CreateMap<Cliente, ClienteModel>();
        }
    }
}
