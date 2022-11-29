using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Adm.Facade.Implementation
{
    public class FindClientInputDto
    {
        public string ClientId { get; init; }
    }

    public class FindClientOutputDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Document { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
