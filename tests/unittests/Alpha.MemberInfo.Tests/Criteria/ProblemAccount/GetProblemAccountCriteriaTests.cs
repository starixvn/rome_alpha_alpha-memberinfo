namespace Alpha.MemberInfo.Tests.Criteria.ProblemAccount
{
    using Alpha.MemberInfo.Criteria.ProblemAccount;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Xunit;

    [Trait("ProblemAccount", "Get")]
    public class GetProblemAccountCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new GetProblemAccountCriteria(new GetProblemAccountParam());
            var expected = "Age_ProblemAccount_CustomerList";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0, 3)]
        [InlineData(1, -1, 3)]
        [InlineData(1, 0, 4)]
        [InlineData(-1, -1, 4)]
        public void GetSettingKey_IncorrectInput_ReturnsFalse(int custId, int subAccId, byte searchRoleId)
        {
            // Arrange
            var criteria = new GetProblemAccountCriteria(new GetProblemAccountParam
            {
                CustId = custId,
                SubAccId = subAccId,
                SearchRoleId = searchRoleId
            });

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1, 0, 3)]
        public void GetSettingKey_CorrectInput_ReturnsTrue(int custId, int subAccId, byte searchRoleId)
        {
            // Arrange
            var criteria = new GetProblemAccountCriteria(new GetProblemAccountParam
            {
                CustId = custId,
                SubAccId = subAccId,
                SearchRoleId = searchRoleId
            });

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.True(actual);
        }
    }
}
