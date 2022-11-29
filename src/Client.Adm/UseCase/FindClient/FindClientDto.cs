using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MonolithTests")]
namespace Client.Adm.UseCase.FindClient
{
    internal class FindClientInputDto
    {
        public string ClientId { get; init; }
    }

    internal class FindClientOutputDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
