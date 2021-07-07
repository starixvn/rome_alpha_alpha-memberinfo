namespace Alpha.MemberInfo.Tests.Feature.DormantAccountList.Criteria
{
    using Alpha.MemberInfo.Features.DormantAccountList.Criteria;
    using Alpha.MemberInfo.Features.DormantAccountList.Params;
    using Xunit;

    [Trait("Category", "DormantAccountList")]
    public class GetDormantAccountsCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var expected = "Acc_DormantAccountList_Sel";
            var param = new DormantAccountParam(1176070, null, 0, 0, 1, 500);
            var criteria = new GetDormantAccountsCriteria(param);

            // Act
            var actual = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-1176070, false)]
        [InlineData(0, false)]
        [InlineData(1176070, true)]
        public void IsValid_CustId_ReturnsValid(int custId, bool expected)
        {
            // Arrange
            var param = new DormantAccountParam(custId, null, 0, 0, 1, 500);
            var criteria = new GetDormantAccountsCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(10, false)]
        public void IsValid_RoleId_ReturnsValid(byte roleId, bool expected)
        {
            // Arrange
            var param = new DormantAccountParam(1176070, null, roleId, 0, 1, 500);
            var criteria = new GetDormantAccountsCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(5, true)]
        public void IsValid_StatusId_ReturnsValid(byte statusId, bool expected)
        {
            // Arrange
            var param = new DormantAccountParam(1176070, null, 0, statusId, 1, 500);
            var criteria = new GetDormantAccountsCriteria(param);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Constructor_Always_AssignsCorrectValues()
        {
            // Arrange & Act
            var param = new DormantAccountParam(1176070, "master1", 3, 4, 2, 100);
            var criteria = new GetDormantAccountsCriteria(param) { TotalItems = 1001 };

            // Assert
            Assert.True(
                1176070 == criteria.CustId
                && "master1" == criteria.FilterUsername
                && 3 == criteria.FilterRoleId
                && 4 == criteria.FilterStatusId
                && 2 == criteria.PageIndex
                && 100 == criteria.PageSize
                && 1001 == criteria.TotalItems);
        }
    }
}
