using Checkout.Domain.Entity;
using Checkout.Repository.Interface;
using InfraStructure.Context;
using InfraStructure.Model.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Repository.Implementation
{
    public class CheckoutRepository : ICheckoutRepository
    {
        readonly SharedContext _sharedContext;
        public CheckoutRepository(SharedContext sharedContext)
        {
            _sharedContext = sharedContext;
        }
        public async Task<int> AddOrder(Order order)
        {
            await _sharedContext.CheckoutClients.AddAsync(new()
            {
                Name = order.Client.Name,
                Email = order.Client.Email,
                Document = order.Client.Document,
                Street = order.Client.Street,
                City = order.Client.City,
                Complement = order.Client.Complement,
                ZipCode = order.Client.ZipCode,
                Number = order.Client.Number,
                State = order.Client.State,
                CreatedAt = order.Client.CreatedAt,
                UpdatedAt = order.Client.UpdatedAt,
                Products = order.Products.Select(p => new InfraStructure.Model.Checkout.Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    SalesPrice = p.SalesPrice,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                }).ToList()
            });;
            return await _sharedContext.SaveChangesAsync();
        }
    }
}
