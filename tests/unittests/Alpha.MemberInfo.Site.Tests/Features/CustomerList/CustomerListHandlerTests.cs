namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Services;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Site.Tests.Extensions;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting;
    using Alpha.Products.AccountInfo.Setting.ViewModels;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.AccountInfo.Status;
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Models;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using Alpha.Products.Core.ViewModels;
    using Alpha.SecurityCode.Services;
    using Alpha.Web.Helpers;
    using AutoFixture;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Models;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using Xunit;

    [Trait("Category", "CustomerList")]
    [Collection("UserCollection")]
    public class CustomerListHandlerTests
    {
        private readonly CustomerListService service;
        private readonly SecurityCodeChecker securityCodeChecker;
        private readonly BrowserHelper browserHelper;
        private readonly CacheRefService cacheRefService;
        private readonly CustomerListHandler handler;
        private readonly IUser user;
        private readonly Fixture fixture;
        private readonly ISearchAccountInfoRequest request = RequestHelper.CreateRequest(4, 3);
        private readonly Customer actor = new Customer(1, 0, "username1", 4, CustomerHelper.CreateCustomerProductStatus());
        private readonly Customer searchAccount = new Customer(2, 0, "username2", 3, CustomerHelper.CreateCustomerProductStatus());
        private readonly CompareLogic compareLogic = new CompareLogic();

        public CustomerListHandlerTests(UserFixture userFixture)
        {
            var configurationLoader = Substitute.For<IConfigurationLoader>();
            configurationLoader.Load().Returns(AppProductConfigurationHelper.LoadApplicationProductConfiguration());

            SimpleServiceLocator
           .Instance
           .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
           .Register<Func<string, ResourceManager, string>>((language, resources) => "{}")
           .Register<IConfigurationLoader>(configurationLoader)
           .Register<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>(new StatusViewModelFactory())
           .Register<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>(new SettingViewModelFactory())
           .Register<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel>>(new SettingMultipleViewModelFactory())
           .Register<Func<int, string>>((productId) => $"Product Name of {productId}")
           .Register<Func<string, string>>(key => $"value of {key}")
           .Register<Func<bool>>(() => false)
           .Register<Func<string, string, string>>((microSiteName, virtualPath) => $"microSiteName/");

            user = userFixture.User;
            fixture = new Fixture();
            service = Substitute.For<CustomerListService>();
            securityCodeChecker = Substitute.For<SecurityCodeChecker>();
            browserHelper = Substitute.For<BrowserHelper>();
            cacheRefService = Substitute.For<CacheRefService>();

            handler = new CustomerListHandler(service, null, null, null, securityCodeChecker, browserHelper, cacheRefService);
        }

        [Fact]
        public void HandleCustomerStatusCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerNonProductStatusCommand(user)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            var model = new Result() { Code = 0, Message = string.Empty };
            service.UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>()).Returns(model);

            var actualViewModel = handler.Handle(message);

            AssertExtension.Equal(actualViewModel, model);
        }

        [Fact]
        public void HandleCustomerAllStatusQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerStatusQuery(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var expectedSetting = StubSettingViewModel();
            var expectedStatus = StubStatusViewModel();

            var model = fixture.Create<CustomerStatusModel>();
            service.GetCustomerStatus(Arg.Any<int>()).Returns(model);
            var expectedViewModel = new CustomerStatusViewModel(expectedStatus, expectedSetting);

            var actualViewModel = handler.Handle(message);

            Assert.True(compareLogic.Compare(actualViewModel, expectedViewModel).AreEqual);
        }

        [Fact]
        public void BuildCustomerProductSettings_Always_ReturnsCorrectSettingViewModel()
        {
            var message = new CustomerStatusQuery(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var expectedSetting = StubSettingViewModel();

            var actualSettings = handler.BuildCustomerProductSettings(actor, searchAccount, message, CreateCustomerStatusModelStub());
            Assert.True(compareLogic.Compare(actualSettings, expectedSetting).AreEqual);
        }

        [Fact]
        public void BuildCustomerProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var message = new CustomerStatusQuery(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var expectedStatus = StubStatusViewModel();

            var actualStatus = handler.BuildCustomerProductStatus(actor, searchAccount, message, CreateCustomerStatusModelStub());
            Assert.True(compareLogic.Compare(actualStatus, expectedStatus).AreEqual);
        }

        [Fact]
        public void BuildActorProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var message = new CustomerStatusQuery(user)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, true),
                new CustomerProductStatus(ProductConstants.RNGCasino, true, synced: true),
                new CustomerProductStatus(ProductConstants.ESport, true),
                new CustomerProductStatus(ProductConstants.Keno, hidden: true),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, true, true)
            };

            var actualActorProductStatus = handler.BuildActorProductStatus(message, Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualActorProductStatus, customerProductStatus).AreEqual);
        }

        [Fact]
        public void BuildSearchAccountProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, true),
                new CustomerProductStatus(ProductConstants.RNGCasino, true, synced: true),
                new CustomerProductStatus(ProductConstants.ESport, true),
                new CustomerProductStatus(ProductConstants.Keno, hidden: true),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, true, true)
            };

            var actualSearchAccountProduct = handler.BuildSearchAccountProductStatus(StubNonDCSProduct(), Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualSearchAccountProduct, customerProductStatus).AreEqual);
        }

        [Fact]
        public void BuildUplineProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, true),
                new CustomerProductStatus(ProductConstants.RNGCasino, true, synced: true),
                new CustomerProductStatus(ProductConstants.ESport, true),
                new CustomerProductStatus(ProductConstants.Keno, hidden: true),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, true, true)
            };

            var actualUplineProductStatus = handler.BuildUplineProductStatus(StubNonDCSProduct(), Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualUplineProductStatus, customerProductStatus).AreEqual);
        }

        private IEnumerable<IStatusItemViewModel> StubStatusViewModel()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(1);

            return new List<IStatusItemViewModel>
            {
                new ConfirmAllowAutoMasterPTViewModel(request),
                new ConfirmAllowOutrightViewModel(request),
                new ConfirmClosedViewModel(request),
                new ConfirmDoubleCommViewModel(request),
                new ConfirmSuspendedViewModel(request),
                new  ProductStatusItemViewModel(request, configItem)
            }
             .Where(status => status.Visible)
             .OrderBy(status => status.DisplayOrder);
        }

        private IEnumerable<ISettingItemViewModel> StubSettingViewModel()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(1);

            return new List<ISettingItemViewModel>
                {
                    new BetSettingViewModel(request),
                    new CommissionViewModel(request),
                    new EditInfoViewModel(request),
                    new RemoveOtpViewModel(request),
                    new ResetPasswordViewModel(request),
                    new ResetSecurityCodeViewModel(request),
                    new SportsbookMinPTViewModel(request),
                    new TransferViewModel(request),
                    new SportsbookViewModel(request, configItem)
                }
                .Where(setting => setting.Visible)
                .OrderBy(setting => setting.DisplayOrder);
        }

        private static CustomerStatusModel CreateCustomerStatusModelStub()
            => new CustomerStatusModel(StubNonProductStatus(), StubNonDCSProduct(), StubDcsProducts());

        private static NonDCSProductStatusModel StubNonDCSProduct()
            => new NonDCSProductStatusModel(
                       syncCasino: true,
                       disableCasino: true,
                       uplineDisableCS: true,
                       disableNumber: true,
                       uplineDisableNumber: true,
                       disableEsports: true,
                       uplineDisableEsports: true,
                       hideKeno: true,
                       uplineHideGoldDeluxe: true,
                       uplineDisableGoldDeluxe: true,
                       disableGoldDeluxe: true,
                       hideGoldDeluxe: true);

        private static NonProductStatusModel StubNonProductStatus()
            => new NonProductStatusModel(
                             hasDownline: true,
                             disableAutoMPT: true,
                             closed: true,
                             uplineClosed: true,
                             suspended: true,
                             isDirectSuspend: true,
                             uplineSuspend: true,
                             transferOption: "00000000",
                             doubleCommLosing: true,
                             uplineDoubleCommLosing: true,
                             disableOutright: true,
                             uplineDisableOutright: true,
                             isAllDayBalTransfer: false);

        private static List<DCSProductStatusModel> StubDcsProducts()
            => new List<DCSProductStatusModel>
                 {
                     new DCSProductStatusModel(
                                  productId: 5,
                                  sportType: 12,
                                  betType: 24,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true),
                     new DCSProductStatusModel(
                                  productId: 6,
                                  sportType: 13,
                                  betType: 26,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true)
                    };
    }
}