using Moq;
using Payment.Domain.Entity;
using Payment.Repository.Interface;
using Payment.UseCase.ProcessPayment;
using static Payment.Domain.Entity.TransactionEntity;

namespace MonolithTests.Payment
{
    public class ProcessPaymentUseCaseTest
    {
        private ProcessPaymentInputDto saveInputOne;
        private ProcessPaymentInputDto saveInputTwo;
        private TransactionEntity entityOne;
        private TransactionEntity entityTwo;
        public ProcessPaymentUseCaseTest()
        {
            var transactionProps = new TransactionProps
            {
                Order_Id = Guid.NewGuid().ToString(),
                Amount = 100,
                Status = "approved"
            };

            saveInputOne = new ProcessPaymentInputDto
            {
                Amount = transactionProps.Amount,
                Order_ID = transactionProps.Order_Id,
            };

            entityOne = new TransactionEntity(transactionProps);

            transactionProps = new TransactionProps
            {
                Order_Id = Guid.NewGuid().ToString(),
                Amount = 50,
                Status = "declined"
            };

            saveInputTwo = new ProcessPaymentInputDto
            {
                Amount = transactionProps.Amount,
                Order_ID = transactionProps.Order_Id,
            };

            entityTwo = new TransactionEntity(transactionProps);
        }

        [Fact]
        public async Task ShouldApproveATransaction()
        {
            var paymentRepository = new Mock<IPaymentRepository>();
            var useCase = new ProcessPaymentUseCase(paymentRepository.Object);

            paymentRepository
                .Setup(p => p.Save(It.IsAny<TransactionEntity>()))
                .Returns(() => Task.FromResult(entityOne));
            

            var response = await useCase.Execute(saveInputOne);

            Assert.NotNull(response);
            Assert.Equal(response.Transaction_Id, entityOne._id.GetId());
            Assert.Equal(response.Order_Id, entityOne.Order_Id);
            Assert.Equal(response.Amount, entityOne.Amount);
            Assert.Equal(response.Status,"approved");
            Assert.StrictEqual(response.CreatedAt, entityOne.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, entityOne.UpdatedAt);
            
        }

        [Fact]
        public async Task ShouldDeclineATransaction()
        {
            var paymentRepository = new Mock<IPaymentRepository>();
            var useCase = new ProcessPaymentUseCase(paymentRepository.Object);

            paymentRepository
                .Setup(p => p.Save(It.IsAny<TransactionEntity>()))
                .Returns(() => Task.FromResult(entityTwo));


            var response = await useCase.Execute(saveInputTwo);

            Assert.NotNull(response);
            Assert.Equal(response.Transaction_Id, entityTwo._id.GetId());
            Assert.Equal(response.Order_Id, entityTwo.Order_Id);
            Assert.Equal(response.Amount, entityTwo.Amount);
            Assert.Equal(response.Status, "declined");
            Assert.StrictEqual(response.CreatedAt, entityTwo.CreatedAt);
            Assert.StrictEqual(response.UpdatedAt, entityTwo.UpdatedAt);

        }
    }
}
