namespace Alpha.MemberInfo.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Services;
    using Alpha.MemberInfo.Services.Transfer;
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
    using AlphaPlus.Utilities.IP;
    using Alpha.Web.Helpers;
    using AutoFixture;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using MemberInfo.Models;
    using NSubstitute;
    using Xunit;
    using Alpha.SecurityCode.Services;

    [Trait("Category", "CustomerList")]
    [Collection("UserCollection")]
    public class CustomerListHandlerTests
    {
        private readonly CustomerListService service;
        private readonly SecurityCodeChecker securityCodeChecker;
        private readonly BrowserHelper browserHelper;
        private readonly CacheRefService cacheRefService;
        private readonly IPService ipService;
        private readonly CustomerListHandler handler;
        private readonly IUser user;
        private readonly Fixture fixture;
        private readonly ISearchAccountInfoRequest request = RequestHelper.CreateRequest(4, 3);
        private readonly Customer actor = new Customer(1, 0, "username1", 4, CustomerHelper.CreateCustomerProductStatus());
        private readonly Customer searchAccount = new Customer(2, 0, "username2", 3, CustomerHelper.CreateCustomerProductStatus());
        private readonly CompareLogic compareLogic = new CompareLogic();
        private readonly TransferService transferService;

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
            ipService = Substitute.For<IPService>();
            transferService = Substitute.For<TransferService>();

            handler = new CustomerListHandler(service, null, null, null, ipService, securityCodeChecker, browserHelper, transferService, cacheRefService);
        }

        [Fact]
        public void HandleCustomerListRequest_Always_ReturnsCorrectViewModel()
        {
            var breadcrumbLevel = new List<CustInfo>() { new CustInfo(10, "Name", 2) };
            var message = new CustomerListRequest(user)
            {
                BreadcrumbLevel = breadcrumbLevel
            };

            cacheRefService.GetProductName(Arg.Any<int>()).Returns("Product");
            browserHelper.IsMobile(isOnlyTablet: false).Returns(true);

            var customerListDTO = new CustomerListDTO(new List<CustomerListModelBase>(), 0, 0);
            service.GetCustomerList(Arg.Any<CustomerListParam>()).Returns(customerListDTO);

            var customerListPagingViewModel = new CustomerListPagingViewModel(
                customerListDTO,
                new FetchDownlinesListRequest(message.Viewer)
                {
                    CustName = message.CustName,
                    CustId = message.CustId,
                    CustLevelId = message.CustLevelId,
                    Username = message.Username,
                    Status = message.Status,
                    DoubleComm = message.DoubleComm,
                    UnconfirmSetting = message.UnconfirmSetting,
                    PageIndex = message.PageIndex,
                    PageSize = message.PageSize
                },
                ipService);

            var expectedViewModel = new CustomerListViewModel(
                message,
                breadcrumbLevel,
                true,
                true,
                customerListPagingViewModel: customerListPagingViewModel);

            var actualViewModel = handler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleCustomerStatusCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerNonProductStatusCommand(user)
            {
                SearchAccountId = 1,
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
            service.GetCustomerStatus(Arg.Any<int>(), Arg.Any<byte>()).Returns(model);
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
            var actualStatus = handler.BuildCustomerProductStatus(actor, searchAccount, message, CreateCustomerStatusModelStub(), false);
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
                new CustomerProductStatus(ProductConstants.ESport, true),
            };

            var actualActorProductStatus = handler.BuildActorProductStatus(Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualActorProductStatus, customerProductStatus).AreEqual);
        }

        [Fact]
        public void BuildSearchAccountProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.ESport, true),
            };

            var actualSearchAccountProduct = handler.BuildSearchAccountProductStatus(Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualSearchAccountProduct, customerProductStatus).AreEqual);
        }

        [Fact]
        public void BuildUplineProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.ESport, true),
            };

            var actualUplineProductStatus = handler.BuildUplineProductStatus(Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualUplineProductStatus, customerProductStatus).AreEqual);
        }

        private IEnumerable<IStatusItemViewModel> StubStatusViewModel()
        {
            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(1);

            return new List<IStatusItemViewModel>
            {
                new ConfirmAllowAutoTransferViewModel(request, null),
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
                    new EditInfoViewModel(request),
                    new RemoveOtpViewModel(request),
                    new ResetPasswordViewModel(request),
                    new ResetSecurityCodeViewModel(request),
                    new TransferViewModel(request),
                }
                .Where(setting => setting.Visible)
                .OrderBy(setting => setting.DisplayOrder);
        }

        private static CustomerStatusModel CreateCustomerStatusModelStub()
            => new CustomerStatusModel(StubNonProductStatus(), StubDcsProducts());

        private static NonProductStatusModel StubNonProductStatus()
        {
            var nonProductStatus = new NonProductStatusModel(
                hasDownline: true,
                disableAutoMPT: true,
                closed: true,
                isDirectClosed: true,
                uplineClosed: true,
                suspended: true,
                isDirectSuspend: true,
                uplineSuspend: true,
                transferOption: "00000000",
                doubleCommLosing: true,
                uplineDoubleCommLosing: true,
                disableOutright: true,
                uplineDisableOutright: true,
                isAllDayBalTransfer: false,
                locked: 0,
                isAllowAutoTransfer: false);
            nonProductStatus.IsProtectedMode = false;

            return nonProductStatus;
        }

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
                                  hasDownline: true,
                                  groupId:1),
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
                                  hasDownline: true,
                                  groupId:1)
                    };
    }
}