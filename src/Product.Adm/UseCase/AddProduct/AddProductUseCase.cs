using InfraStructure.Model;
using InfraStructure.Model.ProductAdm;
using Product.Adm.Repository.ProductRepository.Interface;

namespace Product.Adm.UseCase.AddProduct
{
    public class AddProductUseCase

    {
        IProductRepository _productRepository;
        public AddProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<AddProductOutputDTO> Execute(AddProductInputDTO productProps)
        {
            var product = new ProductModel()
            {
                Id = productProps.id.GetId(),
                Description = productProps.Description,
                Name = productProps.Name,
                PurchasePrice = productProps.PurchasePrice,
                Stock = productProps.Stock,
            };
            await _productRepository.Add(product);


            return new AddProductOutputDTO
            {
                id = new _Shared.Domain.ValueObject.Id(product.Id),
                Name = product.Name,
                CreatedAt = product.CreatedAt,
                Description = product.Description,
                PurchasePrice = product.PurchasePrice,
                Stock = product.Stock,
                UpdatedAt = product.UpdatedAt,   
            };
        }
    }
}
