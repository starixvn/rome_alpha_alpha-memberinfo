namespace Alpha.MemberInfo.Site.Requests.CreditBalance
{
    using System;
    using System.Net;
    using Constants;
    using Enumerations;
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.CreditBalance;

    public class CreditBalanceQuery : IAsyncRequest<CreditBalanceViewModel>
    {
        private int custId;
        private byte custLevelId;
        private string userName;
        private byte? statusId;
        private int pageIndex;
        private int pageSize;

        public CreditBalanceQuery(IUser user)
        {
            LoggedInUserId = user.LoggedInUserId;
            SubAccId = user.SubAccId;
            ActorId = user.Id;
            ActorLevel = Enumeration.FromValue<UserLevel>(user.LevelId);
            CanUpdateAccount = user.CanUpdateAccount;
            IsAllDayBalTransfer = user.IsAllDayBalTransfer;
            IsAllowedTotalBalanceTransfer = user.IsAllowedTotalBalanceTransfer;
            CanTransfer = user.CanTransfer;
            CanViewBetLists = user.CanViewBetLists;
            LimitedDateToShowInfo = user.LimitedDateToShowInfo;
            AdminId = user.AdminId;
            IsAdminCanViewVNIP = user.IsAdminCanViewVNIP;
        }

        public int CustId
        {
            get
            {
                return custId == 0 ? ActorId : custId;
            }
            set
            {
                custId = value;
            }
        }

        public byte CustLevelId
        {
            get
            {
                return custLevelId == 0 ? ActorLevel.Value : custLevelId;
            }
            set
            {
                custLevelId = value;
            }
        }

        public UserLevel CustLevel
            => Enumeration.FromValue<UserLevel>(CustLevelId);

        public string UserName
        {
            get
            {
                return string.IsNullOrWhiteSpace(userName) ? userName : WebUtility.HtmlEncode(userName);
            }
            set
            {
                userName = value;
            }
        }

        public byte? StatusId
        {
            get
            {
                return statusId ?? StatusIdentity.Open;
            }
            set
            {
                statusId = value;
            }
        }

        public int PageIndex
        {
            get
            {
                return pageIndex == 0 ? 1 : pageIndex;
            }
            set
            {
                pageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return pageSize == 0 ? 500 : pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        public int LoggedInUserId { get; }

        public int SubAccId { get; }

        public int ActorId { get; }

        public UserLevel ActorLevel { get; }

        public bool CanUpdateAccount { get; }

        public bool IsAllDayBalTransfer { get; }

        public bool IsAllowedTotalBalanceTransfer { get; }

        public bool CanTransfer { get; }

        public bool CanViewBetLists { get; }

        public DateTime LimitedDateToShowInfo { get; }

        public int AdminId { get; }

        public bool IsAdminCanViewVNIP { get; }

        public bool IsValid()
            => ActorLevel.IsEqualOrUplineOf(CustLevel);
    }
}