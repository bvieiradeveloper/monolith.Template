using _Shared.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Adm.UseCase.AddClient
{
    public class AddClientInputDto
    {
        public Id? Id { get; set; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }

    public class AddClientOutputDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
