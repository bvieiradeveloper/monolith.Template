
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InfraStructure.Model.StoreCatalog
{
    public class ProductModel
    {
        [Key]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public long SalePrice { get; set; }
        [NotNull]
        public DateTime CreatedAt { get; set; }
        [NotNull]
        public DateTime UpdatedAt { get; set; }
    }
}
