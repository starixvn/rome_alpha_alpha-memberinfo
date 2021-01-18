namespace Alpha.MemberInfo.Tests.Feature.DormantAccountList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.DormantAccountList;
    using Alpha.MemberInfo.Features.DormantAccountList.Models;
    using Alpha.MemberInfo.Features.DormantAccountList.Params;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "DormantAccountList")]
    public class DormantAccountServiceTests
    {
        private readonly IDynamicRepository repository;
        private readonly DormantAccountService service;

        public DormantAccountServiceTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            service = new DormantAccountServiceImpl(repository);
        }

        [Fact]
        public async Task GetDormantAccountsAsync_NoData_ReturnsEmptyModel()
        {
            // Arrange
            var param = new DormantAccountParam(1176070, null, 0, 0, 1, 500);
            repository
                .FetchMultipleAsync<DormantAccountEntity, YesterdayBalanceEntity>(Arg.Any<ICriteria>())
                .Returns(new Tuple<IEnumerable<DormantAccountEntity>, IEnumerable<YesterdayBalanceEntity>>(Enumerable.Empty<DormantAccountEntity>(), Enumerable.Empty<YesterdayBalanceEntity>()));

            // Act
            var actual = await service.GetDormantAccountsAsync(param);

            // Assert
            Assert.True(
                !actual.DormantAccounts.Any()
                && !actual.Total.ToBeClearedGivenCredit.HasValue
                && !actual.Total.ClearedGivenCredit.HasValue
                && 0 == actual.Total.YesterdayBalance
                && 0 == actual.TotalItems
                && !actual.HasData);
        }

        [Fact]
        public async Task GetDormantAccountsAsync_HasData_ReturnsCorrectModel()
        {
            // Arrange
            var param = new DormantAccountParam(1176070, null, 0, 0, 1, 500);
            repository
                .FetchMultipleAsync<DormantAccountEntity, YesterdayBalanceEntity>(Arg.Any<ICriteria>())
                .Returns(new Tuple<IEnumerable<DormantAccountEntity>, IEnumerable<YesterdayBalanceEntity>>(FakeDormantAccountEntityList(), FakeYesterdayBalanceEntityList()));

            // Act
            var actual = await service.GetDormantAccountsAsync(param);

            // Assert
            Assert.True(
                actual.DormantAccounts.Any()
                && 1000 == actual.Total.ToBeClearedGivenCredit
                && 3000 == actual.Total.ClearedGivenCredit
                && -3000 == actual.Total.YesterdayBalance);
        }

        private static IEnumerable<DormantAccountEntity> FakeDormantAccountEntityList()
        {
            yield return new DormantAccountEntity(1, "SP0101", 3, 3, 1, 1000, null, new DateTime(2020, 6, 5), new DateTime(2020, 9, 15, 15, 33, 0), null, null, false);
            yield return new DormantAccountEntity(2, "SP010301", 2, 3, 1, 0, null, new DateTime(2020, 6, 2), new DateTime(2020, 9, 15, 15, 33, 0), null, null, true);
            yield return new DormantAccountEntity(3, "SP010301001", 1, 3, 1, 0, null, new DateTime(2020, 12, 2), new DateTime(2020, 9, 15, 15, 33, 0), null, null, true);
            yield return new DormantAccountEntity(4, "SP010301002", 1, 3, 1, 0, null, new DateTime(2020, 12, 2), new DateTime(2020, 9, 15, 15, 33, 0), null, null, false);
            yield return new DormantAccountEntity(5, "SP102", 3, 4, 2, null, 2000, new DateTime(2020, 2, 22), new DateTime(2020, 6, 15, 15, 15, 0), new DateTime(2020, 7, 15, 15, 15, 0), null, false);
            yield return new DormantAccountEntity(6, "SP10207", 2, 4, 2, null, 1000, new DateTime(2020, 2, 22), new DateTime(2020, 6, 15, 15, 15, 0), new DateTime(2020, 7, 15, 15, 15, 0), null, false);
            yield return new DormantAccountEntity(7, "SP10209", 2, 4, 2, null, null, new DateTime(2020, 2, 22), new DateTime(2020, 6, 15, 15, 15, 0), new DateTime(2020, 7, 15, 15, 15, 0), null, false);
            yield return new DormantAccountEntity(8, "SP10302", 2, 4, 3, null, 0, new DateTime(2020, 2, 28), new DateTime(2020, 6, 15, 15, 15, 0), new DateTime(2020, 7, 15, 15, 15, 0), null, false);
            yield return new DormantAccountEntity(9, "SP010401", 2, 4, 3, null, 0, new DateTime(2020, 2, 3), new DateTime(2020, 5, 15, 15, 15, 0), new DateTime(2020, 6, 15, 19, 1, 0), new DateTime(2020, 9, 22, 16, 1, 0), false);
        }

        private static IEnumerable<YesterdayBalanceEntity> FakeYesterdayBalanceEntityList()
        {
            yield return new YesterdayBalanceEntity(1, 0);
            yield return new YesterdayBalanceEntity(2, 2000);
            yield return new YesterdayBalanceEntity(3, 0);
            yield return new YesterdayBalanceEntity(4, 0);
            yield return new YesterdayBalanceEntity(5, -3000);
            yield return new YesterdayBalanceEntity(6, -3000);
            yield return new YesterdayBalanceEntity(7, -3000);
            yield return new YesterdayBalanceEntity(8, 4000);
            yield return new YesterdayBalanceEntity(9, 0);
        }
    }
}
