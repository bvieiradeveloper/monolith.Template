using InfraStructure.Context;
using InfraStructure.Model;
using InfraStructure.Model.ProductAdm;
using Product.Adm.Domain.Entity;
using Product.Adm.Repository.ProductRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Adm.Repository.ProductRepository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly SharedContext _sharedContext;
        public ProductRepository(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }
        public async Task Add(ProductModel productEntity)
        {
            try
            {
                _sharedContext.ProductsAdm.Add(productEntity);
                await _sharedContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
      
        }

        public async Task<ProductModel> Find(string id)
        {
            try
            {
                return await _sharedContext.ProductsAdm.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
