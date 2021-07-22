using System;
using Alpha.MemberInfo.Site.Helpers;
using Xunit;

namespace Alpha.MemberInfo.Site.Tests.Helpers
{
    [Trait("Category", "Site.Helpers")]
    public class BackgroundColorHelperTests
    {
        [Fact]
        public void GetClassNameByAccountStatus_Disabled_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = false;
            bool closed = false;
            byte closedReason = 0;
            bool suspended = false;
            byte locked = 0;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.Equal("disabled-bg", actual);
        }

        [Fact]
        public void GetClassNameByAccountStatus_ClosedByAdmin_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = true;
            bool closed = true;
            byte closedReason = 1;
            bool suspended = false;
            byte locked = 0;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.Equal("closed-byadmin", actual);
        }

        [Fact]
        public void GetClassNameByAccountStatus_ClosedByUpline_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = true;
            bool closed = true;
            byte closedReason = 2;
            bool suspended = false;
            byte locked = 0;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.Equal("closed-byupline", actual);
        }

        [Fact]
        public void GetClassNameByAccountStatus_LockedFalseOTPSC_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = true;
            bool closed = false;
            byte closedReason = 0;
            bool suspended = false;
            byte locked = 1;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.Equal("locked-falseOTPSC", actual);
        }

        [Fact]
        public void GetClassNameByAccountStatus_LockedFalsePassword_ReturnsCorrectValue()
        {
            bool enabled = true;
            bool closed = false;
            byte closedReason = 0;
            bool suspended = false;
            byte locked = 2;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.Equal("locked-falsePassword", actual);
        }

        [Fact]
        public void GetClassNameByAccountStatus_Suspended_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = true;
            bool closed = false;
            byte closedReason = 0;
            bool suspended = true;
            byte locked = 0;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.Equal("suspended-bg", actual);
        }

        [Fact]
        public void GetClassNameByAccountStatus_Open_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = true;
            bool closed = false;
            byte closedReason = 0;
            bool suspended = false;
            byte locked = 0;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked);

            // Act & Assert
            Assert.True(actual.Length == 0);
        }

        [Fact]
        public void GetClassNameByAccountStatus_OpenEven_ReturnsCorrectValue()
        {
            // Arrange
            bool enabled = true;
            bool closed = false;
            byte closedReason = 0;
            bool suspended = false;
            byte locked = 0;

            // Act
            var actual = BackgroundColorHelper.GetClassNameByAccountStatus(
                enabled,
                closed,
                closedReason,
                suspended,
                locked,
                2);

            // Act & Assert
            Assert.Equal("even-row", actual);
        }
    }
}
