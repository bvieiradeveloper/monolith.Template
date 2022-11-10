using Invoice.Repository.Interface;

namespace Invoice.UseCase.Find
{
    public class FindInvoiceUseCase
    {
        readonly IInvoiceRepository _invoiceRepository;
        public FindInvoiceUseCase(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<FindInvoiceOutputDto> Execute(FindInvoiceInputDto input)
        {
            var response = await _invoiceRepository.Find(input.Id);

            return new FindInvoiceOutputDto
            {
                Id = response._id.GetId(),
                Name = response.Name,
                Document = response.Document,
                Address = new()
                {
                    Street = response.Address.Street,
                    City = response.Address.City,
                    Complement = response.Address.Complement,
                    Number = response.Address.Number,
                    State = response.Address.State,
                    ZipCode = response.Address.ZipCode,
                },
                Items = response.Items.Select(item => new FindInvoiceProductOutputDto
                {
                    Id = item._id.GetId(),
                    Name = item.Name,
                    Price = item.Price,
                }).ToList(),
                Total = response.Total(),
                CreatedAt = response.CreatedAt,
                UpdatedAt = response.UpdatedAt,
            };
        }
    }
}
