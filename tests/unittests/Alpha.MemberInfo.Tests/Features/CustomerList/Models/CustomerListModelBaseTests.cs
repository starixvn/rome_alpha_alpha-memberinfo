namespace Alpha.MemberInfo.Tests.Services
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using System;
    using Xunit;

    [Trait("Category", "CustomerList")]
    [Collection("AnyValueCreatorCollection")]
    public class CustomerListModelBaseTests
    {
        [Theory]
        [InlineData(false, false, false, "Disabled")]
        [InlineData(false, true, false, "Disabled")]
        [InlineData(false, true, true, "Disabled")]
        [InlineData(false, false, true, "Disabled")]
        [InlineData(true, true, true, "Closed")]
        [InlineData(true, true, false, "Closed")]
        [InlineData(true, false, true, "Suspended")]
        [InlineData(true, false, false, "Open")]
        public void MasterListModel_Return_CorrectStatusName(
            bool enable,
            bool closed,
            bool suspended,
            string expectedStatusName)
        {
            var model = new MasterListModel(
                    1,
                    "master1",
                    3,
                    "a",
                    string.Empty,
                     string.Empty,
                      string.Empty,
                      "1234",
                      string.Empty,
                      string.Empty,
                      20,
                      1,
                      1,
                      new DateTime(2020, 11, 30),
                      false,
                      true,
                      2,
                      2,
                      enable,
                      closed,
                      suspended,
                      true,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      true,
                      (decimal)0.05,
                      0);

            model.AllowOpenInfoPopup = true;

            Assert.Equal(expectedStatusName, model.Status, ignoreCase: true);
        }
    }
}