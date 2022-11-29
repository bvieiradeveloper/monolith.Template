using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MonolithTests")]
namespace Store.Catalog.UseCase
{
    internal class FindProductInputDto
    {
        public string Id { get; set; }
    }

    internal class FindProductOutputDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long SalesPrice { get; set; }
    }
}
