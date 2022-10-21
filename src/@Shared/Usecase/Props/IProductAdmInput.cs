using _Shared.Domain.ValueObject;
using _Shared.Usecase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Shared.Usecase.Props
{
    public interface IProductAdmInput
    {
        public Id? id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PurchasePrice { get; set; }
        public long Stock { get; set; }
    }
}
