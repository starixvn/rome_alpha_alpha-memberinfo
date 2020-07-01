namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.IndirectLoginAccount;
    using Alpha.MemberInfo.Params.IndirectLoginAccount;
    using Alpha.MemberInfo.Services.IndirectLoginAccount;
    using Alpha.MemberInfo.Site.Handlers;
    using Alpha.MemberInfo.Site.Requests.IndirectLoginAccount;
    using Alpha.Web.Helpers;
    using NSubstitute;
    using Sunplus.Agent.Utilities;
    using Xunit;

    [Trait("Category", "IndirectLoginAccount")]
    public class IndirectLoginAccountHandlerTests
    {
        private const string HashKeySessionKey = "Alpha.MemberInfo.Site.Handlers.IndirectLoginAccountHandler.PasswordHash";

        private readonly IUser user;
        private readonly IndirectLoginAccountService service;
        private readonly SessionManager sessionManager;
        private readonly Encryptor encryptor;
        private readonly IndirectLoginAccountHandler handler;

        public IndirectLoginAccountHandlerTests()
        {
            user = Substitute.For<IUser>();
            user.Id.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Name.Returns("master1");
            user.IP.Returns("127.0.0.1");
            user.LanguageKey.Returns("E");

            service = Substitute.For<IndirectLoginAccountService>();
            sessionManager = Substitute.For<SessionManager>();
            encryptor = Substitute.For<Encryptor>();
            handler = new IndirectLoginAccountHandler(service, sessionManager, encryptor);
        }

        [Fact]
        public async Task HandlerManageAccountQuery_NoData_ReturnsEmptyList()
        {
            // Arrange
            var message = new ManageAccountQuery(user);
            service
                .GetIndirectLoginAccountsAsync(Arg.Is<int>(a => a == message.ViewerId), Arg.Is<int>(a => a == message.SubAccId))
                .Returns(Enumerable.Empty<IndirectLoginAccountItem>());

            // Act
            var actual = await handler.Handle(message);

            // Assert
            Assert.Empty(actual.Accounts);
        }

        [Fact]
        public async Task HandlerManageAccountQuery_HasData_ReturnsEmptyList()
        {
            // Arrange
            var message = new ManageAccountQuery(user);
            var expected = new List<IndirectLoginAccountItem>
            {
                new IndirectLoginAccountItem(1176083, 0, "master1", 3, true, false, false, null, null),
                new IndirectLoginAccountItem(1176086, 0, "agent01", 2, true, true, false, null, null),
                new IndirectLoginAccountItem(1775970, 0,"agent99", 2, true, false, true, null, null)
            };
            service
                .GetIndirectLoginAccountsAsync(Arg.Is<int>(a => a == message.ViewerId), Arg.Is<int>(a => a == message.SubAccId))
                .Returns(expected);

            // Act
            var actual = await handler.Handle(message);

            // Assert
            Assert.NotEmpty(actual.Accounts);
        }

        [Fact]
        public async Task HandlerValidateAccountQuery_Always_ReturnsResult()
        {
            // Arrange
            var message = new ValidateAccountQuery(user);
            service
                .ValidateIndirectLoginAccountAsync(message.ViewerId, message.ViewerSubAccId, message.CustId, message.SubAccId)
                .Returns(new Result());

            // Act
            var actual = await handler.Handle(message);

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void HandlerNewAccountQuery_Always_ReturnsHashKey()
        {
            // Arrange
            var expected = "uklp";
            encryptor.RandomString(4).Returns(expected);

            // Act
            var actual = handler.Handle(new NewAccountQuery());

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HandlerUpdatePasswordQuery_Always_ReturnsViewModel()
        {
            // Arrange
            var hashKey = "uklp";
            encryptor.RandomString(4).Returns(hashKey);
            var message = new UpdatePasswordQuery(user);

            // Act
            var actual = handler.Handle(message);

            // Assert
            Assert.True(message.CustId == actual.CustId
                && message.CustName == actual.CustName
                && hashKey == actual.HashKey);
        }

        [Fact]
        public async Task HandlerDisconnectCommand_Always_ReturnsCorrectMessage()
        {
            // Arrange
            var message = new DisconnectCommand(user);
            service
                .DisconnectAsync(Arg.Is<DisconnectAccountParam>(a =>
                    a.IndirectCustId == message.CustId
                    && a.IndirectSubAccId == message.SubAccId
                    && a.IndirectUsername == message.CustName
                    && a.ActorId == message.ViewerId
                    && a.ActorSubAccId == message.ViewerSubAccId
                    && a.ActorUsername == message.ViewerUsername
                    && a.IP == message.IP
                    && a.LanguageKey == message.LanguageKey))
                .Returns(new Result { Message = Resources.IndirectLoginAccountDisconnectSuccessfully });

            // Act
            var actual = await handler.Handle(message);

            // Assert
            Assert.Equal(Resources.IndirectLoginAccountDisconnectSuccessfully, actual.Message);
        }
    }
}
