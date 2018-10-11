namespace Alpha.MemberInfo.Site.Tests.ViewModels.CreditBalance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MemberInfo.Models;
    using MemberInfo.Models.CreditBalance;
    using NSubstitute;
    using Site.Requests.CreditBalance;
    using Site.ViewModels.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CreditBalanceViewModelTests
    {
        private readonly IUser user;
        private readonly PagedCreditBalance pagedCreditBalances;
        private readonly CreditBalanceQuery message;

        public CreditBalanceViewModelTests()
        {
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");
            pagedCreditBalances = new PagedCreditBalance(1, 500, 1, new List<CreditBalanceItem> { creditBalanceItem });

            message = new CreditBalanceQuery(user);
        }

        [Fact]
        public void Constructor_InvalidQuery_AssignsEmptyViewModel()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);

            // Act & Assert
            Assert.True(
                viewModel.PagedCreditBalances.TotalItems == 0
                && viewModel.PagedCreditBalances.ShowPager == false
                && viewModel.PagedCreditBalances.CreditBalances.Any() == false
                && viewModel.TransferCondition == null);
        }

        [Fact]
        public void GeStatustBackgroundColor_Disabled_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                false,
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("disabled-bg", actual);
        }

        [Fact]
        public void GeStatustBackgroundColor_ClosedByAdmin_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                true,
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1")
            {
                ClosedReason = 1
            };

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("closed-byadmin", actual);
        }

        [Fact]
        public void GeStatustBackgroundColor_ClosedByUpline_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                true,
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1")
            {
                ClosedReason = 2
            };

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("closed-byupline", actual);
        }

        [Fact]
        public void GeStatustBackgroundColor_ClosedByWrongOTPSC_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                true,
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1")
            {
                ClosedReason = 3
            };

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("closed-wrongotp-sc", actual);
        }

        [Fact]
        public void GeStatustBackgroundColor_ClosedByWrongPwdOthers_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                true,
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1")
            {
                ClosedReason = 0
            };

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("closed-wrongpwd-others", actual);
        }

        [Fact]
        public void GeStatustBackgroundColor_Suspended_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                1,
                true,
                false,
                true,
                1176070,
                "master1",
                "---",
                "---",
                900000002,
                826715185,
                -20399.61m,
                -20399.61m,
                371662,
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("suspended-bg", actual);
        }

        [Fact]
        public void GeStatustBackgroundColor_Open_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.True(actual.Length == 0);
        }

        [Fact]
        public void GeStatustBackgroundColor_OpenEvent_ReturnsCorrectValue()
        {
            // Arrange
            var viewModel = new CreditBalanceViewModel(message);
            var creditBalanceItem = new CreditBalanceItem(
                2,
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
                new DateTime(2018, 8, 7, 4, 47, 0),
                "192.168.1.1");

            // Act
            var actual = viewModel.GeStatustBackgroundColor(creditBalanceItem);

            // Act & Assert
            Assert.Equal("even-row", actual);
        }
    }
}
