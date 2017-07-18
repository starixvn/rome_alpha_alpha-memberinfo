using System.Collections.Generic;
using System.Linq;
using Alpha.MemberInfo.Constants;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Models.MemberRequests;
using Alpha.MemberInfo.Params;
using Alpha.MemberInfo.Services.MemberRequest;
using Alpha.MemberInfo.Tests.Fixtures;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    [Trait("Category", "MemberRequests")]
    [Collection("AnyValueCreatorCollection")]
    public class CasinoCustomerImplTests
    {
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly CasinoCustomerImplMock serviceImpl;
        private readonly CompareLogic compareLogic;
        private readonly IEnumerable<CasinoGameKindModel> gameKinds;

        public CasinoCustomerImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;

            gameKinds = new List<CasinoGameKindModel>
            {
                new CasinoGameKindModel(CasinoGameKind.RNGGame, 0, 1)
            };

            serviceImpl = new CasinoCustomerImplMock();
            serviceImpl.SetGameKinds(gameKinds);

            compareLogic = new CompareLogic();
        }

        [Fact]
        public void GetSetting_NotError_ReturnsCorrectSetting()
        {
            // Arrange
            var param = anyValueCreatorFixture.Create<ProductSettingParam>();
            var expectedSetting = new GetCasinoSettingResult(gameKinds, 0, string.Empty);

            // Act
            var actualSetting = serviceImpl.GetSetting(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedSetting, actualSetting).AreEqual);
        }

        [Fact]
        public void GetSetting_Error_ReturnsCorrectSetting()
        {
            // Arrange
            var param = anyValueCreatorFixture.Create<ProductSettingParam>();
            var expectedSetting = new GetCasinoSettingResult(Enumerable.Empty<CasinoGameKindModel>(), 1, "Not casino member.");
            var service = new CasinoCustomerImplErrorMock();

            // Act
            var actualSetting = service.GetSetting(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedSetting, actualSetting).AreEqual);
        }
    }
}
