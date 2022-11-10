namespace Invoice.Domain.ValueObject
{
    public class AddressProps
    {
        public string Street { get; init; }
        public string Number { get; init; }
        public string Complement { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }
    }
}
