using System;
using System.Threading.Tasks;
using Alpha.MemberInfo.Criteria.Transfer;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Models.Transfer;
using Alpha.MemberInfo.Services;
using Alpha.MemberInfo.Tests.Fixtures;
using Fanex.Data.Repository;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Alpha.MemberInfo.Tests.Services
{
    [Trait("Category", "Transfer")]
    [Collection("AnyValueCreatorCollection")]
    public class CashBalanceTransferServiceImplTests
    {
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CashBalanceTransferService serviceImpl;

        public CashBalanceTransferServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;

            dynamicRepository = Substitute.For<IDynamicRepository>();
            serviceImpl = new CashBalanceTransferServiceImpl(dynamicRepository);
        }

        [Fact]
        public async Task GetCashBalanceTransferAsync_ValidCustId_ReturnsDataFromRepository()
        {
            // Arrange
            var custId = 1176083;
            var cashBalanceTransfer = new CashBalanceTransfer(
                custId,
                "master1",
                3,
                67599433.9247M,
                1101240.3283M,
                342552.00M,
                900000002.00M);

            dynamicRepository.GetAsync<CashBalanceTransfer>(Arg.Any<GetCashBalanceTransferCriteria>())
                .Returns(cashBalanceTransfer);

            var compareLogic = new CompareLogic();

            // Act
            var actualData = await serviceImpl.GetCashBalanceTransferAsync(custId);

            // Assert
            var result = compareLogic.Compare(cashBalanceTransfer, actualData);
            Assert.True(result.AreEqual);
        }

        [Theory]
        [InlineData(-1176083)]
        [InlineData(0)]
        [InlineData(1176070)]
        public void GetCashBalanceTransferAsync_InValidCustId_ThrowInvalidOperationException(int custId)
        {
            // Arrange
            dynamicRepository.GetAsync<CashBalanceTransfer>(new GetCashBalanceTransferCriteria(custId))
                .Throws(new InvalidOperationException());

            var cashBalanceTransferService = new CashBalanceTransferServiceImpl(dynamicRepository);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await cashBalanceTransferService.GetCashBalanceTransferAsync(custId));
        }

        [Fact]
        public async Task AddCashBalanceTransferAsync_ValidParams_ReturnsTransferSuccessfully()
        {
            // Arrange
            var expectedResult = new Result
            {
                Message = AppResources.Transfer.TransferSuccessfully
            };

            dynamicRepository.ExecuteAsync(Arg.Any<AddCashBalanceTransferCriteria>())
                .Returns(Task.CompletedTask);

            // Act
            var actualData = await serviceImpl.AddCashBalanceTransferAsync(
                1176070,
                1176083,
                1101240.32M,
                1176070,
                "Admin1",
                0,
                "127.0.0.1",
                "E");

            // Assert
            Assert.True(actualData.Code == expectedResult.Code &&
                actualData.Message == expectedResult.Message);
        }

        [Theory]
        [InlineData(-1176070, 1176083, "127.0.0.1")]
        [InlineData(1176070, -1176083, "127.0.0.1")]
        [InlineData(1176070, 1176083, null)]
        public async Task AddCashBalanceTransferAsync_InvalidParams_ReturnsErrorCodeFromSP(int uplineId, int custId, string ip)
        {
            // Arrange
            var expectedResult = new Result();

            var criteria = new AddCashBalanceTransferCriteria(uplineId, custId, 0, uplineId, 0, ip, "E");

            dynamicRepository.ExecuteAsync(criteria)
                .Returns(Task.CompletedTask);

            // Act
            var actualData = await serviceImpl.AddCashBalanceTransferAsync(
                criteria.FromCustId,
                criteria.ToCustId,
                criteria.Amount,
                criteria.Creator,
                "Admin1",
                criteria.SubAccId,
                criteria.IP,
                criteria.Language);

            // Assert
            Assert.True(actualData.Code == expectedResult.Code);
        }
    }
}
