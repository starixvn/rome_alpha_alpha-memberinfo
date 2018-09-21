namespace Alpha.MemberInfo.Models
{
    using System;
    using System.Collections.Generic;

    public interface IUser
    {
        int LoggedInUserId { get; }

        int Id { get; }

        string Name { get; }

        string Language { get; }

        string LanguageKey { get; }

        byte LevelId { get; }

        int SubAccId { get; }

        string SubAccName { get; }

        string IP { get; }

        bool CanUpdateAccount { get; }

        bool IsEnabledJackpot { get; }

        IList<int> EnableJackpotProductIds { get; }

        bool GetHideProduct(int productId);

        bool GetSyncedProduct(int productId);

        bool CanViewBetLists { get; }

        bool IsAllDayBalTransfer { get; }

        bool IsAllowedTotalBalanceTransfer { get; }

        bool CanTransfer { get; }

        DateTime LimitedDateToShowInfo { get; }

        bool IsAdminCanViewVNIP { get; }
    }
}