
using Store.Catalog.Facade.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.Facade.Interface
{
    public interface IProductStoreCatalogFacade
    {
        Task<FindProductOutputDto> Find(FindProductInputDto input);
        Task<FindAllProductsOutputDto> FindAll();
    }
}
