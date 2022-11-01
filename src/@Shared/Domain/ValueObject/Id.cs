namespace _Shared.Domain.ValueObject
{
    public class Id : ValueObject
    {
        private string _id;

        public Id()
        {
            _id =  Guid.NewGuid().ToString();
        }

        public Id(string? id)
        {
            _id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
        }

        public string GetId()
        {
            return _id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _id;
        }
    }
}
