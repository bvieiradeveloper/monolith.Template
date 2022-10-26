using InfraStructure.Model.StoreCatalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.Repository.Interface
{
    public interface IProductRepository
    {
        Task<ProductModel> Find(string productId);
        Task<IList<ProductModel>> FindAll();
    }
}
