using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Api.Models
{
    public class ClienteModel
    {
        public Guid Id { get; set; }
        
        [Required, MaxLength(25)]
        public string Name { get; set; }

        [Required, MaxLength(25)]
        public string LastName { get; set; }
        public string City { get; set; }

        [Required, MaxLength(10)]
        public string Phone { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }
    }
}
