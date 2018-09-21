namespace Alpha.MemberInfo.Tests.Services.Transfer
{
    using System.Threading.Tasks;
    using AppResources;
    using Fanex.Data.Repository;
    using Fixtures;
    using MemberInfo.Criteria.Transfer;
    using MemberInfo.Services.Transfer;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "Transfer")]
    [Collection("AnyValueCreatorCollection")]
    public class CashBalanceTransferServiceImplTests
    {
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly IDynamicRepository dynamicRepository;
        private readonly TransferService serviceImpl;

        public CashBalanceTransferServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;

            dynamicRepository = Substitute.For<IDynamicRepository>();
            serviceImpl = new TransferServiceImpl(dynamicRepository);
        }

        [Fact]
        public async Task GetAllowTransfer_NoData_ReturnAllowTransferText()
        {
            //// Arrange
            dynamicRepository.ExecuteAsync(Arg.Any<GetTransferOptionCriteria>()).Returns(Task.CompletedTask);

            //// Act
            var act = await serviceImpl.GetAllowTransfer(263);

            //// Assert
            Assert.Equal(Lang.AllowTransfer, act);
        }

        [Fact]
        public async Task GetAllowTransfer_Daily_ReturnDailyText()
        {
            //// Arrange
            dynamicRepository.ExecuteAsync(Arg.Any<GetTransferOptionCriteria>()).Returns(x =>
            {
                ((GetTransferOptionCriteria)x[0]).TransferOption = "00000000";

                return Task.CompletedTask;
            });

            var expected = $"{Lang.AllowTransfer} {Lang.Daily}";

            //// Act
            var act = await serviceImpl.GetAllowTransfer(263);

            //// Assert
            Assert.Equal(expected, act);
        }

        [Theory]
        [InlineData("10000001", "Sunday")]
        [InlineData("11000001", "Monday - Sunday")]
        [InlineData("01000001", "Monday")]
        public async Task GetAllowTransfer_HasAllowTransfer_ReturnCorrectText(string transferOption, string text)
        {
            //// Arrange
            dynamicRepository.ExecuteAsync(Arg.Any<GetTransferOptionCriteria>()).Returns(x =>
            {
                ((GetTransferOptionCriteria)x[0]).TransferOption = transferOption;

                return Task.CompletedTask;
            });

            var expected = $"{Lang.AllowTransfer} {text}";

            //// Act
            var act = await serviceImpl.GetAllowTransfer(263);

            //// Assert
            Assert.Equal(expected, act);
        }
    }
}
