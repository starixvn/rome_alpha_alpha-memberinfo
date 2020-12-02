namespace Alpha.MemberInfo.Site.Features.QRCodeMemberSignIn
{
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Services;
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
    using Alpha.SecurityCode.Services;
    using Alpha.Web.Helpers;
    using Fanex.Common;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerListHandler :
        IRequestHandler<CustomerListRequest, CustomerListViewModel>,
        IRequestHandler<FetchDownlinesListRequest, CustomerListDTO>,
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
        private readonly CacheRefService cacheRefService;

        public CustomerListHandler(
            CustomerListService customerListService,
            SecurityCodeChecker securityCodeChecker,
            BrowserHelper browserHelper,
            CacheRefService cacheRefService)
            : this(customerListService, null, null, null, securityCodeChecker, browserHelper, cacheRefService)
        {
        }

        internal CustomerListHandler(
            CustomerListService customerListService,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel> settingMultipleViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel> settingViewModelFactory,
            GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel> statusViewModelFactory,
            SecurityCodeChecker securityCodeChecker,
            BrowserHelper browserHelper,
            CacheRefService cacheRefService)
        {
            this.customerListService = customerListService;

            this.settingMultipleViewModelFactory = settingMultipleViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemMultipleViewModel>>();

            this.settingViewModelFactory = settingViewModelFactory ??
                SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>();

            this.statusViewModelFactory = statusViewModelFactory ??
                 SimpleServiceLocator.GetService<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>();

            this.securityCodeChecker = securityCodeChecker;
            this.browserHelper = browserHelper;
            this.cacheRefService = cacheRefService;
        }

        public CustomerListViewModel Handle(CustomerListRequest message)
        {
            var unsyncedProducts = customerListService.GetUnsyncedProductIds(message.ViewerId)
                .Select(productId => new UnsyncedProductModel(productId, cacheRefService.GetProductName(productId)));

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

            return new CustomerListViewModel(message, breadcrumbLevel, unsyncedProducts, browserHelper.IsMobile(isOnlyTablet: false), message.LimitedDateToShowInfo);
        }

        public CustomerListDTO Handle(FetchDownlinesListRequest message)
            => customerListService.GetCustomerList(
                    new CustomerListParam(
                    message.ViewerId,
                    message.CustLevel,
                    message.CustId,
                    message.Username,
                    message.Enabled,
                    message.Closed,
                    message.Suspended,
                    message.DoubleComm,
                    message.UnconfirmSetting,
                    message.PageSize,
                    message.PageIndex,
                    message.LimitedDateToShowInfo));

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
            var customerStatusModel = customerListService.GetCustomerStatus(message.SearchAccountId);

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
                 BuildUplineProductStatus(customerStatusModel.NonDCSProduct, customerStatusModel.DCSProducts.ToList()));

            var searchAccount = new Customer(
               message.SearchAccountId,
               message.SubAccountId,
               message.SearchAccountName,
               message.SearchAccountRoleId,
               BuildSearchAccountProductStatus(customerStatusModel.NonDCSProduct, customerStatusModel.DCSProducts.ToList()),
               upline);

            return new CustomerStatusViewModel(
                BuildCustomerProductStatus(actor, searchAccount, message, customerStatusModel),
                BuildCustomerProductSettings(actor, searchAccount, message, customerStatusModel));
        }

        internal IEnumerable<ISettingItemMultipleViewModel> BuildCustomerStatusMultiple(CustomerStatusMultipleQuery message)
        {
            var customerStatusModel = customerListService.GetCustomerStatus(message.SearchAccountId);

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
                 BuildUplineProductStatus(customerStatusModel.NonDCSProduct, customerStatusModel.DCSProducts.ToList()));

            var searchAccount = new Customer(
               message.SearchAccountId,
               message.SubAccountId,
               message.SearchAccountName,
               message.SearchAccountRoleId,
               BuildSearchAccountProductStatus(customerStatusModel.NonDCSProduct, customerStatusModel.DCSProducts.ToList()),
               upline);

            return BuildCustomerProductMultipleSettings(actor, searchAccount, message, customerStatusModel);
        }

        internal IEnumerable<CustomerProductStatus> BuildSearchAccountProductStatus(
            NonDCSProductStatusModel nonDCSProduct,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, nonDCSProduct.DisableNumber),
                new CustomerProductStatus(ProductConstants.RNGCasino, nonDCSProduct.DisableCasino, synced: nonDCSProduct.SyncCasino),
                new CustomerProductStatus(ProductConstants.ESport, nonDCSProduct.DisableEsports),
                new CustomerProductStatus(ProductConstants.Keno, hidden: nonDCSProduct.HideKeno),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, nonDCSProduct.DisableGoldDeluxe, hidden: nonDCSProduct.HideGoldDeluxe)
            };

            dcsProducts.ToList().ForEach(product =>
            {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabled, product.IsHidden, product.IsSync));
            });

            return customerProductStatus;
        }

        internal IEnumerable<CustomerProductStatus> BuildUplineProductStatus(
            NonDCSProductStatusModel nonDCSProduct,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, nonDCSProduct.UplineDisableNumber),
                new CustomerProductStatus(ProductConstants.RNGCasino, nonDCSProduct.UplineDisableCS),
                new CustomerProductStatus(ProductConstants.ESport, nonDCSProduct.UplineDisableEsports),
                new CustomerProductStatus(ProductConstants.Keno, hidden: nonDCSProduct.HideKeno),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, nonDCSProduct.UplineDisableGoldDeluxe, hidden: nonDCSProduct.UplineHideGoldDeluxe)
            };

            dcsProducts.ToList().ForEach(product =>
            {
                customerProductStatus.Add(
                    new CustomerProductStatus(product.ProductId, product.IsDisabledUpline, product.IsHiddenUpline, product.IsSyncedUpline));
            });

            return customerProductStatus;
        }

        internal IEnumerable<CustomerProductStatus> BuildActorProductStatus(
            CustomerStatusQueryBase message,
            IEnumerable<DCSProductStatusModel> dcsProducts)
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, message.IsNumberGameDisabled),
                new CustomerProductStatus(ProductConstants.RNGCasino, message.IsCasinoDisabled, synced: message.IsCasinoSync),
                new CustomerProductStatus(ProductConstants.ESport, message.IsDisabledESports),
                new CustomerProductStatus(ProductConstants.Keno, hidden:message.IsHideKeno),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, message.DisableGoldDeluxe, hidden: message.IsHideGoldDeluxe)
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
            CustomerStatusModel model)
        {
            var request = new StatusSearchAccountRequest(actor, searchAccount);

            request
                .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)

                .AddData(DataKey.SearchAccount.IsAllowedMasterAutoPT, !model.NonProduct.DisableAutoMPT)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedOutright, !model.NonProduct.UplineDisableOutright)
                .AddData(DataKey.SearchAccount.IsAllowedOutright, !model.NonProduct.DisableOutright)

                .AddData(DataKey.SearchAccount.Upline.IsClosed, model.NonProduct.UplineClosed)
                .AddData(DataKey.SearchAccount.IsClosed, model.NonProduct.Closed)

                .AddData(DataKey.SearchAccount.Upline.IsAllowedDoubleComm, model.NonProduct.UplineDoubleCommLosing)
                .AddData(DataKey.SearchAccount.IsAllowedDoubleComm, model.NonProduct.DoubleCommLosing)

                .AddData(DataKey.SearchAccount.Upline.IsSuspended, model.NonProduct.UplineSuspend)
                .AddData(DataKey.SearchAccount.IsSuspended, model.NonProduct.Suspended)
                .AddData(DataKey.SearchAccount.IsDirectSuspend, model.NonProduct.IsDirectSuspend)

                .AddData(DataKey.SearchAccount.AllowedAllDayBalTransfer, message.AllowedAllDayBalTransfer)
                .AddData(DataKey.SearchAccount.IsAllDayBalTransfer, model.NonProduct.IsAllDayBalTransfer);


            return statusViewModelFactory.GetViewModels(request).Where(status => status.Visible).OrderBy(status => status.DisplayOrder);
        }

        internal IEnumerable<ISettingItemViewModel> BuildCustomerProductSettings(
            Customer actor,
            Customer searchAccount,
            CustomerStatusQueryBase message,
            CustomerStatusModel model)
        {
            var request = new SettingSearchAccountRequest(actor, searchAccount);
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode());

            return settingViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }

        internal IEnumerable<ISettingItemMultipleViewModel> BuildCustomerProductMultipleSettings(
           Customer actor,
           Customer searchAccount,
           CustomerStatusMultipleQuery message,
           CustomerStatusModel model)
        {
            var syncedCasinoModel = customerListService.GetSyncedCasinoModel(message.CustIds);

            var request = new SettingSearchAccountRequest(actor, searchAccount);
            request
                 .AddData(DataKey.Actor.CanEditDailyTransferOption, model.NonProduct.TransferDaily)
                 .AddData(DataKey.Actor.HasUpdateAccountPermission, message.CanUpdateAccount)
                 .AddData(DataKey.SearchAccount.IsEnableSecurityCode, securityCodeChecker.IsEnableSecurityCode())
                 .AddData("customerIds", message.CustIds)
                 .AddData("usernames", message.Usernames)
                 .AddData("customerNames", message.Usernames)
                 .AddData("syncedCasinoList", string.Join(",", syncedCasinoModel.Select(item => item.SyncedCasino ? "1" : "0")))
                 .AddData("language", message.Language);

            return settingMultipleViewModelFactory.GetViewModels(request).Where(setting => setting.Visible).OrderBy(setting => setting.DisplayOrder);
        }
    }
}