
namespace Invoice.Domain.ValueObject
{
    public class Address : _Shared.Domain.ValueObject.ValueObject
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public Address(AddressProps addressProps)
        {
            Street = addressProps.Street;
            Number = addressProps.Number;
            Complement =addressProps.Complement;
            City = addressProps.City;
            State = addressProps.State;
            ZipCode = addressProps.ZipCode;

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return Complement;
            yield return City;
            yield return State;
            yield return ZipCode;
        }
    }
}
