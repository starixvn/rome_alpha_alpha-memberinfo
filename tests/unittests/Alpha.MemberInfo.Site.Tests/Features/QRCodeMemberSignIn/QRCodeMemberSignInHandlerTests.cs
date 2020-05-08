namespace Alpha.MemberInfo.Site.Tests.Handlers.MemberRequests
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using Constants;
    using Extensions;
    using Fixtures;
    using MemberInfo.Models;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    [Collection("UserCollection")]
    public class QRCodeMemberSignInHandlerTests
    {
        private readonly QRCodeMemberSignInService service;
        private readonly QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper;
        private readonly QrCodeAdapter qrCodeAdapter;
        private readonly PasswordService passwordService;
        private readonly QRCodeMemberSignInHandler handler;
        private readonly IUser user;
        private readonly Fixture fixture;

        public QRCodeMemberSignInHandlerTests(UserFixture userFixture)
        {
            user = userFixture.User;
            fixture = new Fixture();
            service = Substitute.For<QRCodeMemberSignInService>();
            passwordService = Substitute.For<PasswordService>();
            qrCodeLoginConfigsHelper = Substitute.For<QRCodeLoginConfigsHelper>();
            qrCodeAdapter = Substitute.For<QrCodeAdapter>();

            handler = new QRCodeMemberSignInHandler(qrCodeAdapter, service, passwordService, qrCodeLoginConfigsHelper);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        [Fact]
        public void HandlerQRCodeMemberSignInRequest_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new QRCodeMemberSignInRequest(user)
            {
                MemberName = "test",
                Credit = 100,
                ShowPageTitle = false,
                ErrorMessage = string.Empty
            };

            service.GetSuggestUsername(Arg.Any<string>(), Arg.Any<int>()).Returns("Suggestion Name");

            var expectedViewModel = new QRCodeMemberSignInViewModel(
                message.MemberName,
                message.CustName,
                message.Credit,
                "Suggestion Name",
                false,
                string.Empty);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandlerUserNameSuggestionQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new UserNameSuggestionQuery(user)
            {
                SuggestionQuery = "AA"
            };

            var model = new List<UsernameSuggestion>
            {
                new UsernameSuggestion("test1", "--", "--", "NickName1"),
                new UsernameSuggestion("test2", "--", "--", "NickName2")
            };

            service.GetUsernamesSuggestion(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(model, actualViewModel);
        }

        [Fact]
        public void HandlerCreateAccountByQRCodeRequest_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = "User",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            var custIdCopyFrom = 999;

            service.CheckBelongingByUsername(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>()).Returns(
                new CheckBelongingByUsernameModel(true, custIdCopyFrom, false, 1));

            service.IsEnableCustomer(Arg.Any<int>()).Returns(true);

            passwordService.CreateMemberPasswordRandom().Returns(new PasswordModel("AA", "abcd"));

            var result = new ResultCreateMemberQRCode
            {
                CustId = 1,
                CustIdCopyFrom = custIdCopyFrom,
                Success = true,
                ErrCode = 0,
                ErrMsg = string.Empty,
                RawPassword = "AA"
            };

            service.CreateMemberByQRCode(Arg.Any<InsertMemberCommand>()).Returns(result);

            var sportBookSettingModel = fixture.Create<SportBookSettingModel>();
            service.GetExistedMemberSetting(Arg.Any<int>()).Returns(sportBookSettingModel);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(result, actualViewModel);
        }

        [Fact]
        public void HandlerCreateAccountByQRCodeRequest_InvalidCustIdFrom_ReturnsErrorResult()
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = "User",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            var custIdCopyFrom = -1;

            service.CheckBelongingByUsername(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>()).Returns(
                new CheckBelongingByUsernameModel(true, custIdCopyFrom, false, 2));

            var result = new ResultCreateMemberQRCode
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = "Username is invalid."
            };

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(result, actualViewModel);
        }

        [Fact]
        public void HandlerCreateAccountByQRCodeRequest_CustIdFromIsDisabled_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new CreateAccountByQRCodeRequest(user)
            {
                UsernameCopyFrom = "User",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000,
                FirstSuffix = "A",
                SecondSuffix = "A",
                ThirdSuffix = "A",
                FirstName = "James",
                ShowPageTitle = true
            };

            var custIdCopyFrom = 999;

            service.CheckBelongingByUsername(
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>()).Returns(
                new CheckBelongingByUsernameModel(true, custIdCopyFrom, false, 1));

            service.IsEnableCustomer(Arg.Any<int>()).Returns(false);

            var result = new ResultCreateMemberQRCode
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = "Username is invalid."
            };

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(result, actualViewModel);
        }

        [Fact]
        public void HandlerGetMemberCreditRequest_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new GetMemberCreditRequest(user)
            {
                SearchAccountName = "test"
            };

            var model = new CreditRangeModel(100, 10, 1000);
            service.GetCreditRange(Arg.Any<string>()).Returns(model);

            service.CheckBelongingByUsername(
              Arg.Any<int>(),
              Arg.Any<string>(),
              Arg.Any<int>()).Returns(
              new CheckBelongingByUsernameModel(true, 999, false, 1));
            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(model, actualViewModel);
        }

        [Fact]
        public void HandlerShowQRCodeRequest_ValidFirstLogin_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ShowQRCodeRequest(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "user test",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000
            };

            qrCodeLoginConfigsHelper.LocalTest().Returns(true);
            service.CheckFirstLoginByQRCode(Arg.Any<int>()).Returns(false);

            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            service.GetRnGStatus(Arg.Any<string>()).Returns(new SelectRnGStatusModel(true));
            qrCodeAdapter.Generate(Arg.Any<string>()).Returns("imageBase64");

            var domains = new string[] { "domain.com", "domain1.com" };
            qrCodeLoginConfigsHelper.GetQRCodeMemberSiteLoginConfigs().Returns(
               new QRCodeLoginConfigs("192.468.1.1", domains, 1, 3600));

            passwordService.CreateMemberPasswordRandom().Returns(new PasswordModel("ABC", "abcd"));

            var expectedViewModel = new ShowQRCodeViewModel(
                message.Credit,
                message.CreditMin,
                message.CreditMax,
                message.SearchAccountName,
                message.SearchAccountId,
                message.FirstName,
                "imageBase64",
                string.Empty,
                domains,
                message.CreateMode,
                message.ShowPageTitle,
                true,
                message.CustIdCopyFrom,
                "ABC");

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandlerShowQRCodeRequest_InvalidFirstLogin_ReturnsNull()
        {
            // Arrange
            var message = new ShowQRCodeRequest(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "user test",
                Credit = 100,
                CreditMin = 10,
                CreditMax = 1000
            };

            qrCodeLoginConfigsHelper.LocalTest().Returns(true);
            service.CheckFirstLoginByQRCode(Arg.Any<int>()).Returns(true);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(null, actualViewModel);
        }

        [Fact]
        public void HandlerQGenerateQRCodeRequest_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new GenerateQRCodeRequest(user)
            {
                SearchAccountName = "test",
                Domain = "domain.com"
            };

            qrCodeLoginConfigsHelper.GetQRCodeMemberSiteLoginConfigs().Returns(
                new QRCodeLoginConfigs("192.468.1.1", new string[] { "domain.com", "domain1.com" }, 1 , 3600));

            qrCodeAdapter.CreateMobileLoginUrl(
                Arg.Any<bool>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>()).Returns("linktest.com");

            var expectedViewModel = "imageBase64";
            qrCodeAdapter.Generate(Arg.Any<string>()).Returns(expectedViewModel);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 2, false)]
        public void IsValidBelongingByUsername_Always_ReturnsCorrectViewModel(
            int custId,
            byte roleId,
            bool expectedResult)
        {
            // Arrange

            service.CheckBelongingByUsername(
              Arg.Any<int>(),
              Arg.Any<string>(),
              Arg.Any<int>()).Returns(
              new CheckBelongingByUsernameModel(true, custId, false, roleId));
            
            // Act
            var actualResult = handler.IsValidBelongingByUsername(custId, "test", roleId );

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }
    }
}