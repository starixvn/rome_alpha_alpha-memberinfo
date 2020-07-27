namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Helpers;
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

        public CustomerListViewModel(
            CustomerListRequest request,
            IEnumerable<CustInfo> breadcrumbLevel)
        {
            ActorId = request.ViewerId;
            ActorName = request.ViewerUsername;
            ActorRoleId = request.ViewerLevelId;

            SearchAccountId = request.CustId;
            SearchAccountRoleId = request.CustLevelId;
            SearchAccountName = request.CustName;

            HideEditMultiple =
                Enumeration.FromValue<UserLevel>(ActorRoleId).IsSuperLevel() &&
                Enumeration.FromValue<UserLevel>(request.CustLevelId).IsMasterLevel();

            PageTitle = PageTitleDictonary[Enumeration.FromValue<UserLevel>(request.CustLevelId)];

            BreadcrumbLevel = breadcrumbLevel;
            BreadcrumbLevelJson = JsonHelper.ToJsonFunc(breadcrumbLevel);

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

        public IEnumerable<CustInfo> BreadcrumbLevel { get; }

        public string BreadcrumbLevelJson { get; }
    }
}