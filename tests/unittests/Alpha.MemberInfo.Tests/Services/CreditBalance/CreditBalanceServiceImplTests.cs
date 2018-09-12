namespace Alpha.MemberInfo.Tests.Services.CreditBalance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fanex.Data.Repository;
    using MemberInfo.Constants;
    using MemberInfo.Criteria.CreditBalance;
    using MemberInfo.Models;
    using MemberInfo.Models.CreditBalance;
    using MemberInfo.Params;
    using MemberInfo.Services.CreditBalance;
    using NSubstitute;
    using Utilities.IP;
    using Xunit;

    [Trait("Category", "Transfer")]
    public class CreditBalanceServiceImplTests
    {
        private const int CustIdTest = 1176070;
        private readonly UserLevel UserLevelTest = UserLevel.Super;
        private const string UserNameTest = "master1";
        private const byte StatusIdTest = StatusIdentity.Open;
        private const int PageIndexTest = 1;
        private const int PageSizeTest = 500;
        private const bool IsAdminCanViewVNIPTest = true;

        private readonly IDynamicRepository dynamicRepository;
        private readonly IPService ipService;
        private readonly CreditBalanceService service;

        public CreditBalanceServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            ipService = Substitute.For<IPService>();
            service = new CreditBalanceServiceImpl(dynamicRepository, ipService);
        }

        [Fact]
        public async Task GetPagedCreditBalancesAsync_Super_ReturnsPagedModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Super,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var creditBalances = new List<CreditBalanceItem>
            {
                new CreditBalanceItem(
                    1,
                    true,
                    true,
                    false,
                    1176083,
                    "master1",
                    "---",
                    "---",
                    900000002,
                    826715185,
                    -20399.61m,
                    -20399.61m,
                    371662,
                    new DateTime(2018, 8, 6, 23, 35, 0),
                    "10.18.200.119"),
            };

            dynamicRepository.FetchAsync<CreditBalanceItem>(Arg.Any<GetSuperCreditBalanceCriteria>())
                .Returns(creditBalances);
            var closedReasons = new List<CustomerClosedReason>
            {
                new CustomerClosedReason(1176083, 1)
            };
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetSuperClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetPagedCreditBalancesAsync(param);

            // Assert
            Assert.True(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetPagedCreditBalancesAsync_Master_ReturnsPagedModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Master,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var creditBalances = new List<CreditBalanceItem>
            {
                new CreditBalanceItem(
                    1,
                    true,
                    false,
                    false,
                    1176083,
                    "master1",
                    "---",
                    "---",
                    900000002,
                    826715185,
                    -20399.61m,
                    -20399.61m,
                    371662,
                    new DateTime(2018, 8, 6, 23, 35, 0),
                    "10.18.200.119"),
            };
            dynamicRepository.FetchAsync<CreditBalanceItem>(Arg.Any<GetMasterCreditBalanceCriteria>())
                .Returns(creditBalances);

            var closedReasons = Enumerable.Empty<CustomerClosedReason>();
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetMasterClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetPagedCreditBalancesAsync(param);

            // Assert
            Assert.True(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetPagedCreditBalancesAsync_Agent_ReturnsPagedModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Agent,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var creditBalances = new List<CreditBalanceItem>
            {
                new CreditBalanceItem(
                    1,
                    true,
                    false,
                    false,
                    1176083,
                    "master1",
                    "---",
                    "---",
                    900000002,
                    826715185,
                    -20399.61m,
                    -20399.61m,
                    371662,
                    new DateTime(2018, 8, 6, 23, 35, 0),
                    "10.18.200.119"),
            };
            dynamicRepository.FetchAsync<CreditBalanceItem>(Arg.Any<GetAgentCreditBalanceCriteria>())
                .Returns(creditBalances);

            var agentNetPositions = new List<AgentNetPosition>
            {
                new AgentNetPosition(1176083, 99802.05m)
            };
            dynamicRepository.FetchAsync<AgentNetPosition>(Arg.Any<GetAgentNetPositionCriteria>())
                .Returns(agentNetPositions);

            var closedReasons = Enumerable.Empty<CustomerClosedReason>();
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetAgentClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetPagedCreditBalancesAsync(param);

            // Assert
            Assert.True(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetCreditBalancesAsync_Super_ReturnsPagedModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Super,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var creditBalances = new List<CreditBalanceItem>
            {
                new CreditBalanceItem(
                    1,
                    true,
                    false,
                    false,
                    1176083,
                    "master1",
                    "---",
                    "---",
                    900000002,
                    826715185,
                    -20399.61m,
                    -20399.61m,
                    371662,
                    new DateTime(2018, 8, 6, 23, 35, 0),
                    "10.18.200.119"),
            };
            dynamicRepository.FetchAsync<CreditBalanceItem>(Arg.Any<GetSuperCreditBalanceCriteria>())
                .Returns(creditBalances);

            // Act
            var actual = await service.GetCreditBalancesAsync(param);

            // Assert
            Assert.True(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetCreditBalancesAsync_Master_ReturnsPagedModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Master,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var creditBalances = new List<CreditBalanceItem>
            {
                new CreditBalanceItem(
                    1,
                    true,
                    false,
                    false,
                    1176083,
                    "master1",
                    "---",
                    "---",
                    900000002,
                    826715185,
                    -20399.61m,
                    -20399.61m,
                    371662,
                    new DateTime(2018, 8, 6, 23, 35, 0),
                    "10.18.200.119"),
            };
            dynamicRepository.FetchAsync<CreditBalanceItem>(Arg.Any<GetMasterCreditBalanceCriteria>())
                .Returns(creditBalances);

            // Act
            var actual = await service.GetCreditBalancesAsync(param);

            // Assert
            Assert.True(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetCreditBalancesAsync_Agent_ReturnsPagedModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Agent,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var creditBalances = new List<CreditBalanceItem>
            {
                new CreditBalanceItem(
                    1,
                    true,
                    false,
                    false,
                    1176083,
                    "master1",
                    "---",
                    "---",
                    900000002,
                    826715185,
                    -20399.61m,
                    -20399.61m,
                    371662,
                    new DateTime(2018, 8, 6, 23, 35, 0),
                    "10.18.200.119"),
            };
            dynamicRepository.FetchAsync<CreditBalanceItem>(Arg.Any<GetAgentCreditBalanceCriteria>())
                .Returns(creditBalances);

            // Act
            var actual = await service.GetCreditBalancesAsync(param);

            // Assert
            Assert.True(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetCreditBalancesAsync_Member_ReturnsEmptyModel()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Member,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);

            // Act
            var actual = await service.GetCreditBalancesAsync(param);

            // Assert
            Assert.False(actual.CreditBalances.Any());
        }

        [Fact]
        public async Task GetClosedReasonsAsync_Super_ReturnsDictionary()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Super,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var closedReasons = new List<CustomerClosedReason>
            {
                new CustomerClosedReason(1, 0),
                new CustomerClosedReason(2, 1),
                new CustomerClosedReason(3, 2),
                new CustomerClosedReason(4, 3),
                new CustomerClosedReason(5, 4)
            };
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetSuperClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetClosedReasonsAsync(param);

            // Assert
            Assert.True(
                actual.Count > 0
                && actual[3] == 2);
        }

        [Fact]
        public async Task GetClosedReasonsAsync_Master_ReturnsDictionary()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Master,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var closedReasons = new List<CustomerClosedReason>
            {
                new CustomerClosedReason(1, 0),
                new CustomerClosedReason(2, 1),
                new CustomerClosedReason(3, 2),
                new CustomerClosedReason(4, 3),
                new CustomerClosedReason(5, 4)
            };
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetMasterClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetClosedReasonsAsync(param);

            // Assert
            Assert.True(
                actual.Count > 0
                && actual[3] == 2);
        }

        [Fact]
        public async Task GetClosedReasonsAsync_Agent_ReturnsDictionary()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Agent,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var closedReasons = new List<CustomerClosedReason>
            {
                new CustomerClosedReason(1, 0),
                new CustomerClosedReason(2, 1),
                new CustomerClosedReason(3, 2),
                new CustomerClosedReason(4, 3),
                new CustomerClosedReason(5, 4)
            };
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetAgentClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetClosedReasonsAsync(param);

            // Assert
            Assert.True(
                actual.Count > 0
                && actual[3] == 2);
        }

        [Fact]
        public async Task GetClosedReasonsAsync_Member_ReturnsEmptyDictionary()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Member,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var closedReasons = Enumerable.Empty<CustomerClosedReason>();
            dynamicRepository.FetchAsync<CustomerClosedReason>(Arg.Any<GetAgentClosedReasonCriteria>())
                .Returns(closedReasons);

            // Act
            var actual = await service.GetClosedReasonsAsync(param);

            // Assert
            Assert.True(actual.Count == 0);
        }

        [Fact]
        public async Task GetAgentNetPositionsAsync_ValidCustLevel_ReturnsDictionary()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Agent,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);
            var agentNetPositions = new List<AgentNetPosition>
            {
                new AgentNetPosition(1, 99802.05m),
                new AgentNetPosition(2, 9981.06m),
                new AgentNetPosition(3, 10009.04m),
                new AgentNetPosition(4, -1228.8m),
                new AgentNetPosition(5, 100000)
            };
            dynamicRepository.FetchAsync<AgentNetPosition>(Arg.Any<GetAgentNetPositionCriteria>())
                .Returns(agentNetPositions);

            // Act
            var actual = await service.GetAgentNetPositionsAsync(param);

            // Assert
            Assert.True(
                actual.Count > 0
                && actual[3] == 10009.04m);
        }

        [Fact]
        public async Task GetAgentNetPositionsAsync_InvalidCustLevel_ReturnsEmptyDictionary()
        {
            // Arrange
            var param = new CreditBalanceParam(
                CustIdTest,
                UserLevel.Super,
                UserNameTest,
                StatusIdTest,
                PageIndexTest,
                PageSizeTest,
                IsAdminCanViewVNIPTest);

            // Act
            var actual = await service.GetAgentNetPositionsAsync(param);

            // Assert
            Assert.True(actual.Count == 0);
        }
    }
}
