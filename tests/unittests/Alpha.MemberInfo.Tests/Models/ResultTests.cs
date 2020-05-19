namespace Alpha.MemberInfo.Tests.Models
{
    using Alpha.MemberInfo.Models;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class ResultTests
    {
        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(500, false)]
        public void IsSuccessful_Code_ReturnsCorrectValue(int code, bool expected)
        {
            // Arrange.
            var result = new Result { Code = code };

            // Act.
            var actual = result.IsSuccessful;

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(0, false)]
        [InlineData(500, true)]
        public void IsFailed_Code_ReturnsCorrectValue(int code, bool expected)
        {
            // Arrange.
            var result = new Result { Code = code };

            // Act.
            var actual = result.IsFailed;

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(500, false)]
        public void IsOk_Code_ReturnsCorrectValue(int code, bool expected)
        {
            // Arrange.
            var result = new Result { Code = code };

            // Act.
            var actual = result.IsOk;

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, true)]
        [InlineData(0, false)]
        [InlineData(500, true)]
        public void IsNotOk_Code_ReturnsCorrectValue(int code, bool expected)
        {
            // Arrange.
            var result = new Result { Code = code };

            // Act.
            var actual = result.IsNotOk;

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
