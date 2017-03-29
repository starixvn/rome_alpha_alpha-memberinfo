namespace Alpha.MemberInfo.Tests.Services.MemberRequestService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.MemberRequests;
    using Fanex.Data.Repository;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Services;
    using Models;
    using Models.MemberRequests;
    using NSubstitute;
    using Params;
    using RemotingRef;
    using Sunplus.Agent.Model.Casino;
    using Xunit;

    [Trait("Category", "MemberRequests")]
    [Collection("AnyValueCreatorCollection")]
    public class MemberRequestServiceImplTests
    {
        private readonly MemberRequestServiceImpl serviceImpl;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CasinoService casinoService;
        private readonly CacheRefService cacheRefService;
        private readonly CompareLogic compareLogic;
        private readonly IUser user;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;

        public MemberRequestServiceImplTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            casinoService = Substitute.For<CasinoService>();

            user = Substitute.For<IUser>();
            user.LevelId.Returns((byte)2);
            user.Id.Returns(10);
            user.Name.Returns("Name");
            user.Language.Returns("en-US");
            user.SubAccId.Returns(0);
            user.IP.Returns("192.168.1.1");
            user.GetHideProduct(Arg.Any<int>()).Returns(true);

            dynamicRepository = Substitute.For<IDynamicRepository>();
            dynamicRepository.Fetch<PlayForFunProduct>(Arg.Any<PlayForFunProductCriteria>()).Returns(new List<PlayForFunProduct>
            {
                new PlayForFunProduct(6, string.Empty, false),
                new PlayForFunProduct(22, string.Empty, false),
                new PlayForFunProduct(24, string.Empty, false)
            });
            dynamicRepository.Execute(Arg.Any<EnableCasinoCommand>());

            cacheRefService = Substitute.For<CacheRefService>();
            cacheRefService.GetProductName(Arg.Any<int>()).Returns(callInfo => "Product " + callInfo.Args()[0]);
             
            serviceImpl = new MemberRequestServiceImpl(casinoService, dynamicRepository, user, cacheRefService);
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
                new Product(6, string.Empty,  cacheRefService.GetProductName),
                new Product(22, string.Empty,  cacheRefService.GetProductName)
            };

            // Act
            var actualProducts = serviceImpl.SupportedProduct(custId).ToList();

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
                new Product(6, string.Empty,  cacheRefService.GetProductName),
                new Product(22, string.Empty,  cacheRefService.GetProductName),
                new Product(24, string.Empty,  cacheRefService.GetProductName)
            };

            // Act
            var actualProducts = serviceImpl.SupportedProduct(custId).ToList();

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

        public void EnableCasino_ExistedMember_CallEnableCasino()
        {
            // Arrange
            var members = new List<MemberRequestModel>
            {
                new MemberRequestModel(
                    custId: 12,
                    username: "member01",
                    sync: true,
                    balance: 123,
                    outstanding: 236,
                    betCredit: 12)
            };

            var param = new EnableProductParam(
                 productId: ProductId.Casino,
                 members: members,
                 actorId: 16,
                 actorName: "Username",
                 subAccId: 0,
                 loginIP: "192.168.1.1",
                 language: "E");

            // Act 
            serviceImpl.EnableProduct(param);

            // Assert
            serviceImpl
                .Received()
                .EnableCasino(Arg.Is<EnableProductParam>(
                    inputParam => inputParam.ProductId.Equals(param.ProductId)
                    && inputParam.ActorId.Equals(param.ActorId)
                    && inputParam.ActorName.Equals(param.ActorName)
                    && inputParam.SubAccId.Equals(param.SubAccId)
                    && inputParam.LoginIP.Equals(param.LoginIP)
                    && inputParam.Language.Equals(param.Language)));
        }
    }
}