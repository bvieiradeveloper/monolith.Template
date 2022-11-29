using Checkout.Domain.Entity;

namespace Checkout.Repository.Interface
{
    public interface ICheckoutRepository
    {
        Task<int> AddOrder(Order order);
    }
}
