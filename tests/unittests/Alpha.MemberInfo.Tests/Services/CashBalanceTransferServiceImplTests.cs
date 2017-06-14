//using System;
//using System.Threading.Tasks;
//using Alpha.MemberInfo.Criteria.Transfer;
//using Alpha.MemberInfo.Models;
//using Alpha.MemberInfo.Models.Transfer;
//using Alpha.MemberInfo.Services;
//using Alpha.MemberInfo.Tests.Fixtures;
//using Fanex.Data.Repository;
//using KellermanSoftware.CompareNetObjects;
//using NSubstitute;
//using NSubstitute.ExceptionExtensions;
//using Xunit;

//namespace Alpha.MemberInfo.Tests.Services
//{
//    [Trait("Category", "Transfer")]
//    [Collection("AnyValueCreatorCollection")]
//    public class CashBalanceTransferServiceImplTests
//    {
//        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
//        private readonly IDynamicRepository dynamicRepository;
//        private readonly CashBalanceTransferService serviceImpl;

//        public CashBalanceTransferServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
//        {
//            this.anyValueCreatorFixture = anyValueCreatorFixture;

//            dynamicRepository = Substitute.For<IDynamicRepository>();
//            serviceImpl = new CashBalanceTransferServiceImpl(dynamicRepository);
//        }

//        [Fact]
//        public async Task GetCashBalanceTransferAsync_ValidCustId_ReturnsDataFromRepository()
//        {
//            // Arrange
//            var custId = 1176083;
//            var cashBalanceTransfer = new CashBalanceTransfer
//            {
//                CustId = custId,
//                UserName = "master1",
//                RoleId = 3,
//                UplineCashBalance = 67599433.9247M,
//                CashBalance = 1101240.3283M,
//                Outstanding = 342552.00M,
//                GivenCredit = 900000002.00M
//            };

//            dynamicRepository.GetAsync<CashBalanceTransfer>(Arg.Any<GetCashBalanceTransferCriteria>())
//                .Returns(cashBalanceTransfer);

//            var compareLogic = new CompareLogic();

//            // Act
//            var actualData = await serviceImpl.GetCashBalanceTransferAsync(custId);

//            // Assert
//            var result = compareLogic.Compare(cashBalanceTransfer, actualData);
//            Assert.True(result.AreEqual);
//        }

//        [Theory]
//        [InlineData(-1176083)]
//        [InlineData(0)]
//        [InlineData(1176070)]
//        public void GetCashBalanceTransferAsync_InValidCustId_ThrowInvalidOperationException(int custId)
//        {
//            // Arrange
//            dynamicRepository.GetAsync<CashBalanceTransfer>(new GetCashBalanceTransferCriteria(custId))
//                .Throws(new InvalidOperationException());

//            var cashBalanceTransferService = new CashBalanceTransferServiceImpl(dynamicRepository);

//            // Act & Assert
//            Assert.ThrowsAsync<InvalidOperationException>(async () => await cashBalanceTransferService.GetCashBalanceTransferAsync(custId));
//        }

//        [Fact]
//        public async Task UpdateCashBalanceTransferAsync_ValidParams_ReturnsTransferSuccessfully()
//        {
//            // Arrange
//            var expectedResult = new Result
//            {
//                Message = AppResources.Transfer.TransferSuccessfully
//            };

//            dynamicRepository.ExecuteAsync(Arg.Any<AddCashBalanceTransferCriteria>())
//                .Returns(Task.CompletedTask);

//            // Act
//            var actualData = await serviceImpl.UpdateCashBalanceTransferAsync(
//                1176070,
//                1176083,
//                1101240.32M,
//                string.Empty,
//                0,
//                "127.0.0.1",
//                "E");

//            // Assert
//            Assert.True(actualData.Code == expectedResult.Code &&
//                actualData.Message == expectedResult.Message);
//        }
//    }
//}
