namespace Alpha.MemberInfo.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using MemberInfo.Extensions;
    using TestCases;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class StringExtensionsTests
    {
        [Fact]
        public void ToListInt_StringIsNull_ReturnEmptyListInt()
        {
            // Arrange
            string s = null;

            // Act
            var expected = StringExtensions.ToListInt(s);

            // Assert
            Assert.Empty(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void ToListInt_StringIsEmptyOrWhiteSpace_ReturnEmptyListInt(string s)
        {
            // Act
            var expected = s.ToListInt();

            // Assert
            Assert.Empty(expected);
        }

        [Theory]
        [MemberData("StringContainsNonNumericCharactersCases", MemberType = typeof(StringExtensionsTestCases))]
        public void ToListInt_Always_IgnoreNonNumbericCharacters(string s, List<int> ids)
        {
            // Arrange

            // Act
            var expected = s.ToListInt();

            // Assert
            Assert.Equal(expected, ids);
        }

        [Fact]
        public void FormatDateTimeEn12_DateTimeIsNull_ReturnDash()
        {
            // Arrange
            DateTime? dateTime = null;
            var expected = "-";

            // Act
            var actual = StringExtensions.FormatDateTimeEn12(dateTime);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FormatDateTimeEn12_ValidDateTime_ReturnCorrectFormat()
        {
            // Arrange
            DateTime? dateTime = new DateTime(2018, 8, 5, 23, 4, 16);
            var expected = "08/05/2018 11:04:16 PM";

            // Act
            var actual = StringExtensions.FormatDateTimeEn12(dateTime);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}