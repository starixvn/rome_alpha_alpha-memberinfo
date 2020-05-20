namespace Alpha.MemberInfo.Tests.Criteria.WinLossLimit
{
    using MemberInfo.Criteria.WinLossLimit;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class MapPlayerId3rdToCustIdCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expectedSettingKey = "Age_CustomerSelByPlayerId";
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, "2", 0);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_Always_ReturnsCorrectValue()
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, "2", 0);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }

        [Fact]
        public void IsValid_Always_ReturnsCorrectValueWithFullCtor()
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, "1", 1, 1, 1);

            // Act
            var actualIsValid = criteria.IsValid();

            // Assert
            Assert.True(actualIsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void ProductId_Always_GetFromCtor(int id)
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(id, string.Empty, id, id, id);

            // Act
            var actualIsValid = criteria.ProductId;

            // Assert
            Assert.Equal(id, actualIsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CustId_Always_GetFromCtor(int id)
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(id, string.Empty, id, id, id);

            // Act
            var actualIsValid = criteria.CustId;

            // Assert
            Assert.Equal(id, actualIsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Mrecommend_Always_GetFromCtor(int id)
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(id, string.Empty, id, id, id);

            // Act
            var actualIsValid = criteria.Mrecommend;

            // Assert
            Assert.Equal(id, actualIsValid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Recommend_Always_GetFromCtor(int id)
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(id, string.Empty, id, id, id);

            // Act
            var actualIsValid = criteria.Recommend;

            // Assert
            Assert.Equal(id, actualIsValid);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        public void PlayerId_Always_GetFromCtor(string id)
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, id, 1, 1, 1);

            // Act
            var actualIsValid = criteria.PlayerId;

            // Assert
            Assert.Equal(id, actualIsValid);
        }
    }
}
