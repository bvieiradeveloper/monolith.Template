namespace Product.Adm.API.Dto.Input.Client
{
    public class AddClientInputDto
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Document { get; init; }
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
    }
}
