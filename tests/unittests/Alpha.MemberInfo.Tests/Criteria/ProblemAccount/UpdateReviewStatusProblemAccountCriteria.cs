namespace Alpha.MemberInfo.Tests.Criteria.ProblemAccount
{
    using Alpha.MemberInfo.Criteria.ProblemAccount;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Xunit;

    [Trait("ProblemAccount", "Update")]
    public class UpdateReviewStatusProblemAccountCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var param = new UpdateProblemAccountParam();
            var criteria = new UpdateReviewStatusProblemAccountCriteria(
                param.CustId,
                param.SubAccId,
                param.RoleId,
                param.CustomerList,
                param.IsAffectedAllDownlines,
                param.Language,
                param.LoginIp);
            var expected = "Age_ProblemAccount_ReviewStatusUpd";

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1, 0, 3, "1,2")]
        [InlineData(1, -1, 3, "1,2")]
        [InlineData(1, 0, 4, "1,2")]
        [InlineData(1, 0, 3, "")]
        public void GetSettingKey_IncorrectInput_ReturnsFalse(int custId, int subAccId, byte searchRoleId, string ids)
        {
            // Arrange
            var param = new UpdateProblemAccountParam
            {
                CustId = custId,
                SubAccId = subAccId,
                RoleId = searchRoleId,
                CustomerList = ids
            };
            var criteria = new UpdateReviewStatusProblemAccountCriteria(
                param.CustId,
                param.SubAccId,
                param.RoleId,
                param.CustomerList,
                param.IsAffectedAllDownlines,
                param.Language,
                param.LoginIp);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1, 0, 3, "1,2")]
        public void GetSettingKey_CorrectInput_ReturnsTrue(int custId, int subAccId, byte searchRoleId, string ids)
        {
            // Arrange
            var param = new UpdateProblemAccountParam
            {
                CustId = custId,
                SubAccId = subAccId,
                RoleId = searchRoleId,
                CustomerList = ids
            };
            var criteria = new UpdateReviewStatusProblemAccountCriteria(
                param.CustId,
                param.SubAccId,
                param.RoleId,
                param.CustomerList,
                param.IsAffectedAllDownlines,
                param.Language,
                param.LoginIp);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.True(actual);
        }
    }
}
