using InfraStructure.Model;
using Product.Adm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.Repository.ProductRepository.Interface
{
    public interface IProductRepository
    {
        Task Add(ProductModel productEntity);
        Task<ProductModel> Find(string id);
    }
}
