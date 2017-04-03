namespace Alpha.MemberInfo.Tests.Services.MemberRequestService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.MemberRequests;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Services;
    using Models;
    using NSubstitute;
    using Params;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    [Collection("AnyValueCreatorCollection")]
    public class MemberRequestServiceImplTests
    {
        private readonly MemberRequestServiceImpl serviceImpl;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CasinoDataAccess casinoDataAccess;
        private readonly CompareLogic compareLogic;
        private readonly IUser user;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;

        public MemberRequestServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            casinoDataAccess = Substitute.For<CasinoDataAccess>();

            user = Substitute.For<IUser>();
            user.LevelId.Returns((byte)2);
            user.Id.Returns(10);
            user.Name.Returns("Name");
            user.Language.Returns("en-US");
            user.SubAccId.Returns(0);
            user.IP.Returns("192.168.1.1");
            user.GetHideProduct(Arg.Any<int>()).Returns(true);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            dynamicRepository.Fetch<Product>(Arg.Any<PlayForFunProductCriteria>()).Returns(new List<Product>
            {
                new Product(6, "Casino", false),
                new Product(22, "Fishing word", true),
                new Product(24, "Macau Games", true)
            });

            serviceImpl = new MemberRequestServiceImpl(dynamicRepository, user, casinoDataAccess);
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void SupportedProduct_HideFishingWorld_ReturnsCorrectList()
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
        public void SupportedProduct_ShowFishingWorld_ReturnsCorrectList()
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
    }
}