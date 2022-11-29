using InfraStructure.Context;
using Payment.Facade;
using static Payment.Domain.Entity.TransactionEntity;

namespace MonolithTests.Payment
{
    public class PaymentFacadeTest
    {
        readonly SharedContext _sharedContext;
        readonly ProcessPaymentInputDto saveInput;

        public PaymentFacadeTest()
        {
            _sharedContext = InMemoryDb.InitDb();

            var transactionProps = new TransactionProps
            {
                Order_Id = Guid.NewGuid().ToString(),
                Amount = 100,
                Status = "approved"
            };

            saveInput = new ProcessPaymentInputDto
            {
                Amount = transactionProps.Amount,
                Order_ID = transactionProps.Order_Id,
            };
        }

        [Fact]
        public async Task ShouldCreateATransaction()
        {
            var facade = PaymentFacadeFactory.Create(_sharedContext);

            var response = await facade.Process(saveInput);

            Assert.NotNull(response);
            Assert.NotNull(response.Transaction_Id);
            Assert.Equal(response.Order_Id, saveInput.Order_ID);
            Assert.Equal(response.Amount, saveInput.Amount);
            Assert.Equal(response.Status, "approved");
        }
    }
}
