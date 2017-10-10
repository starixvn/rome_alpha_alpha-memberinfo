namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Extensions;
    using Fanex.Data.Repository;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Criteria.MemberRequests;
    using MemberInfo.Models;
    using MemberInfo.Models.MemberRequests;
    using MemberInfo.Params;
    using MemberInfo.Services.MemberRequest;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    [Collection("AnyValueCreatorCollection")]
    public class MemberRequestServiceImplTests
    {
        private readonly MemberRequestServiceImpl serviceImpl;
        private readonly CompareLogic compareLogic;
        private readonly IUser user;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly IEnumerable<MemberRequestModel> memberRequests;
        private readonly CasinoCustomer casinoCustomer;

        public MemberRequestServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            casinoCustomer = Substitute.For<CasinoCustomer>();

            user = Substitute.For<IUser>();
            user.LevelId.Returns((byte)2);
            user.Id.Returns(10);
            user.Name.Returns("Name");
            user.Language.Returns("en-US");
            user.SubAccId.Returns(0);
            user.IP.Returns("192.168.1.1");
            user.GetHideProduct(Arg.Any<int>()).Returns(true);

            var dynamicRepository = Substitute.For<IDynamicRepository>();
            dynamicRepository.Fetch<Product>(Arg.Any<PlayForFunProductCriteria>()).Returns(new List<Product>
            {
                new Product(6, "Casino", false),
                new Product(22, "Fishing word", true),
                new Product(24, "Macau Games", true)
            });

            memberRequests = new List<MemberRequestModel>
            {
                new MemberRequestModel(),
                new MemberRequestModel(1, string.Empty, false, 0, 0, 0)
            };

            dynamicRepository.Fetch<MemberRequestModel>(Arg.Any<MemberRequestCriteria>()).Returns(memberRequests);

            serviceImpl = new MemberRequestServiceImpl(dynamicRepository, user, casinoCustomer);
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void GetPlayForFunProduct_HideFishingWorld_ReturnsCorrectList()
        {
            // Arrange
            var custId = anyValueCreatorFixture.Create<int>();
            user.GetHideProduct(ProductId.FishingWorld).Returns(true);
            var expectedProductList = new List<Product>
            {
                new Product(6, "Casino", false),
                new Product(22, "Fishing word", true)
            };

            // Act
            var actualProducts = serviceImpl.GetPlayForFunProduct(custId).ToList();

            // Assert
            var result = compareLogic.Compare(expectedProductList, actualProducts);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void GetPlayForFunProduct_ShowFishingWorld_ReturnsCorrectList()
        {
            // Arrange
            var custId = anyValueCreatorFixture.Create<int>();
            user.GetHideProduct(ProductId.FishingWorld).Returns(false);
            var expectedProductList = new List<Product>
            {
                new Product(6, "Casino", false),
                new Product(22, "Fishing word", true),
                new Product(24, "Macau Games", true)
            };

            // Act
            var actualProducts = serviceImpl.GetPlayForFunProduct(custId).ToList();

            // Assert
            var result = compareLogic.Compare(expectedProductList, actualProducts);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void GetMemberRequest_NullParam_ThrowsArgumentNullException()
        {
            // Arrange
            MemberRequestsParam nullParam = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => serviceImpl.GetMemberRequest(nullParam));
        }

        [Fact]
        public void GetMemberRequest_NotNullParam_ReturnsDataFromRepository()
        {
            // Arrange
            var param = anyValueCreatorFixture.Create<MemberRequestsParam>();
            var expectedData = memberRequests;

            // Act
            var actualData = serviceImpl.GetMemberRequest(param);

            // Assert
            AssertExtension.Equal(expectedData, actualData);
        }

        [Fact]
        public void GetSetting_Casino_ReturnsCorrectSetting()
        {
            // Arrange
            var setting = anyValueCreatorFixture.Create<GetCasinoSettingResult>();
            casinoCustomer.GetSetting(Arg.Any<ProductSettingParam>()).Returns(setting);
            var param = new ProductSettingParam(6, 1, string.Empty);
            var expectedSetting = setting;

            // Act
            var actualSetting = serviceImpl.GetSetting(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedSetting, actualSetting).AreEqual);
        }

        [Fact]
        public void IsSync_NotCasino_ReturnsTrue()
        {
            // Arrange
            var productId = 1;

            // Act
            var actualIsSync = serviceImpl.IsSync(productId);

            // Assert
            Assert.True(actualIsSync);
        }

        [Fact]
        public void IsSync_CasinoNotSync_ReturnsFalse()
        {
            // Arrange
            var productId = 6;
            user.GetSyncedProduct(Arg.Is<int>(p => p == 6)).Returns(false);

            // Act
            var actualIsSync = serviceImpl.IsSync(productId);

            // Assert
            Assert.False(actualIsSync);
        }

        [Fact]
        public void IsSync_CasinoSync_ReturnsFalse()
        {
            // Arrange
            var productId = 6;
            user.GetSyncedProduct(Arg.Is<int>(p => p == 6)).Returns(true);

            // Act
            var actualIsSync = serviceImpl.IsSync(productId);

            // Assert
            Assert.True(actualIsSync);
        }
    }
}
