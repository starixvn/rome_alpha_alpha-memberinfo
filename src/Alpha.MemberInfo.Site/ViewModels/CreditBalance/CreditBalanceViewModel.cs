namespace Alpha.MemberInfo.Site.ViewModels.CreditBalance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AppResources;
    using Constants;
    using MemberInfo.Models;
    using MemberInfo.Models.CreditBalance;
    using Requests.CreditBalance;
    using Sunplus.Agent.Utilities;
    using Web.Helpers;

    public class CreditBalanceViewModel
    {
        public CreditBalanceViewModel(CreditBalanceQuery message)
            : this(new PagedCreditBalance(message.PageIndex, message.PageSize, 0, Enumerable.Empty<CreditBalanceItem>()),
                  null,
                  message)
        {
        }

        public CreditBalanceViewModel(
            PagedCreditBalance pagedCreditBalances,
            string transferCondition,
            CreditBalanceQuery message)
        {
            PagedCreditBalances = pagedCreditBalances;
            TransferCondition = transferCondition;

            CustId = message.CustId;
            CustLevelId = message.CustLevelId;
            UserName = message.UserName;
            StatusId = message.StatusId.Value;

            DownlineLevel = message.CustLevel.GetDirectDownLineLevel();
            IsViewingMemberList = DownlineLevel.IsMemberLevel();
            ActorIsAgent = message.ActorLevel.IsAgentLevel();
            CanUpdateAccount = message.CanUpdateAccount;
            var showTransferPopup = !message.IsAllDayBalTransfer
                && ActorIsAgent
                && message.CanTransfer;
            CanTransferOnBalanceColumn = showTransferPopup && message.IsAllowedTotalBalanceTransfer;
            CanTransferOnYesterdayBalanceColumn = showTransferPopup && !message.IsAllowedTotalBalanceTransfer;
            CanViewBetListsFromOutstanding = IsViewingMemberList && message.CanViewBetLists;
            IsShowDateTimeColumn = message.LimitedDateToShowInfo == default(DateTime);
            CookiePrefix = Encryption.PearsonEncode(string.Concat(message.LoggedInUserId, message.ActorId, message.SubAccId, "CreditBalance"));
        }

        public PagedCreditBalance PagedCreditBalances { get; }

        public string TransferCondition { get; }

        public int CustId { get; }

        public byte CustLevelId { get; }

        public string UserName { get; }

        public byte StatusId { get; }

        public IEnumerable<SelectListItem> Status
        {
            get
            {
                yield return BuildSelectListItem(Resources.All, StatusIdentity.All);
                yield return BuildSelectListItem(Resources.Open, StatusIdentity.Open);
                yield return BuildSelectListItem(Resources.Suspended, StatusIdentity.Suspended);
                yield return BuildSelectListItem(Resources.Closed, StatusIdentity.Closed);
                yield return BuildSelectListItem(Resources.Disabled, StatusIdentity.Disabled);
            }
        }

        private SelectListItem BuildSelectListItem(string text, byte value)
            => new SelectListItem { Text = text, Value = value.ToString(), Selected = value == StatusId };

        public UserLevel DownlineLevel { get; }

        public bool IsViewingMemberList { get; }

        public bool ActorIsAgent { get; }

        public bool CanUpdateAccount { get; }

        public bool CanTransferOnBalanceColumn { get; }

        public bool CanTransferOnYesterdayBalanceColumn { get; }

        public bool CanViewBetListsFromOutstanding { get; }

        public bool IsShowDateTimeColumn { get; }

        public string CookiePrefix { get; }

        public string GeStatustBackgroundColor(CreditBalanceItem item)
        {
            if (!item.Enabled)
            {
                return "disabled-bg";
            }

            if (item.Closed)
            {
                return GetClosedBackgroundColor(item.ClosedReason);
            }

            if (item.Suspended)
            {
                return "suspended-bg";
            }

            return item.RowNumber % 2 == 0 ? "even-row" : string.Empty;
        }

        private static string GetClosedBackgroundColor(byte closedReason)
        {
            const byte WrongPasswordOrOthers = 0;
            const byte ClosedByAdmin = 1;
            const byte ClosedByUpline = 2;
            const byte WrongOTPOrSC = 3;

            switch (closedReason)
            {
                case ClosedByAdmin:
                    return "closed-byadmin";

                case ClosedByUpline:
                    return "closed-byupline";

                case WrongOTPOrSC:
                    return "closed-wrongotp-sc";

                case WrongPasswordOrOthers:
                default:
                    return "closed-wrongpwd-others";
            }
        }

        public string GetRunningBetListLink(CreditBalanceItem item)
            => MicroSites.BetListsUrl($"~/RunningBetList/Index?custId={item.CustId}&custName={item.UserName}&getAll=true");

        public string OldMainRootPath => MicroSites.OldMainUrl();

        public string GivenCreditLink => MicroSites.AccountInfoUrl($"~/SearchAccount/GivenCredit");
    }
}