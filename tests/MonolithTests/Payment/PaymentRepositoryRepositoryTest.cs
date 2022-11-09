using InfraStructure.Context;
using Payment.Domain.Entity;
using Payment.Repository.Implementation;
using Payment.UseCase.ProcessPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payment.Domain.Entity.TransactionEntity;

namespace MonolithTests.Payment
{
    public class PaymentRepositoryRepositoryTest
    {

        private TransactionEntity entity;
        SharedContext _sharedContext;
        public PaymentRepositoryRepositoryTest()
        {
             _sharedContext = InMemoryDb.InitDb();

            var transactionProps = new TransactionProps
            {
                Order_Id = Guid.NewGuid().ToString(),
                Amount = 100,
            };

            entity = new TransactionEntity(transactionProps);
        }

        [Fact]
        public async Task ShouldSaveATransaction()
        {
            var repository = new PaymentRepository(_sharedContext);

            await repository.Save(entity);
            var response =  await _sharedContext.Transactions.FindAsync(entity._id.GetId());

            Assert.NotNull(response);
            Assert.Equal(response.Id, entity._id.GetId());
            Assert.Equal(response.Order_Id, entity.Order_Id);
            Assert.Equal(response.Amount, entity.Amount);
            Assert.Equal(response.Status, entity.Status);
            Assert.StrictEqual(response.CreatedAt, entity.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, entity.UpdatedAt);
        }
    }
}
