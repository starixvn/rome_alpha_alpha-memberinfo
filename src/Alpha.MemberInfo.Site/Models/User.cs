namespace Alpha.MemberInfo.Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MemberInfo.Models;
    using Sunplus.Agent.Model.Entities;

    public class User : IUser
    {
        private const string Jackpot = "Jackpot";
        private const string GameEnableSessionKey = "_GameEnable_";
        private const string CustomerDatelimit = "CustomerDateLimit";

        private readonly IUserSession userSession;

        public User(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public int LoggedInUserId => userSession.UserId;

        public int Id => userSession.CustId;

        public string Name => userSession.UserName;

        public string Language => userSession.Language;

        public string LanguageKey => userSession.LangKey;

        public byte LevelId => userSession.RoleId;

        public int SubAccId => userSession.SubAccId;

        public string SubAccName => userSession.SubUserName;

        public string IP => userSession.UserLoginIp;

        public bool CanUpdateAccount => userSession.SubPageAccess.UpdateAccount;

        public bool IsEnabledJackpot => EnableJackpotProductIds.Any();

        public IList<int> EnableJackpotProductIds => GetEnableProductsForGame(Jackpot);

        public bool GetHideProduct(int productId) => userSession.GetHideProduct(productId);

        public bool GetSyncedProduct(int productId) => userSession.GetSyncedProduct(productId);

        internal virtual IList<int> GetEnableProductsForGame(string gameName)
            => (IList<int>)HttpContext.Current.Session[HttpContext.Current.Session.SessionID + GameEnableSessionKey + gameName];

        public bool CanViewBetLists => userSession.SubPageAccess.GetPermission(PageAccessType.ViewBetLists);

        public bool IsAllDayBalTransfer => userSession.IsAllDayBalTransfer;

        public bool IsAllowedTotalBalanceTransfer => userSession.IsAllowedTotalBalanceTransfer;

        public bool CanTransfer => userSession.SubPageAccess.Transfer;

        public DateTime LimitedDateToShowInfo => userSession.GetProperty<DateTime>(CustomerDatelimit);

        public bool IsAdminCanViewVNIP => userSession.IsAdminCanViewVNIP;

        public bool IsInternal => userSession.IsInternal;
    }
}