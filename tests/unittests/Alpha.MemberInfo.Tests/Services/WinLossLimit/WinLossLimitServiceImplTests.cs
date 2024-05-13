namespace Alpha.MemberInfo.Tests.Services.WinLossLimit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Criteria;
    using MemberInfo.Criteria.WinLossLimit;
    using MemberInfo.Models;
    using MemberInfo.Models.WinLossLimit;
    using MemberInfo.Params;
    using MemberInfo.Services.WinLossLimit;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class WinLossLimitServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly WinLossLimitServiceImpl winLossLimitServiceImpl;
        private readonly CompareLogic compareLogic;
        private readonly Func<int, string> getProductName;
        private readonly Func<int, string> getSportTypeName;

        public WinLossLimitServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            getProductName = Substitute.For<Func<int, string>>();
            getSportTypeName = Substitute.For<Func<int, string>>();
            winLossLimitServiceImpl = new WinLossLimitServiceImpl(dynamicRepository, getProductName, getSportTypeName);

            compareLogic = new CompareLogic();
        }

        [Fact]
        public void GetWinLossLimitProducts_Always_ReturnCorrectData()
        {
            // Arrange
            var expectedValue = new List<WinLossLimitProductModel>();
            dynamicRepository.Fetch<WinLossLimitProductModel>(Arg.Any<GetWinLossLimitProductCriteria>()).Returns(expectedValue);

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitProducts(1);

            // Assert
            Assert.Equal(actualValue, actualValue);
        }

        [Fact]
        public void GetMasterList_Always_ReturnCorrectData()
        {
            // Arrange
            var expectedValue = new List<CustomerInfo>();
            dynamicRepository.Fetch<CustomerInfo>(Arg.Any<GetMasterListCriteria>()).Returns(expectedValue);

            // Act
            var actualValue = winLossLimitServiceImpl.GetMasterList(1);

            // Assert
            Assert.Equal(actualValue, actualValue);
        }

        [Fact]
        public void GetAgentList_Always_ReturnCorrectData()
        {
            // Arrange
            var expectedValue = new List<CustomerInfo>();
            dynamicRepository.Fetch<CustomerInfo>(Arg.Any<GetAgentListCriteria>()).Returns(expectedValue);

            // Act
            var actualValue = winLossLimitServiceImpl.GetAgentList(1);

            // Assert
            Assert.Equal(actualValue, actualValue);
        }

        [Fact]
        public void GetWinLossLimitPage_NotHaveWinLossLimits_ReturnCorrectValue()
        {
            // Arrange
            var param = new WinLossLimitParam(1, 0, 1, 1, string.Empty, "123", 1, 1, 1, 1, string.Empty);
            //winLossLimitDataAccess.GetWinLossLimits(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(new WinLossLimitPage());
            var expectedValue = new WinLossLimitPage();

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitPage(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue.WinLossLimitDetail.ToList(), actualValue.WinLossLimitDetail.ToList()).AreEqual);
        }

        [Fact]
        public void GetWinLossLimitPage_HaveWinLossLimitsAndNotPokerGames_ReturnCorrectValue()
        {
            // Arrange
            var param = new WinLossLimitParam(1, 0, 1, 1, string.Empty, string.Empty, 1, 1, 1, 1, string.Empty);
            var winLossLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2 },
            };

            dynamicRepository.Fetch<WinLossLimitDetail>(Arg.Any<GetBalanceCriteria>()).Returns(new List<WinLossLimitDetail>
            {
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 }
            });

            //winLossLimitDataAccess.GetWinLossLimits(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(new WinLossLimitPage(2, winLossLimits));

            var expectedLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 },
            };

            var expectedValue = new WinLossLimitPage(2, expectedLimits);

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitPage(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue.WinLossLimitDetail.ToList(), actualValue.WinLossLimitDetail.ToList()).AreEqual);
        }

        [Fact]
        public void GetWinLossLimitPage_HaveWinLossLimits_ReturnCorrectValue()
        {
            // Arrange
            var param = new WinLossLimitParam(1, 0, 1, 1, "123", string.Empty, 1, 1, 1, 1, string.Empty);
            var winLossLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2 },
            };

            dynamicRepository.Fetch<int>(Arg.Any<MapPlayerId3rdToCustIdCriteria>()).Returns(new List<int> { 1, 2 });

            dynamicRepository.Fetch<WinLossLimitDetail>(Arg.Any<GetBalanceCriteria>()).Returns(new List<WinLossLimitDetail>
            {
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 }
            });

            //winLossLimitDataAccess.GetWinLossLimits(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(new WinLossLimitPage(2, winLossLimits));

            var expectedLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 },
            };

            var expectedValue = new WinLossLimitPage(2, expectedLimits);

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitPage(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue.WinLossLimitDetail.ToList(), actualValue.WinLossLimitDetail.ToList()).AreEqual);
        }
    }
}
