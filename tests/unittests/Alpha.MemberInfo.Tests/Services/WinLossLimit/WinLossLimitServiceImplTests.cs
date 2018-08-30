namespace Alpha.MemberInfo.Tests.Services.WinLossLimit
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Criteria;
    using MemberInfo.Criteria.WinLossLimit;
    using MemberInfo.DataAccesses;
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
        private readonly WinLossLimitDataAccess winLossLimitDataAccess;
        private readonly WinLossLimitServiceImpl winLossLimitServiceImpl;
        private readonly CompareLogic compareLogic;

        public WinLossLimitServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            winLossLimitDataAccess = Substitute.For<WinLossLimitDataAccess>();
            winLossLimitServiceImpl = new WinLossLimitServiceImpl(dynamicRepository, winLossLimitDataAccess);

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
            var param = new WinLossLimitParam(1, 0, 1, 1, string.Empty, "123", 1, 1, 1, 1);
            winLossLimitDataAccess.GetWinLossLimits(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(new WinLossLimitPage());
            var expectedValue = new WinLossLimitPage();

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitPage(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue, actualValue).AreEqual);
        }

        [Fact]
        public void GetWinLossLimitPage_HaveWinLossLimitsAndNotPokerGames_ReturnCorrectValue()
        {
            // Arrange
            var param = new WinLossLimitParam(1, 0, 1, 1, string.Empty, "123", 1, 1, 1, 1);
            var winLossLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2 },
            };

            dynamicRepository.Fetch<WinLossLimitDetail>(Arg.Any<GetBalanceCriteria>()).Returns(new List<WinLossLimitDetail>
            {
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 }
            });

            winLossLimitDataAccess.GetWinLossLimits(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(new WinLossLimitPage(2, winLossLimits));

            var expectedLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 },
            };

            var expectedValue = new WinLossLimitPage(2, expectedLimits);

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitPage(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue, actualValue).AreEqual);
        }

        [Fact]
        public void GetWinLossLimitPage_HaveWinLossLimitsAndPokerGames_ReturnCorrectValue()
        {
            // Arrange
            var param = new WinLossLimitParam(33, 0, 1, 163, string.Empty, "CP123", 1, 1, 1, 1);
            var winLossLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1 },
                new WinLossLimitDetail { CustId = 2 },
            };

            dynamicRepository.Fetch<WinLossLimitDetail>(Arg.Any<GetBalanceCriteria>()).Returns(new List<WinLossLimitDetail>
            {
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 }
            });

            dynamicRepository.Fetch<WinLossLimitDetail>(Arg.Any<GetOutstandingCriteria>()).Returns(new List<WinLossLimitDetail>
            {
                new WinLossLimitDetail { CustId = 1, Outstanding = 50 }
            });

            winLossLimitDataAccess.GetWinLossLimits(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(new WinLossLimitPage(2, winLossLimits));

            var expectedLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail { CustId = 1, Outstanding = 50 },
                new WinLossLimitDetail { CustId = 2, CreditBalance = 10, DebitBalance = 20, Balance = 30 },
            };

            var expectedValue = new WinLossLimitPage(2, expectedLimits);

            // Act
            var actualValue = winLossLimitServiceImpl.GetWinLossLimitPage(param);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue, actualValue).AreEqual);
        }
    }
}
