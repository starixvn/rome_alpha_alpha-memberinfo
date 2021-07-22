namespace Alpha.MemberInfo.Tests.Models.IndirectLoginAccount
{
    using System;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class IndirectLoginAccountItemTests
    {
        [Fact]
        public void DefaultConstructor_Always_AssignsDefaultValue()
        {
            // Arrange & Act
            var model = new IndirectLoginAccountItem();

            // Assert
            Assert.True(
                model.CustId == 0
                && model.SubAccId == 0
                && model.Username == null
                && model.Level == 0
                && !model.Enabled
                && !model.Closed
                && !model.Suspended
                && model.LastVerifyPwd == null
                && model.LastChangedPwdDate == null);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public void CustLevel_Always_ReturnsCorrectValue(byte level)
        {
            // Arrange
            var model = new IndirectLoginAccountItem(1176083, 91726, "master1", level, true, false, false, DateTime.Today, DateTime.Today.AddDays(1), 0);

            // Act
            var actual = model.CustLevel;

            // Assert
            Assert.Equal(level, actual.Value);
        }

        [Theory]
        [InlineData(4, "Super")]
        [InlineData(3, "Master")]
        [InlineData(2, "Agent")]
        [InlineData(1, "Member")]
        public void LevelName_Always_ReturnsCorrectValue(byte level, string expected)
        {
            // Arrange
            var model = new IndirectLoginAccountItem(1176083, 0, string.Empty, level, true, false, false, DateTime.Today.AddDays(-1), DateTime.Today, 0);

            // Act
            var actual = model.LevelName;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Status_Disabled_ReturnsDisabledText()
        {
            // Arrange
            var model = new IndirectLoginAccountItem(1176083, 91726, "master1", 3, false, false, false, null, null, 0);

            // Act
            var actual = model.Status;

            // Assert
            Assert.Equal("Disabled", actual);
        }

        [Fact]
        public void Status_Closed_ReturnsClosedText()
        {
            // Arrange
            var model = new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, true, false, null, null, 0);

            // Act
            var actual = model.Status;

            // Assert
            Assert.Equal("Closed", actual);
        }

        [Fact]
        public void Status_Suspended_ReturnsSuspendedText()
        {
            // Arrange
            var model = new IndirectLoginAccountItem(1176083, 91726, "master1", 3, true, false, true, null, null, 0);

            // Act
            var actual = model.Status;

            // Assert
            Assert.Equal("Suspended", actual);
        }

        [Fact]
        public void Status_Open_ReturnsOpenText()
        {
            // Arrange
            var model = new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, false, false, null, null, 0);

            // Act
            var actual = model.Status;

            // Assert
            Assert.Equal("Open", actual);
        }
    }
}
