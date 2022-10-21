using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Model
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
