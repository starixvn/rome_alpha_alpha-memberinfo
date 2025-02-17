namespace Alpha.MemberInfo.Site.Tests.Handlers.MemberRequests
{
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Features.Shared.Params;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Helpers;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.Requests;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn.ViewModels;
    using AutoFixture;
    using Constants;
    using Extensions;
    using Fanex.Customer.Client.Models;
    using Fanex.Customer.Client.Services;
    using Fixtures;
    using MemberInfo.Models;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    [Collection("UserCollection")]
    public class QRCodeMemberSignInHandlerTests
    {
        private readonly QRCodeMemberSignInService qrCodeMemberSignInService;
        private readonly QRCodeLoginConfigsHelper qrCodeLoginConfigsHelper;
        private readonly QrCodeAdapter qrCodeAdapter;
        private readonly PasswordService passwordService;
        private readonly CheckBelongingService checkBelongingService;
        private readonly QRCodeMemberSignInHandler handler;
        private readonly IUser user;
        private readonly Fixture fixture;
        private readonly ICustomerService customerService;

        public QRCodeMemberSignInHandlerTests(UserFixture userFixture)
        {
            user = userFixture.User;
            fixture = new Fixture();
            qrCodeMemberSignInService = Substitute.For<QRCodeMemberSignInService>();
            passwordService = Substitute.For<PasswordService>();
            checkBelongingService = Substitute.For<CheckBelongingService>();
            qrCodeLoginConfigsHelper = Substitute.For<QRCodeLoginConfigsHelper>();
            qrCodeAdapter = Substitute.For<QrCodeAdapter>();
            customerService = Substitute.For<ICustomerService>();

            handler = new QRCodeMemberSignInHandler(qrCodeAdapter, customerService, qrCodeMemberSignInService, passwordService, checkBelongingService, qrCodeLoginConfigsHelper);

            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageCode.English);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        [Fact]
        public async void HandlerQRCodeMemberSignInRequest_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new QRCodeMemberSignInRequest(user)
            {
                MemberName = "test",
                Credit = 100,
                ShowPageTitle = false,
                ErrorMessage = string.Empty
            };

            customerService.CheckAllLimitedRules(message.ViewerId, message.ViewerLevelId, message.ActorUsername, false, message.Language).Returns(Task.FromResult(new Fanex.Customer.Client.Models.Result() { Code = 0, Message = "Success" }));
            customerService.GetSuggestUsername(Arg.Any<string>(), Arg.Any<byte>()).Returns(Task.FromResult("Suggestion Name"));

            var expectedViewModel = new QRCodeMemberSignInViewModel(
                message,
                "Suggestion Name",
                true);

            // Act
            var actualViewModel = await handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel.MemberNameSuffix, actualViewModel.MemberNameSuffix);
            AssertExtension.Equal(expectedViewModel.Credit, actualViewModel.Credit);
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

            customerService.CheckAllLimitedRules(message.ViewerId, message.ViewerLevelId, message.ActorUsername, false, message.Language).Returns(Task.FromResult(new Fanex.Customer.Client.Models.Result()));

            qrCodeMemberSignInService.GetCreditAndStatus(Arg.Any<string>()).Returns(new CreditAndStatusModel(100, 10, 1000, "Open"));

            customerService.GetDownlineByUsername(
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<string>()).Returns(
                Task.FromResult(new Downline
                {
                    CustId = custIdCopyFrom,
                }));

            customerService.IsCustomerEnabled(Arg.Any<int>()).Returns(Task.FromResult(true));

            passwordService.CreateMemberPasswordRandom().Returns(new PasswordModel("AA", "abcd"));

            //var result = new ResultCreateMemberQRCode
            //{
            //    CustId = 1,
            //    CustIdCopyFrom = custIdCopyFrom,
            //    Success = true,
            //    ErrCode = 0,
            //    ErrMsg = string.Empty,
            //    RawPassword = "AA"
            //};

            //customerService.CopyCustomer(Arg.Any<CopyCustomerParam>()).Returns(Task.FromResult(result));

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            //AssertExtension.Equal(result, actualViewModel.Result);
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

            var invalidCustIdCopyFrom = -1;
            qrCodeMemberSignInService.GetCreditAndStatus(Arg.Any<string>()).Returns(new CreditAndStatusModel(100, 10, 1000, "Open"));

            customerService.GetDownlineByUsername(
                Arg.Any<int>(),
                Arg.Any<int>(),
                Arg.Any<string>()).Returns(
                Task.FromResult(new Downline
                {
                    CustId = invalidCustIdCopyFrom,
                }));

            var result = new ResultCreateMemberQRCode
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = "This username is not available."
            };

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(result, actualViewModel.Result);
        }

        [Fact]
        public void HandlerCreateAccountByQRCodeRequest_SuspendedCopyFrom_ReturnsErrorResult()
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

            //qrCodeMemberSignInService.CheckLimitNewDownline(message.ViewerId, message.ViewerLevelId, message.Language, message.ActorUsername).Returns(new Result());
            qrCodeMemberSignInService.GetCreditAndStatus(Arg.Any<string>()).Returns(new CreditAndStatusModel(100, 10, 1000, "Closed"));
            var result = new ResultCreateMemberQRCode ////
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = "You can not copy setting from closed/ suspended account."
            };

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(result, actualViewModel.Result);
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
            qrCodeMemberSignInService.GetCreditAndStatus(Arg.Any<string>()).Returns(new CreditAndStatusModel(100, 10, 1000, "Open"));
            customerService.GetDownlineByUsername(message.CustId, message.SubAccId, message.UsernameCopyFrom)
                .Returns(new Downline() { CustId = custIdCopyFrom, IsActive = false, IsSubAcc = false, RoleId = 1 });

            // customerService.CheckLimitNewDownline(message.ViewerId, message.ViewerLevelId, message.Language, message.ActorUsername).Returns(new Result());
            //
            // customerService.CheckBelongingByUsername(
            //     Arg.Any<int>(),
            //     Arg.Any<string>(),
            //     Arg.Any<int>()).Returns(
            //     new CheckBelongingByUsernameModel(true, custIdCopyFrom, false, 1));

            customerService.IsCustomerEnabled(Arg.Any<int>()).Returns(false);

            var result = new ResultCreateMemberQRCode
            {
                Success = false,
                ErrCode = 100,
                ErrMsg = "This username is not available."
            };

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(result, actualViewModel.Result);
        }

        [Fact]
        public async void HandlerGetMemberCreditRequest_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new GetMemberCreditAndStatusRequest(user)
            {
                SearchAccountName = "test"
            };

            var model = new CreditAndStatusModel(100, 10, 1000, "open");
            customerService.GetDownlineByUsername(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(new Downline() { CustId = 1, IsActive = false, IsSubAcc = false, RoleId = 1 });
            qrCodeMemberSignInService.GetCreditAndStatus(Arg.Any<string>()).Returns(model);
            //qrCodeMemberSignInService.CheckBelongingByUsername(
            //  Arg.Any<int>(),
            //  Arg.Any<string>(),
            //  Arg.Any<int>()).Returns(
            //  new CheckBelongingByUsernameModel(true, 999, false, 1));
            // Act
            var actualViewModel = await handler.Handle(message);

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
                CreditMax = 1000,
                CreateMode = true,
                RawPassword = "ABC"
            };
            var belongingInfo = new BelongingInfo(true, 1, string.Empty);

            checkBelongingService.GetBelongingInfo(Arg.Any<CheckBelongingParam>()).Returns(belongingInfo);
            qrCodeLoginConfigsHelper.LocalTest().Returns(true);
            qrCodeMemberSignInService.IsNotFirstLoginByQRCode(Arg.Any<int>()).Returns(true);

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
                "linktest.com",
                "imageBase64",
                domains,
                message.CreateMode,
                message.ShowPageTitle,
                "ABC");

            qrCodeAdapter.CreateMobileLoginUrl(
             Arg.Any<bool>(),
             Arg.Any<string>(),
             Arg.Any<int>(),
             Arg.Any<string>(),
             Arg.Any<int>()).Returns("linktest.com");
            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel.Credit, actualViewModel.Credit);
            AssertExtension.Equal(expectedViewModel.CreditMin, actualViewModel.CreditMin);
            AssertExtension.Equal(expectedViewModel.CreditMax, actualViewModel.CreditMax);
            AssertExtension.Equal(expectedViewModel.SearchAccountName, actualViewModel.SearchAccountName);
            AssertExtension.Equal(expectedViewModel.FirstName, actualViewModel.FirstName);
            AssertExtension.Equal(expectedViewModel.CreateMode, actualViewModel.CreateMode);
            AssertExtension.Equal(expectedViewModel.RawPassword, actualViewModel.RawPassword);
        }

        [Fact]
        public void HandlerShowQRCodeRequest_FirstLogin_ReturnsNull()
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
            var belongingInfo = new BelongingInfo(true, 1, string.Empty);

            checkBelongingService.GetBelongingInfo(Arg.Any<CheckBelongingParam>()).Returns(belongingInfo);
            qrCodeMemberSignInService.IsNotFirstLoginByQRCode(Arg.Any<int>()).Returns(false);

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
                new QRCodeLoginConfigs("192.468.1.1", new string[] { "domain.com", "domain1.com" }, 1, 3600));

            qrCodeMemberSignInService.IsNotFirstLoginByQRCode(Arg.Any<int>()).Returns(true);

            qrCodeAdapter.CreateMobileLoginUrl(
                Arg.Any<bool>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<string>(),
                Arg.Any<int>()).Returns("linktest.com");

            var qrCodeImage = "imageBase64";
            qrCodeAdapter.Generate(Arg.Any<string>()).Returns(qrCodeImage);
            var expectedViewModel = new QRCodeLinkViewModel("linktest.com", qrCodeImage);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(expectedViewModel, actualViewModel);
        }

        [Fact]
        public void HandlerQGenerateQRCodeRequest_FirstLogin_ReturnsDefaultViewModel()
        {
            // Arrange
            var message = new GenerateQRCodeRequest(user)
            {
                SearchAccountName = "test",
                Domain = "domain.com"
            };

            qrCodeMemberSignInService.IsNotFirstLoginByQRCode(Arg.Any<int>()).Returns(false);

            // Act
            var actualViewModel = handler.Handle(message);

            // Assert
            AssertExtension.Equal(new QRCodeLinkViewModel(), actualViewModel);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 2, false)]
        public async void IsValidBelongingByUsername_Always_ReturnsCorrectViewModel(
            int custId,
            byte roleId,
            bool expectedResult)
        {
            // Arrange

            customerService.GetDownlineByUsername(
              Arg.Any<int>(),
              Arg.Any<int>(),
              Arg.Any<string>()).Returns(
              new Downline() { CustId = custId, IsActive = true, IsSubAcc = false, RoleId = roleId });

            // Act
            var actualResult = await handler.IsValidBelongingByUsername(custId, "test", roleId);

            // Assert
            AssertExtension.Equal(expectedResult, actualResult);
        }
    }
}