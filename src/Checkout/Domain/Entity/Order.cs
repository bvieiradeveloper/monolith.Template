using _Shared.Domain.Entity;
using _Shared.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Domain.Entity
{
    public class OrderProps
    {
        public Id? Id { get; init; }
        public Client Client { get; init; }
        public IList<Product> Products { get; init; }
        public string? Status { get; init; }
        public DateTime? CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
    public class Order : BaseEntity
    {
        public Client Client { get; private set; }
        public IList<Product> Products { get; private set; }
        public string? Status { get; private set; }
        public Order(OrderProps props ) : base(props.Id, props.CreatedAt, props.UpdatedAt)
        {
            Client = props.Client;
            Products = props.Products;
            Status = props.Status ?? "pending";
        }

        public decimal Total()
        {
            return Products.Sum(p => p.SalesPrice);
        }

        public void Approve()
        {
            Status = "approved";
        }
    }
}
