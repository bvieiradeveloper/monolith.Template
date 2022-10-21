using Product.Adm.Domain.Entity;

namespace Product.Adm.Gateway.Interface
{
    public interface ProductGateway
    {
        Task Add(ProductEntity product);
        Task<ProductEntity> Find(string id);
    }
}
