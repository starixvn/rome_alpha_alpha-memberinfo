namespace Alpha.MemberInfo.Site.Tests.Requests.CreditBalance
{
    using System;
    using MemberInfo.Constants;
    using MemberInfo.Models;
    using NSubstitute;
    using Site.Requests.CreditBalance;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CreditBalanceQueryTests
    {
        private readonly IUser user;

        public CreditBalanceQueryTests()
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
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.True(
                message.LoggedInUserId == user.LoggedInUserId
                && message.SubAccId == user.SubAccId
                && message.ActorId == user.Id
                && message.ActorLevel.Value == user.LevelId
                && message.CanUpdateAccount == user.CanUpdateAccount
                && message.IsAllDayBalTransfer == user.IsAllDayBalTransfer
                && message.IsAllowedTotalBalanceTransfer == user.IsAllowedTotalBalanceTransfer
                && message.CanTransfer == user.CanTransfer
                && message.CanViewBetLists == user.CanViewBetLists
                && message.LimitedDateToShowInfo == user.LimitedDateToShowInfo);
        }

        [Fact]
        public void PropertyCustId_IsZero_ReturnsActorId()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.Equal(message.ActorId, message.CustId);
        }

        [Fact]
        public void PropertyCustId_NoneZero_ReturnsCustId()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.CustId = 1176070;

            // Act & Assert
            Assert.Equal(1176070, message.CustId);
        }

        [Fact]
        public void PropertyCustLevelId_IsZero_ReturnsActorLevelValue()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.Equal(message.ActorLevel.Value, message.CustLevelId);
        }

        [Fact]
        public void PropertyCustLevelId_NoneZero_ReturnsCustLevelId()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.CustLevelId = 3;

            // Act & Assert
            Assert.Equal(3, message.CustLevelId);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void PropertyCustLevel_ValidValue_ReturnsCorrectUserLevel(byte expected)
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.CustLevelId = expected;

            // Act & Assert
            Assert.Equal(expected, message.CustLevel.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("---")]
        public void PropertyUserName_Always_ReturnsCorrectValues(string expected)
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.UserName = expected;

            // Act & Assert
            Assert.Equal(expected, message.UserName);
        }

        [Fact]
        public void PropertyStatusId_IsNull_ReturnsOpen()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.Equal(StatusIdentity.Open, message.StatusId);
        }

        [Theory]
        [InlineData(StatusIdentity.All)]
        [InlineData(StatusIdentity.Open)]
        [InlineData(StatusIdentity.Closed)]
        [InlineData(StatusIdentity.Suspended)]
        [InlineData(StatusIdentity.Disabled)]
        public void PropertyStatusId_NoneNull_ReturnsCorrectValues(byte expected)
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.StatusId = expected;

            // Act & Assert
            Assert.Equal(expected, message.StatusId);
        }

        [Fact]
        public void PropertyPageIndex_IsZero_ReturnsOne()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.Equal(1, message.PageIndex);
        }

        [Fact]
        public void PropertyPageIndex_NoneZero_ReturnsCorrectValue()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.PageIndex = 100;

            // Act & Assert
            Assert.Equal(100, message.PageIndex);
        }

        [Fact]
        public void PropertyPageSize_IsZero_Returns500()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.Equal(500, message.PageSize);
        }

        [Fact]
        public void PropertyPageSize_NoneZero_ReturnsCorrectValue()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);
            message.PageSize = 10;

            // Act & Assert
            Assert.Equal(10, message.PageSize);
        }

        [Fact]
        public void IsValid_ValidCustLevel_ReturnsTrue()
        {
            // Arrange
            var message = new CreditBalanceQuery(user);

            // Act & Assert
            Assert.True(message.IsValid());
        }

        [Fact]
        public void IsValid_InvalidCustLevel_ReturnsFalse()
        {
            // Arrange
            user.LevelId.Returns((byte)2);
            var message = new CreditBalanceQuery(user);
            message.CustLevelId = 4;

            // Act & Assert
            Assert.False(message.IsValid());
        }
    }
}