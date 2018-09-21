namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using MediatR;
    using MemberInfo.Models;
    using NSubstitute;
    using Site.Controllers;
    using Site.Requests.CreditBalance;
    using Site.ViewModels.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CreditBalanceControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUser user;
        private readonly CreditBalanceController controller;

        public CreditBalanceControllerTests()
        {
            mediator = Substitute.For<IMediator>();

            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)4);
            user.CanUpdateAccount.Returns(true);
            user.IsAllDayBalTransfer.Returns(true);
            user.IsAllowedTotalBalanceTransfer.Returns(true);
            user.CanTransfer.Returns(true);
            user.CanViewBetLists.Returns(true);
            user.LimitedDateToShowInfo.Returns(DateTime.MinValue);

            controller = new CreditBalanceController(mediator);
        }

        [Fact]
        public async Task Index_Always_CallsMediatorCreditBalanceQuery()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act
            await controller.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public async Task Index_InvalidQuery_ReturnsEmptyViewModel()
        {
            // Arrange
            user.LevelId.Returns((byte)2);
            var message = new CreditBalanceQuery(user);
            message.CustLevelId = 4;

            // Act
            var actualViewResult = await controller.Index(message) as ViewResult;
            var actualViewModel = actualViewResult.Model as CreditBalanceViewModel;

            // Assert
            Assert.True(
                actualViewModel.PagedCreditBalances.TotalItems == 0
                && actualViewModel.PagedCreditBalances.ShowPager == false
                && actualViewModel.PagedCreditBalances.CreditBalances.Any() == false);
        }
    }
}
