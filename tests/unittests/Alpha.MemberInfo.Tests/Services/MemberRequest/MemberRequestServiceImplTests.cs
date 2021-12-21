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

        public MemberRequestServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;         

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
                new Product(22, "Fishing word", true),
                new Product(24, "Macau Games", true)
            });

            memberRequests = new List<MemberRequestModel>
            {
                new MemberRequestModel(),
                new MemberRequestModel(1, string.Empty, false, 0, 0, 0)
            };

            dynamicRepository.Fetch<MemberRequestModel>(Arg.Any<MemberRequestCriteria>()).Returns(memberRequests);

            serviceImpl = new MemberRequestServiceImpl(dynamicRepository, user);
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
       
    }
}
