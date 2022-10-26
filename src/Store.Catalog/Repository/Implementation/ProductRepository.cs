using InfraStructure.Context;
using InfraStructure.Model.StoreCatalog;
using Microsoft.EntityFrameworkCore;
using Store.Catalog.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Catalog.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {

        private readonly SharedContext _sharedContext;
        public ProductRepository(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }
        public async Task<ProductModel> Find(string productId)
        {
           return await _sharedContext.ProductsCatalog.FindAsync(productId);
        }

        public async Task<IList<ProductModel>> FindAll()
        {
            return await _sharedContext.ProductsCatalog.ToListAsync();
        }
    }
}
