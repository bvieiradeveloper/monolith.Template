﻿using _Shared.Domain.ValueObject;
using _Shared.Usecase.Interface;
using InfraStructure.Context;
using Invoice.Domain.Entity;
using Invoice.Domain.ValueObject;
using Invoice.Facade.Implementation;
using Invoice.Factory;
using Invoice.UseCase.Find;
using Invoice.UseCase.Generate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithTests.Invoice
{
    public class InvoiceFacadeTest
    {
        SharedContext _sharedContext;
        private InvoiceEntity _invoice;
        private List<ProductEntity> _products = new();
        private Address _address;
        public InvoiceFacadeTest()
        {
            _sharedContext = InMemoryDb.InitDb();

            List<ProductProps> inputProduct = new()
            {
                new ProductProps
                {
                    Id = new Id(Guid.NewGuid().ToString()),
                    Name = "Product 1",
                    Price = 500,
                },
                new ProductProps
                {
                    Id = new Id(Guid.NewGuid().ToString()),
                    Name = "Product 2",
                    Price = 750,
                }
            };

            _products.AddRange(inputProduct.Select(props => new ProductEntity(props)));

            AddressProps _addressProps = new()
            {
                Street = "Street 1",
                Number = "123",
                Complement = "Next to drugstore",
                City = "City 1",
                State = "SO",
                ZipCode = "123654987",
            };

            _address = new(_addressProps);

            InvoiceProps _invoiceProps = new()
            {
                Id = new Id(Guid.NewGuid().ToString()),
                Name = "Invoice 1",
                Document = "1234567890",
                Address = _address,
                Items = _products,
            };

            _invoice = new(_invoiceProps);
        }

        [Fact]
        public async Task ShouldGenerateAInvoice()
        {
            InvoiceFacade invoiceFacade = FacadeFactory.Create(_sharedContext);


            var response = await invoiceFacade.Generate(new GenerateInvoiceInputDto
            {
                Id = _invoice._id.GetId(),
                Name = _invoice.Name,
                Document = _invoice.Document,
                Street = _invoice.Address.Street,
                Number = _invoice.Address.Number,
                Complement = _invoice.Address.Complement,
                City = _invoice.Address.City,
                State = _invoice.Address.State,
                ZipCode = _invoice.Address.ZipCode,
                Items = _invoice.Items.Select(item => new GenerateInvoiceProductInputDto
                {
                    Id = item._id.GetId(),
                    Name = item.Name,
                    Price = item.Price,
                }).ToList(),
            });

            Assert.NotNull(response);

            Assert.NotNull(response.Id);
            Assert.Equal(_invoice.Name, response.Name);
            Assert.Equal(_invoice.Document, response.Document);
            Assert.Equal(_invoice.Address.Street, response.Street);
            Assert.Equal(_invoice.Address.ZipCode, response.ZipCode);
            Assert.Equal(_invoice.Address.City, response.City);
            Assert.Equal(_invoice.Address.State, response.State);
            Assert.Equal(_invoice.Address.Complement, response.Complement);
            Assert.Equal(_invoice.Address.Number, response.Number);
            Assert.Equal(_invoice.Total(), response.Total);

            Assert.Equal(2, response.Items.Count);

            Assert.Equal(_invoice.Items.First()._id.GetId(), response.Items.First().Id);
            Assert.Equal(_invoice.Items.First().Name, response.Items.First().Name);
            Assert.Equal(_invoice.Items.First().Price, response.Items.First().Price);

            Assert.Equal(_invoice.Items.Last()._id.GetId(), response.Items.Last().Id);
            Assert.Equal(_invoice.Items.Last().Name, response.Items.Last().Name);
            Assert.Equal(_invoice.Items.Last().Price, response.Items.Last().Price);

        }

        [Fact]
        public async Task ShouldFindAInvoice()
        {
            InvoiceFacade invoiceFacade = FacadeFactory.Create(_sharedContext);

            await invoiceFacade.Generate(new GenerateInvoiceInputDto
            {
                Id = _invoice._id.GetId(),
                Name = _invoice.Name,
                Document = _invoice.Document,
                Street = _invoice.Address.Street,
                Number = _invoice.Address.Number,
                Complement = _invoice.Address.Complement,
                City = _invoice.Address.City,
                State = _invoice.Address.State,
                ZipCode = _invoice.Address.ZipCode,
                Items = _invoice.Items.Select(item => new GenerateInvoiceProductInputDto
                {
                    Id = item._id.GetId(),
                    Name = item.Name,
                    Price = item.Price,
                }).ToList(),
            });

            var response = await invoiceFacade.FindInvoice(new FindInvoiceInputDto { Id = _invoice._id.GetId() });


            Assert.NotNull(response);

            Assert.NotNull(response.Id);
            Assert.Equal(_invoice.Name, response.Name);
            Assert.Equal(_invoice.Document, response.Document);
            Assert.Equal(_invoice.Address.Street, response.Address.Street);
            Assert.Equal(_invoice.Address.ZipCode, response.Address.ZipCode);
            Assert.Equal(_invoice.Address.City, response.Address.City);
            Assert.Equal(_invoice.Address.State, response.Address.State);
            Assert.Equal(_invoice.Address.Complement, response.Address.Complement);
            Assert.Equal(_invoice.Address.Number, response.Address.Number);
            Assert.Equal(_invoice.Total(), response.Total);

            Assert.Equal(2, response.Items.Count);

            Assert.Equal(_invoice.Items.First()._id.GetId(), response.Items.First().Id);
            Assert.Equal(_invoice.Items.First().Name, response.Items.First().Name);
            Assert.Equal(_invoice.Items.First().Price, response.Items.First().Price);

            Assert.Equal(_invoice.Items.Last()._id.GetId(), response.Items.Last().Id);
            Assert.Equal(_invoice.Items.Last().Name, response.Items.Last().Name);
            Assert.Equal(_invoice.Items.Last().Price, response.Items.Last().Price);
        }
    }
}
