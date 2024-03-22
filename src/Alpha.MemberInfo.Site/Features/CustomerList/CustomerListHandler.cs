namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services.Transfer;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.Core.Models;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using Alpha.Products.Core.ViewModels;
    using AlphaPlus.Utilities.IP;
    using Alpha.Web.Helpers;
    using Fanex.Common;
    using MediatR;
    using Alpha.SecurityCode.Services;

    public class CustomerListHandler :
        IRequestHandler<CustomerListRequest, CustomerListViewModel>,
        IRequestHandler<FetchDownlinesListRequest, CustomerListPagingViewModel>,
        IRequestHandler<CustomerStatusQuery, CustomerStatusViewModel>,
        IRequestHandler<CustomerStatusMultipleQuery, IEnumerable<ISettingItemMultipleViewModel>>,
        IRequestHandler<CustomerNonProductStatusCommand, Result>,
        IRequestHandler<MultipleDoubleCommRequest, MultipleDoubleCommViewModel>,
        IRequestHandler<UpdateMultipleDoubleCommCommand, IEnumerable<Result>>
    {
        private readonly CustomerListService customerListService;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel> settingMultipleViewModelFactory;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory;
        private readonly GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory;
        private readonly SecurityCodeChecker securityCodeChecker;
        private readonly BrowserHelper browserHelper;
        private readonly IPService ipService;
        private readonly TransferService transferService;

        public CustomerListHandler(
            CustomerListService customerListService,
            SecurityCodeChecker securityCodeChecker,
            IPService ipService,
            BrowserHelper browserHelper,
            TransferService transferService)
            : this(customerListService, null, null, null, ipService, securityCodeChecker, browserHelper, transferService)
        {
        }

        internal CustomerListHandler(
            CustomerListService customerListService,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel> settingMultipleViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory,
            IPService ipService,
            SecurityCodeChecker securityCodeChecker,
            BrowserHelper browserHelper,
            TransferService transferService)
        {
            this.customerListService = customerListService;

            this.settingMultipleViewModelFactory = settingMultipleViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel>>();

            this.settingViewModelFactory = settingViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>();

            this.statusViewModelFactory = statusViewModelFactory ??
                 SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>();

            this.ipService = ipService;
            this.securityCodeChecker = securityCodeChecker;
            this.browserHelper = browserHelper;
            this.transferService = transferService;
        }

        public CustomerListViewModel Handle(CustomerListRequest message)
        {
            var breadcrumbLevel = new List<CustInfo>();

            if (message.BreadcrumbLevel != null && message.BreadcrumbLevel.Any())
            {
                foreach (var custInfo in message.BreadcrumbLevel)
                {
                    if (custInfo.CustLevelId == message.CustLevelId)
                    {
                        break;
                    }

                    breadcrumbLevel.Add(custInfo);
                }
            }

            breadcrumbLevel.Add(new CustInfo(message.CustId, message.CustName, message.CustLevelId));

            var offerCurrencies = Alpha.MemberInfo.Configurations.AppSettings.GetConfigValue("OfferAutoTransfer.Currencies");
            var customerListPagingViewModel = Handle(
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
                    PageSize = message.PageSize,
                    AllowAutoTransfer = message.AllowAutoTransfer,
                    AutoTransferAffect = transferService.IsAutoTransferAffect(message.Viewer, offerCurrencies),
                    SupportAutoTransfer = transferService.IsEnableAutoTransfer(message.Viewer, offerCurrencies)
                }
             );

            message.EnableAllDayBalanceTransfer = transferService.IsAllDayBalanceTransfer(message.CustId);

            return new CustomerListViewModel(
                message,
                breadcrumbLevel,
                browserHelper.IsMobile(isOnlyTablet: false),
                message.LimitedDateToShowInfo,
                customerListPagingViewModel: customerListPagingViewModel);
        }

        public CustomerListPagingViewModel Handle(FetchDownlinesListRequest message)
        {
            var param = new CustomerListParam(
                    message.ViewerId,
                    message.CustLevel,
                    message.CustId,
                    message.Username,
                    message.Enabled,
                    message.Closed,
                    message.Suspended,
                    message.Locked,
                    message.DoubleComm,
                    message.PageSize,
                    message.PageIndex,
                    message.LimitedDateToShowInfo,
                    message.ShowNickname,
                    message.IsShowMyanmarOdds,
                    message.AllowAutoTransfer);

            return new CustomerListPagingViewModel(customerListService.GetCustomerList(param), message, ipService);
        }

        public CustomerStatusViewModel Handle(CustomerStatusQuery message)
            => BuildCustomerStatus(message);

        public IEnumerable<ISettingItemMultipleViewModel> Handle(CustomerStatusMultipleQuery message)
           => BuildCustomerStatusMultiple(message);

        public Result Handle(CustomerNonProductStatusCommand message)
          => customerListService.UpdateCustomerStatus(
              new UpdateCustomerStatusParam(
                  message.SearchAccountId,
                  message.ActorId,
                  message.SubAccountId,
                  message.Language,
                  message.IP,
                  message.StatusValue,
                  message.StatusName,
                  message.ActorName));

        public MultipleDoubleCommViewModel Handle(MultipleDoubleCommRequest message)
           => new MultipleDoubleCommViewModel(message);

        public IEnumerable<Result> Handle(UpdateMultipleDoubleCommCommand message)
        {
            var results = new List<Result>();

            foreach (var searchAccountId in message.SearchAccountIds.Split(','))
            {
                results.Add(customerListService.UpdateCustomerStatus(
                    new UpdateCustomerStatusParam(
                      Converter.ToInt(searchAccountId),
                      message.ActorId,
                      message.SubAccountId,
                      message.Language,
                      message.IP,
                      message.EnabledDoubleComm,
                      StatusNames.DoubleComm,
                      message.ActorName)));
            }

            return results;
        }

        internal CustomerStatusViewModel BuildCustomerStatus(CustomerStatusQuery message)
        {
            var customerStatusModel = customerListService.GetCustomerStatus(message.SearchAccountId, message.SearchAccountRoleId);
            var isAllDayBalTransfer = transferService.IsAllDayBalanceTransfer(message.ActorId);

            var actor = new Customer(
               message.ActorId,
               message.SubAccountId,
               message.ActorName,
               message.ActorRoleId,
               BuildActorProductStatus(message, customerStatusModel.DCSProducts.ToList()));

            var upline = new Customer(
                 message.UplineId,
                 message.SubAccountId,
                 message.UplineName,
                 message.UplineRoleId,
                 BuildUplineProductStatus(customerStatusModel.DCSProducts.ToList()));

            var searchAccount = new Customer(
               message.SearchAccountId,
               message.SubAccountId,
               message.SearchAccountName,
               message.SearchAccountRoleId,
               BuildSearchAccountProductStatus(customerStatusModel.DCSProducts.ToList()),
               upline);

            return new CustomerStatusViewModel(
                BuildCustomerProductStatus(actor, searchAccount, message, customerStatusModel, isAllDayBalTransfer),
                BuildCustomerProductSettings(actor, searchAccount, message, customerStatusModel));
        }

        internal IEnumerable<ISettingItemMultipleViewModel> BuildCustomerStatusMultiple(CustomerStatusMultipleQuery message)
        {
            var customerStatusModel = customerListService.GetCustomerStatus(message.SearchAccountId, message.SearchAccountRoleId);

            var actor = new Customer(
               message.ActorId,
               message.SubAccountId,
               message.ActorName,
               message.ActorRoleId,
               BuildActorProductStatus(message, customerStatusModel.DCSProducts.ToList()));

            var upline = new Customer(
                 message.UplineId,
                 message.SubAccountId,
                 message.UplineName,
                 message.UplineRoleId,
                 BuildUplineProductStatus(customerStatusModel.DCSProducts.ToList()));

            var searchAccount = new Customer(
               message.SearchAccountId,
               message.SubAccountId,
               message.SearchAccountName,
               message.SearchAccountRoleId,
               BuildSearchAccountProductStatus(customerStatusModel.DCSProducts.ToList()),
               upline);

            return BuildCustomerProductMultipleSettings(actor, searchAccount, message, customerStatusModel);
        }

        internal IEnumerable<CustomerProductStatus> BuildSearchAccountProductStatus(
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            return dcsProducts.Select(product => new CustomerProductStatus(product.ProductId, product.IsDisabled, product.IsHidden, product.IsSync));
        }

        internal IEnumerable<CustomerProductStatus> BuildUplineProductStatus(
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            return dcsProducts.Select(product => new CustomerProductStatus(product.ProductId, product.IsDisabledUpline, product.IsHiddenUpline, product.IsSyncedUpline));
        }

        internal IEnumerable<CustomerProductStatus> BuildActorProductStatus(
            CustomerStatusQueryBase message,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.ESport, message.IsDisabledESports)
            };

            dcsProducts.ToList().ForEach(product =>
            {
                var productId = product.ProductId;
                customerProductStatus.Add(
                    new CustomerProductStatus(
                        productId,
                        message.GetDisabledProduct(productId),
                        message.GetHideProduct(productId),
                        message.GetSyncedProduct(productId)));
            });

            return customerProductStatus;
        }

        internal IEnumerable<IStatusItemViewModel> BuildCustomerProductStatus(
            Customer actor,
            Customer searchAccount,
            CustomerStatusQueryBase message,
            CustomerStatusModel model,
            bool allowedAllDayBalTransfer)
        {
            var request = new StatusSearchAccountRequest(actor, searchAccount, message.Language, message.IsDarkMode)
            {
                SubPageAccess = message.SubPageAccess
            };

            request
                .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)

                .AddData(DataKey.SearchAccount.IsAllowedMasterAutoPT, !model.NonProduct.DisableAutoMPT)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedOutright, !model.NonProduct.UplineDisableOutright)
                .AddData(DataKey.SearchAccount.IsAllowedOutright, !model.NonProduct.DisableOutright)

                .AddData(DataKey.SearchAccount.Upline.IsClosed, model.NonProduct.UplineClosed)
                .AddData(DataKey.SearchAccount.IsClosed, model.NonProduct.Closed)
                .AddData(DataKey.SearchAccount.IsDirectClosed, model.NonProduct.IsDirectClosed)

                .AddData(DataKey.SearchAccount.IsLockedAccess, model.NonProduct.Locked)

                .AddData(DataKey.SearchAccount.IsOfferProtectedMode, message.IsOfferProtectedMode)
                .AddData(DataKey.SearchAccount.IsProtectedMode, model.NonProduct.IsProtectedMode)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedDoubleComm, model.NonProduct.UplineDoubleCommLosing)
                .AddData(DataKey.SearchAccount.IsAllowedDoubleComm, model.NonProduct.DoubleCommLosing)

                .AddData(DataKey.SearchAccount.Upline.IsSuspended, model.NonProduct.UplineSuspend)
                .AddData(DataKey.SearchAccount.IsSuspended, model.NonProduct.Suspended)
                .AddData(DataKey.SearchAccount.IsDirectSuspend, model.NonProduct.IsDirectSuspend)

                .AddData(DataKey.SearchAccount.AllowedAllDayBalTransfer, allowedAllDayBalTransfer)
                .AddData(DataKey.SearchAccount.IsAllDayBalTransfer, model.NonProduct.IsAllDayBalTransfer)
                .AddData(DataKey.SearchAccount.IsAllowAutoTransfer, model.NonProduct.IsAllowAutoTransfer)
                .AddData(DataKey.SearchAccount.ScheduleWeekDays, model.NonProduct.ScheduleWeekDays);

            request.SupportAutoTransfer = message.SupportAutoTransfer;
            request.AutoTransferAffect = message.AutoTransferAffect;

            return statusViewModelFactory.GetViewModels(request).Where(status => status.Visible).OrderBy(status => status.DisplayOrder);
        }

        internal IEnumerable<ISettingItemViewModel> BuildCustomerProductSettings(
            Customer actor,
            Customer searchAccount,
            CustomerStatusQueryBase message,
            CustomerStatusModel model)
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount, message.Language, message.IsDarkMode)
            {
                SubPageAccess = message.SubPageAccess
            };
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode())
                 .AddData(DataKey.Theme.IsDarkMode, !string.IsNullOrWhiteSpace(message.DarkModeName));

            return settingViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }

        internal IEnumerable<ISettingItemMultipleViewModel> BuildCustomerProductMultipleSettings(
           Customer actor,
           Customer searchAccount,
           CustomerStatusMultipleQuery message,
           CustomerStatusModel model)
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount, message.Language, message.IsDarkMode)
            {
                SubPageAccess = message.SubPageAccess,
                SupportAutoTransfer = message.SupportAutoTransfer,
                AutoTransferAffect = message.AutoTransferAffect
            };

            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode())
                 .AddData("customerIds", message.CustIds)
                 .AddData("usernames", message.Usernames)
                 .AddData("customerNames", message.Usernames)
                 .AddData("language", message.Language)
                 .AddData(DataKey.SearchAccount.IsOfferProtectedMode, message.IsOfferProtectedMode)
                 .AddData(DataKey.Theme.IsDarkMode, !string.IsNullOrWhiteSpace(message.DarkModeName));

            return settingMultipleViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }
    }
}