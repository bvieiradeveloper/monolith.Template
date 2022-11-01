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
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get;init; }

    }
    public class ClientEntity : BaseEntity, IAggregatorRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public ClientEntity(ClientProps clientProps) : base(clientProps.Id, clientProps.CreatedAt, clientProps.UpdatedAt)
        {
            Name = clientProps.Name;
            Email = clientProps.Email;
            Address = clientProps.Address;
        }
    }
}
