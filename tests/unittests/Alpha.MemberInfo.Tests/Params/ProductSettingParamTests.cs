namespace Alpha.MemberInfo.Tests.Params
{
    using Alpha.MemberInfo.Params;
    using Xunit;

    [Trait("Category", "MemberInfo")]
    public class ProductSettingParamTests
    {
        [Fact]
        public void CustName_Awlays_ExpectedName()
        {
            //// Arrange
            var expectedName = "razor";
            var param = new ProductSettingParam(123, 321, expectedName);

            //// Act

            //// Assert
            Assert.Equal(expectedName, param.CustName);
        }
    }
}
