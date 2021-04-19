namespace Alpha.MemberInfo.Tests.Services.IndirectLoginAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Criteria.IndirectLoginAccount;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Alpha.MemberInfo.Services;
    using Alpha.MemberInfo.Services.IndirectLoginAccount;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Sunplus.Agent.Utilities;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class IndirectLoginAccountServiceImplTests
    {
        private readonly IndirectLoginAccountService service;
        private readonly IDynamicRepository repository;
        private readonly AesService aes;
        private readonly Encryptor encryptor;
        private readonly CompareLogic compareLogic;

        public IndirectLoginAccountServiceImplTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            aes = Substitute.For<AesService>();
            encryptor = Substitute.For<Encryptor>();
            service = new IndirectLoginAccountServiceImpl(repository, aes, encryptor);
            compareLogic = new CompareLogic();
        }

        [Fact]
        public async Task GetIndirectLoginAccountsAsync_NoData_ReturnsEmptyList()
        {
            // Arrange
            var custId = 1176070;
            repository
                .FetchAsync<IndirectLoginAccountItem>(Arg.Is<GetIndirectLoginAccountsCriteria>(a => a.CustId == custId))
                .Returns(Enumerable.Empty<IndirectLoginAccountItem>());

            // Act
            var actual = await service.GetIndirectLoginAccountsAsync(custId, 0);

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetIndirectLoginAccountsAsync_HasData_ReturnsExpectedList()
        {
            // Arrange
            var custId = 1176070;
            var expected = new List<IndirectLoginAccountItem>
            {
                new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, false, false, null, null, false),
                new IndirectLoginAccountItem(1176086, 0, "agent01", 2, true, true, false, null, null, false),
                new IndirectLoginAccountItem(1775970, 0, "agent99", 2, true, false, true, null, null, false)
            };

            repository
                .FetchAsync<IndirectLoginAccountItem>(Arg.Is<GetIndirectLoginAccountsCriteria>(a => a.CustId == custId))
                .Returns(expected);

            // Act
            var actual = await service.GetIndirectLoginAccountsAsync(custId, 0);

            // Assert
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task ValidateIndirectLoginAccountAsync_IsDisabled_ReturnsCorrectResultMessagse()
        {
            // Arrange
            var custId = 1176070;
            var indirectCustId = 1176083;
            repository
                .GetAsync<IndirectLoginAccountItem>(Arg.Any<GetIndirectLoginAccountsCriteria>())
                .Returns(new IndirectLoginAccountItem());

            // Act
            var actual = await service.ValidateIndirectLoginAccountAsync(custId, 0, indirectCustId, 0);

            // Assert
            Assert.Equal(Resources.IndirectLoginAccountClosedOrDisabledError, actual.Message);
        }

        [Fact]
        public async Task ValidateIndirectLoginAccountAsync_IsClosed_ReturnsCorrectResultMessagse()
        {
            // Arrange
            var custId = 1176070;
            var indirectCustId = 1176083;
            repository
                .GetAsync<IndirectLoginAccountItem>(Arg.Any<GetIndirectLoginAccountsCriteria>())
                .Returns(new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, true, false, null, null, false));

            // Act
            var actual = await service.ValidateIndirectLoginAccountAsync(custId, 0, indirectCustId, 0);

            // Assert
            Assert.Equal(Resources.IndirectLoginAccountClosedOrDisabledError, actual.Message);
        }

        [Fact]
        public async Task ValidateIndirectLoginAccountAsync_LastVerifyPwdIsLessThanLastChangePwdDate_ReturnsCorrectResultCode()
        {
            // Arrange
            var custId = 1176070;
            var indirectCustId = 1176083;
            repository
                .GetAsync<IndirectLoginAccountItem>(Arg.Any<GetIndirectLoginAccountsCriteria>())
                .Returns(new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, false, false, DateTime.Today.AddDays(-1), DateTime.Today, false));

            // Act
            var actual = await service.ValidateIndirectLoginAccountAsync(custId, 0, indirectCustId, 0);

            // Assert
            Assert.Equal(5001, actual.Code);
        }

        [Fact]
        public async Task ValidateIndirectLoginAccountAsync_Valid_ReturnsSuccessResult()
        {
            // Arrange
            var custId = 1176070;
            var indirectCustId = 1176083;
            repository
                .GetAsync<IndirectLoginAccountItem>(Arg.Any<GetIndirectLoginAccountsCriteria>())
                .Returns(new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, false, false, DateTime.Today.AddDays(1), DateTime.Today, false));

            // Act
            var actual = await service.ValidateIndirectLoginAccountAsync(custId, 0, indirectCustId, 0);

            // Assert
            Assert.True(actual.IsSuccessful);
        }

        [Fact]
        public async Task NewAccountAsync_IsOK_ReturnsCorrectMessage()
        {
            // Arrange
            var param = new NewAccountParam(
                "master1",
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                4,
                "en-US",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            aes.Decrypt(param.Password, param.HashKey).Returns("1234aa");
            encryptor.CfsEnCode("1234aa").Returns("56E8A2D78170E87C928E480797CC73");
            repository
                .ExecuteAsync(Arg.Any<NewAccountCriteria>())
                .Returns(Task.CompletedTask);

            // Act
            var actual = await service.NewAccountAsync(param);

            // Assert
            Assert.Equal(Resources.IndirectLoginAccountNewSuccessfully, actual.Message);
        }

        [Fact]
        public async Task UpdatePasswordAsync_IsOK_ReturnsCorrectMessage()
        {
            // Arrange
            var param = new UpdatePasswordParam(
                1176083,
                0,
                "master1",
                3,
                "Fg0Yjs4OUKceI1P9SSxOrw==",
                "qwer",
                1176070,
                0,
                "supertest1",
                "E",
                new RequestInfo("::1", "localhost", "Chrome 81.0.4044, 1920x920", "Windows 10 0.0.0, Device: Other, IsCrawler: False", "-"));
            aes.Decrypt(param.Password, param.HashKey).Returns("1234aa");
            encryptor.CfsEnCode("1234aa").Returns("56E8A2D78170E87C928E480797CC73");
            repository
                .ExecuteAsync(Arg.Any<UpdatePasswordCriteria>())
                .Returns(Task.CompletedTask);

            // Act
            var actual = await service.UpdatePasswordAsync(param);

            // Assert
            Assert.True(actual.IsOk);
        }

        [Fact]
        public async Task DisconnectAsync_IsOK_ReturnsCorrectMessage()
        {
            // Arrange
            var param = new DisconnectAccountParam(1176083, 0, "master1", 1176070, 0, "supertest1", "en-US", "::1");
            repository
                .ExecuteAsync(Arg.Is<DisconnectAccountCriteria>(a =>
                    a.IndirectCustId == param.IndirectCustId
                    && a.IndirectSubAccId == param.IndirectSubAccId
                    && a.IndirectUsername == param.IndirectUsername
                    && a.ActorId == param.ActorId
                    && a.ActorSubAccId == param.ActorSubAccId
                    && a.ActorUsername == param.ActorUsername
                    && a.IP == param.IP
                    && a.Language == param.LanguageKey))
                .Returns(Task.CompletedTask);

            // Act
            var actual = await service.DisconnectAsync(param);

            // Assert
            Assert.Equal(Resources.IndirectLoginAccountDisconnectSuccessfully, actual.Message);
        }
    }
}
