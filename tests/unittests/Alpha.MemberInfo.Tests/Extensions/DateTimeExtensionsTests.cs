namespace Alpha.MemberInfo.Tests.Extensions
{
    using System;
    using Alpha.MemberInfo.Extensions;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ToShortDateWithHyphen_Always_ReturnsCorrectString()
        {
            // Arrange
            var expected = "2020-12-01";
            var date = new DateTime(2020, 12, 1);

            // Act
            var actual = date.ToShortDateWithHyphen();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToShortDateWithSlash_Always_ReturnsCorrectString()
        {
            // Arrange
            var expected = "12/01/2020";
            var date = new DateTime(2020, 12, 1);

            // Act
            var actual = date.ToShortDateWithSlash();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToLongDateTime24h_Always_ReturnsCorrectString()
        {
            // Arrange
            var expected = "12/01/2020 14:59";
            var date = new DateTime(2020, 12, 1, 14, 59, 0);

            // Act
            var actual = date.ToLongDateTime24h();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TToLongDateTime12hNullable_Null_ReturnsEmptyString()
        {
            // Arrange
            DateTime? date = null;

            // Act
            var actual = date.ToLongDateTime12h();

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void ToLongDateTime12hNullable_HasValue_ReturnsCorrectString()
        {
            // Arrange
            var expected = "12/01/2020 02:59 PM";
            DateTime? date = new DateTime(2020, 12, 1, 14, 59, 0);

            // Act
            var actual = date.ToLongDateTime12h();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
