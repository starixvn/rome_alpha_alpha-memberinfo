namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web.Helpers;
    using System.Collections.Generic;

    public class CustomerListViewModel
    {
        private Dictionary<UserLevel, string> PageTitleDictonary = new Dictionary<UserLevel, string>
        {
            {UserLevel.Super, "Master Agent List"},
            {UserLevel.Master, "Agent List"},
            {UserLevel.Agent, "Member List"}
        };

        public CustomerListViewModel(CustomerListRequest request)
        {
            ActorId = request.ActorId;
            ActorName = request.ActorName;
            ActorRoleId = request.ActorRoleId;

            SearchAccountId = request.SearchAccountId;
            SearchAccountRoleId = request.SearchAccountRoleId;
            SearchAccountName = request.SearchAccountName;

            HideEditMultiple =
                Enumeration.FromValue<UserLevel>(ActorRoleId).IsSuperLevel() &&
                Enumeration.FromValue<UserLevel>(request.SearchAccountRoleId).IsMasterLevel();

            PageTitle = PageTitleDictonary[Enumeration.FromValue<UserLevel>(request.SearchAccountRoleId)];

            //SearchAccountLevelName = Enumeration.FromValue<UserLevel>(request.SearchAccountRoleId - 1).DisplayName;
        }
        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public bool HideEditMultiple { get; }

        public int SearchAccountId { get; }

        public string SearchAccountName { get; }

        public byte SearchAccountRoleId { get;  }

        //public string SearchAccountLevelName { get; }

        public string BuildMultipleEditPopupUrl(
            byte searchAccountRoleId,
             int uplineId,
            string uplineName,
            byte uplineRoleId)
            => $"{MicroSites.AccountInfoUrl()}SearchAccount/MultipleEditPopup?SearchAccountRoleId={searchAccountRoleId}"
            + $"&UplineId={uplineId}&UplineName={uplineName}&UplineRoleId={uplineRoleId}";

        public string PageTitle { get; }
    }
}