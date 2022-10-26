using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace InfraStructure.Model.ProductAdm
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
        public long PurchasePrice { get; set; }
        [NotNull]
        public long Stock { get;set; }
        public DateTime CreatedAt{ get; set; }  
        public DateTime UpdatedAt{ get; set; }
    }
}
