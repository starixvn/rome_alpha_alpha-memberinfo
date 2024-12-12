namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Services;
    using MemberInfo.Models;
    using MemberInfo.Models.CreditBalance;
    using NSubstitute;
    using Params;
    using Services.CreditBalance;
    using Services.Transfer;
    using Site.Handlers;
    using Site.Requests.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CreditBalanceHandlerTests
    {
        private readonly IUser user;
        private readonly CreditBalanceService creditBalanceService;
        private readonly TransferService transferService;
        private readonly CreditBalanceHandler handler;
        private readonly IWinlossService winlossService;

        public CreditBalanceHandlerTests()
        {
            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)4);
            user.CanUpdateAccount.Returns(true);
            user.IsAllowedTotalBalanceTransfer.Returns(true);
            user.CanTransfer.Returns(true);
            user.CanViewBetLists.Returns(true);
            user.LimitedDateToShowInfo.Returns(DateTime.MinValue);

            creditBalanceService = Substitute.For<CreditBalanceService>();
            transferService = Substitute.For<TransferService>();
            winlossService = Substitute.For<IWinlossService>();
            handler = new CreditBalanceHandler(creditBalanceService, transferService, winlossService);
        }

        [Fact]
        public async Task HandlerCreditBalanceQuery_SuperLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                false,
                false,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1",
                false);
            var pagedCreditBalances = new PagedCreditBalance(1, 500, 1, new List<CreditBalanceItem> { creditBalanceItem });

            creditBalanceService.GetPagedCreditBalancesAsync(Arg.Any<CreditBalanceParam>()).Returns(pagedCreditBalances);

            // Act
            var actual = await handler.Handle(message);

            // Assert
            Assert.True(
                actual.PagedCreditBalances.TotalItems == 1
                && !actual.PagedCreditBalances.ShowPager
                && actual.PagedCreditBalances.CreditBalances.Any()
                && actual.TransferCondition == null);
        }

        [Fact]
        public async Task HandlerCreditBalanceQuery_AgentLevel_ReturnsCorrectViewModel()
        {
            // Arrange
            user.LevelId.Returns((byte)2);
            var message = new CreditBalanceQuery(user);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                false,
                false,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                0,
                new DateTime(2018, 8, 7, 4, 47, 0),
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1",
                false);
            var pagedCreditBalances = new PagedCreditBalance(1, 500, 1, new List<CreditBalanceItem> { creditBalanceItem });

            creditBalanceService.GetPagedCreditBalancesAsync(Arg.Any<CreditBalanceParam>()).Returns(pagedCreditBalances);
            transferService.GetAllowTransfer(Arg.Any<int>()).Returns("You are allowed to transfer on Daily");

            // Act
            var actual = await handler.Handle(message);

            // Assert
            Assert.True(
                actual.PagedCreditBalances.TotalItems == 1
                && !actual.PagedCreditBalances.ShowPager
                && actual.PagedCreditBalances.CreditBalances.Any()
                && actual.TransferCondition == "You are allowed to transfer on Daily");
        }
    }
}
