using _Shared.Domain.ValueObject;
using Product.Adm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.UseCase.AddProduct
{
    public class AddProductInputDTO : AddProductInputDto{}

    public class AddProductOutputDTO : AddProductInputDto 
    {
        public DateTime CreatedAt  { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
