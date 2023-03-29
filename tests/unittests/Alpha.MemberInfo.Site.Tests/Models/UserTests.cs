namespace Alpha.MemberInfo.Site.Tests.Models
{
    using System;
    using NSubstitute;
    using Site.Models;
    using Sunplus.Agent.Model.Entities;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class UserTests
    {
        [Fact]
        public void Constructor_Always_GetUserDataFromSession()
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.CustId.Returns(1);
            userSession.UserName.Returns("username");
            userSession.RoleId.Returns((byte)2);
            userSession.Language.Returns("en-US");
            userSession.SubAccId.Returns(0);
            userSession.UserLoginIp.Returns("192.168.1.1");
            userSession.LangKey.Returns("E");

            // Act
            var user = new User(userSession);

            // Assert
            Assert.True(
                user.Id == 1
                && user.Name == "username"
                && user.LevelId == 2
                && user.Language == "en-US"
                && user.SubAccId == 0
                && user.IP == "192.168.1.1"
                && user.LanguageKey == "E");
        }

        [Fact]
        public void GetHideProduct_Always_ReturnsCorrectStatus()
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.GetHideProduct(Arg.Any<int>()).Returns(true);
            var user = new User(userSession);
            var productId = 1;
            var expectedHide = true;

            // Act
            var actualHide = user.GetHideProduct(productId);

            // Assert
            Assert.Equal(expectedHide, actualHide);
        }

        [Fact]
        public void GetSyncedProduct_Always_ReturnsCorrectStatus()
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.GetSyncedProduct(Arg.Any<int>()).Returns(true);
            var user = new User(userSession);
            var productId = 1;
            var expectedSync = true;

            // Act
            var actualSync = user.GetSyncedProduct(productId);

            // Assert
            Assert.Equal(expectedSync, actualSync);
        }

        [Fact]
        public void CanUpdateAccount_Always_ExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            var userSession = Substitute.For<IUserSession>();
            userSession.SubPageAccess.Returns(new PageAccess { UpdateAccount = expectedValue });
            var user = new User(userSession);

            //// Act
            var act = user.CanUpdateAccount;

            //// Assert
            Assert.Equal(expectedValue, act);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanViewBetLists_Always_ReturnsExpectedValue(bool expected)
        {
            // Arrange
            var pageAccess = new PageAccess();
            pageAccess.SetPermission(PageAccessType.ViewBetLists, expected);
            var userSession = Substitute.For<IUserSession>();
            userSession.SubPageAccess.Returns(pageAccess);
            var user = new User(userSession);

            // Act
            var actual = user.CanViewBetLists;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsAllowedTotalBalanceTransfer_Always_ReturnsExpectedValue(bool expected)
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.IsAllowedTotalBalanceTransfer.Returns(expected);
            var user = new User(userSession);

            // Act
            var actual = user.IsAllowedTotalBalanceTransfer;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanTransfer_Always_ReturnsExpectedValue(bool expected)
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.SubPageAccess.Returns(new PageAccess { Transfer = expected });
            var user = new User(userSession);

            // Act
            var actual = user.CanTransfer;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LimitedDateToShowInfo_NoLimit_ReturnsExpectedValue()
        {
            // Arrange
            var expected = DateTime.MinValue;

            var userSession = Substitute.For<IUserSession>();
            userSession.GetProperty<DateTime>(Arg.Any<string>()).Returns(expected);
            var user = new User(userSession);

            // Act
            var actual = user.LimitedDateToShowInfo;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void LimitedDateToShowInfo_Limit_ReturnsExpectedValue()
        {
            // Arrange
            var expected = new DateTime(2018, 8, 8);

            var userSession = Substitute.For<IUserSession>();
            userSession.GetProperty<DateTime>(Arg.Any<string>()).Returns(expected);
            var user = new User(userSession);

            // Act
            var actual = user.LimitedDateToShowInfo;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsAdminCanViewVNIP_Always_ReturnsExpectedValue(bool expected)
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.IsAdminCanViewVNIP.Returns(expected);
            var user = new User(userSession);

            // Act
            var actual = user.IsAdminCanViewVNIP;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}