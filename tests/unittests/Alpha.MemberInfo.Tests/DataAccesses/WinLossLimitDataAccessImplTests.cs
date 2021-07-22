namespace Alpha.MemberInfo.Tests.DataAccesses
{
    using System.Collections;
    using System.Collections.Generic;
    using Database.WinLossLimit;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Criteria.WinLossLimit;
    using MemberInfo.DataAccesses;
    using MemberInfo.Models.WinLossLimit;
    using MemberInfo.Params;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "WinLossLimit")]
    public class WinLossLimitDataAccessImplTests
    {
        private readonly WinLossLimitDataAccessImpl winLossLimitDataAccessImpl;
        private readonly CompareLogic compareLogic;
        private readonly IObjectDb objectDb;
        private readonly IDynamicRepository repository;

        public WinLossLimitDataAccessImplTests()
        {
            objectDb = Substitute.For<IObjectDb>();
            MemberInfoObjectDbFactory.CreateInstance = (settingKey) => objectDb;

            compareLogic = new CompareLogic();
            repository = Substitute.For<IDynamicRepository>();
            winLossLimitDataAccessImpl = new WinLossLimitDataAccessImpl(repository);
        }

        [Fact]
        public void GetWinLossLimits_InvalidCriteria_ReturnDefaultValue()
        {
            // Arrange
            var expectedValue = new WinLossLimitPage();
            var param = new WinLossLimitParam(1, 0, 1, 1, string.Empty, string.Empty, 1, 1, 1, 1);
            var criteria = new GetCustWinLossLimitCriteria(param, 1);

            // Act
            var actualValue = winLossLimitDataAccessImpl.GetWinLossLimits(criteria);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue, actualValue).AreEqual);
        }

        [Fact]
        public void GetWinLossLimits_NotPokerGames_ReturnCorrectValue()
        {
            // Arrange
            var param = new WinLossLimitParam(1, 1, 1, 1, string.Empty, string.Empty, 1, 1, 1, 1);
            var criteria = new GetCustWinLossLimitCriteria(param, 1);
            var winLossLimits = new List<WinLossLimitDetail> {
                new WinLossLimitDetail
                {
                    CustId = 3,
                    Closed = true,
                    ClosedReason = 1,
                    Enabled = false,
                    Suspended = false,
                    Username = "CustName",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Credit = 2,
                    IsAutoReset = true,
                    MaxWinLimit = 100,
                    MaxLossLimit = 5,
                    PlayerId = "6",
                    Locked = 0
                },
                new WinLossLimitDetail
                {
                    CustId = 3,
                    Closed = true,
                    ClosedReason = 2,
                    Enabled = false,
                    Suspended = false,
                    Username = "CustName",
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Credit = 2,
                    IsAutoReset = true,
                    MaxWinLimit = 100,
                    PlayerId = "7",
                    Locked = 2
                }
            };

            var expectedValue = new WinLossLimitPage(2, winLossLimits);

            var reportData = CustWinLossLimitDatabase.Data.CreateDataReader();
            objectDb.ExecuteReader(Arg.Any<GetCustWinLossLimitCriteria>()).Returns(reportData);
            objectDb.GetOuputParams().Returns(new Hashtable { ["totalrows"] = 2 });

            // Act
            var actualValue = winLossLimitDataAccessImpl.GetWinLossLimits(criteria);

            // Assert
            Assert.True(compareLogic.Compare(expectedValue, actualValue).AreEqual);
        }

        [Fact]
        public void MapPlayerIdToCustId_Always_ReturnCorrectValue()
        {
            // Arrange
            var criteria = new MapPlayerId3rdToCustIdCriteria(1, "2", 0);
            var expectedValue = new List<int> { 10 };
            winLossLimitDataAccessImpl.MapPlayerIdToCustId(criteria)
                .Returns(new List<int> { 10 });

            // Act
            var actualValue = winLossLimitDataAccessImpl.MapPlayerIdToCustId(criteria);

            // Assert
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
