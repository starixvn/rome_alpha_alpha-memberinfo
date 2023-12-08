namespace Alpha.MemberInfo.Tests.Params
{
    using MemberInfo.Constants;
    using MemberInfo.Models;
    using MemberInfo.Params;
    using Xunit;

    [Trait("Category", "CreditBalance")]
    public class CreditBalanceParamTests
    {
        private const int CustIdTest = 1176070;
        private readonly UserLevel UserLevelTest = UserLevel.Super;
        private const string UserNameTest = "master1";
        private const int PageIndexTest = 1;
        private const int PageSizeTest = 500;
        private const bool IsAdminCanViewVNIPTest = true;

        [Fact]
        public void MapStatusIdToParam_All_ReturnsCorrectParam()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevelTest,
                UserNameTest,
                StatusIdentity.All,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest,
                string.Empty);

            // Act & Assert
            Assert.True(
                param.Enabled == null
                && param.Closed == null
                && param.Suspended == null);
        }

        [Fact]
        public void MapStatusIdToParam_Open_ReturnsCorrectParam()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevelTest,
                UserNameTest,
                StatusIdentity.Open,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest,
                string.Empty);

            // Act & Assert
            Assert.True(
                param.Enabled == null
                && param.Closed == false
                && param.Suspended == null);
        }

        [Fact]
        public void MapStatusIdToParam_Suspended_ReturnsCorrectParam()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevelTest,
                UserNameTest,
                StatusIdentity.Suspended,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest,
                string.Empty);

            // Act & Assert
            Assert.True(
                param.Enabled == null
                && param.Closed == null
                && param.Suspended == true);
        }

        [Fact]
        public void MapStatusIdToParam_Closed_ReturnsCorrectParam()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevelTest,
                UserNameTest,
                StatusIdentity.Closed,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest,
                string.Empty);

            // Act & Assert
            Assert.True(
                param.Enabled == null
                && param.Closed == true
                && param.Suspended == null);
        }

        [Fact]
        public void MapStatusIdToParam_Disabled_ReturnsCorrectParam()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevelTest,
                UserNameTest,
                StatusIdentity.Disabled,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest,
                string.Empty);

            // Act & Assert
            Assert.True(
                param.Enabled == false
                && param.Closed == null
                && param.Suspended == null);
        }
    }
}
