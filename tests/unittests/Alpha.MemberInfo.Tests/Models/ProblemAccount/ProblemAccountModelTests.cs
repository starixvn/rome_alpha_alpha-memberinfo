using System;
using Alpha.MemberInfo.Models.ProblemAccount;
using Xunit;

namespace Alpha.MemberInfo.Tests.Models.ProblemAccount
{
    [Trait("Category", "MemberInfo")]
    public class ProblemAccountModelTests
    {
        [Theory]
        [InlineData("razor", "razor")]
        [InlineData(null, "")]
        public void NickName_Always_GetFromNickName2(string userName2, string expected)
        {
            // Arrange
            var model = new ProblemAccountModel
            {
                Username2 = userName2
            };

            // Act
            var act = model.NickName;

            // Assert
            Assert.Equal(expected, act);
        }

        [Theory]
        [InlineData(1, "Open")]
        [InlineData(2, "Suspended")]
        [InlineData(3, "Closed")]
        [InlineData(4, "Disabled")]
        [InlineData(5, null)]
        public void StatusName_Always_GetFromStatus(byte status, string expected)
        {
            // Arrange
            var model = new ProblemAccountModel
            {
                Status = status
            };

            // Act
            var act = model.StatusName;

            // Assert
            Assert.Equal(expected, act);
        }

        [Theory]
        [InlineData(true, "Reviewed")]
        [InlineData(false, "Not Reviewed")]
        public void StatusName_Always_GetFromStatus(bool isReviewed, string expected)
        {
            // Arrange
            var model = new ProblemAccountModel
            {
                IsReviewed = isReviewed
            };

            // Act
            var act = model.ReviewStatus;

            // Assert
            Assert.Equal(expected, act);
        }

        [Fact]
        public void CreatedDateStr_Always_FormatLongDateTime24h()
        {
            // Arrange
            var model = new ProblemAccountModel
            {
                CreatedDate = new DateTime(2020, 3, 2, 23, 50, 30)
            };

            // Act
            var act = model.CreatedDateStr;

            // Assert
            Assert.Equal("03/02/2020 23:50", act);
        }
    }
}
