using _Shared.Domain.Entity;
using _Shared.Domain.Interface;
using _Shared.Domain.ValueObject;

namespace Client.Adm.Domain.Entity
{
    public class ClientProps
    {
        public Id? Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Address { get; init; }
        public string Document { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get;init; }

    }
    public class ClientEntity : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Document { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public ClientEntity(ClientProps clientProps) : base(clientProps.Id, clientProps.CreatedAt, clientProps.UpdatedAt)
        {
            Name = clientProps.Name;
            Email = clientProps.Email;
            Document = clientProps.Document;
            Street = clientProps.Street;
            Number = clientProps.Number;
            Complement = clientProps.Complement;
            City = clientProps.City;
            State = clientProps.State;
            ZipCode = clientProps.ZipCode;  
        }
    }
}
