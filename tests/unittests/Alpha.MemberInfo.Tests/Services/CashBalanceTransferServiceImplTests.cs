using Alpha.MemberInfo.Criteria.Transfer;
using Alpha.MemberInfo.Models.Transfer;
using Alpha.MemberInfo.Services;
using Alpha.MemberInfo.Tests.Fixtures;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace Alpha.MemberInfo.Tests.Services
{
    [Trait("Category", "Transfer")]
    public class CashBalanceTransferServiceImplTests
    {
        private readonly CashBalanceTransferServiceImpl serviceImpl;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;

        public CashBalanceTransferServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;

            var dynamicRepository = Substitute.For<IDynamicRepository>();

            dynamicRepository.GetAsync<CashBalanceTransfer>(Arg.Any<GetCashBalanceTransferCriteria>())
                .Returns(new CashBalanceTransfer
                {
                    CustId = 1176083,
                    UserName = "master1",
                    RoleId = 3,
                    UplineCashBalance = 67599433.9247M/*,
                    CashBalance = 1101240.3283M,
                    Outstanding = 342552.00M,
                    GivenCredit = 900000002.00M*/
                });

            serviceImpl = new CashBalanceTransferServiceImpl(dynamicRepository);
        }

        [Fact]
        public void GetCashBalanceTransferAsync_ValidCustId_ReturnsDataFromRepository()
        {
            // Arrange
            var param = anyValueCreatorFixture.Create<int>();
            var expectedData = memberRequests;

            // Act
            var actualData = serviceImpl.GetCashBalanceTransferAsync(param);

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }
    }
}
