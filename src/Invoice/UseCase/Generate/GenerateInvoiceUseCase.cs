using _Shared.Domain.ValueObject;
using InfraStructure.Context;
using Invoice.Domain.Entity;
using Invoice.Domain.ValueObject;
using Invoice.Repository.Interface;

namespace Invoice.UseCase.Generate
{
    public class GenerateInvoiceUseCase
    {
        readonly IInvoiceRepository _invoiceRepository;
        public GenerateInvoiceUseCase(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GenerateInvoiceOutputDto> Execute(GenerateInvoiceInputDto input)
        {
            var productProps = input.Items.Select(i => new ProductProps
            {
                Id = new Id(i.Id),
                Name = i.Name,
                Price = i.Price,
            });

            var address = new AddressProps
            {
                Street = input.Street,
                City = input.City,
                Number = input.Number,
                State = input.State,
                ZipCode = input.ZipCode,
                Complement = input.Complement,
            };

            var invoiceProps = new InvoiceProps
            {
                Id = new Id(input.Id),
                Name = input.Name,
                Document = input.Document,
                Address = new Address(address),
                Items = productProps.Select(p => new ProductEntity(p)).ToList(),

            };

            var response = await _invoiceRepository.Generate(new InvoiceEntity(invoiceProps));

            return new GenerateInvoiceOutputDto
            {
                Id = response._id.GetId(),
                Name = response.Name,
                Document = response.Document,
                Street = response.Address.Street,
                City = response.Address.City,
                Number = response.Address.Number,
                State = response.Address.State,
                ZipCode = response.Address.ZipCode,
                Complement = response.Address.Complement,
                Items = response.Items.Select(i => new GenerateInvoiceProductInputDto { Id = i._id.GetId(), Name = i.Name, Price = i.Price }).ToList(),
                Total = response.Total(),
            };
        }
    }
}
