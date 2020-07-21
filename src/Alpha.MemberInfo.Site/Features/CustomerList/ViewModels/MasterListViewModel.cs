namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web.Helpers;
    using System.Collections.Generic;

    public class MasterListViewModel
    {
        private Dictionary<UserLevel, string> PageTitleDictonary = new Dictionary<UserLevel, string>
        {
            {UserLevel.Super, "Master Agent List"},
            {UserLevel.Master, "Agent List"},
            {UserLevel.Agent, "Member List"}
        };

        public MasterListViewModel(CustomerListDTO customerListDTO, MasterListRequest request)
        {
            CustomerListDTO = customerListDTO;
            ActorId = request.ActorId;
            ActorName = request.ActorName;
            ActorRoleId = request.ActorRoleId;

            HideEditMultiple =
                Enumeration.FromValue<UserLevel>(ActorRoleId).IsSuperLevel() &&
                Enumeration.FromValue<UserLevel>(request.SearchAccountRoleId).IsMasterLevel();

            PageTitle = PageTitleDictonary[Enumeration.FromValue<UserLevel>(request.SearchAccountRoleId)];

            //SearchAccountLevelName = Enumeration.FromValue<UserLevel>(request.SearchAccountRoleId - 1).DisplayName;
        }

        public CustomerListDTO CustomerListDTO { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public bool HideEditMultiple { get; }

        //public string SearchAccountLevelName { get; }

        public string BuildMultipleEditPopupUrl(
            byte searchAccountRoleId,
             int uplineId,
            string uplineName,
            byte uplineRoleId)
            => $"{MicroSites.AccountInfoUrl()}SearchAccount/MultipleEditPopup?SearchAccountRoleId={searchAccountRoleId}"
            + $"&UplineId={uplineId}&UplineName={uplineName}&UplineRoleId={uplineRoleId}";

        public string BuildPopupEditUrl(
            int searchAccountId,
            string searchAccountName,
            byte searchAccountRoleId,
            int uplineId,
            string uplineName,
            byte uplineRoleId)
            => $"{MicroSites.AccountInfoUrl()}SearchAccount/EditPopup?" +
            $"SearchAccountId={searchAccountId}&SearchAccountName={searchAccountName}&SearchAccountRoleId={searchAccountRoleId}&" +
            $"UplineId={uplineId}&UplineName={uplineName}&UplineRoleId={uplineRoleId}";

        public string BuildViewDownlineUrl(byte searchAccountRoleId)
            => $"{MicroSites.MemberInfoUrl()}MasterList/{ Enumeration.FromValue<UserLevel>(searchAccountRoleId).DisplayName}?SearchAccountRoleId={searchAccountRoleId}";

        public string BuildStatementUrl(
            int searchAccountId,
            string searchAccountName,
            byte searchAccountRoleId)
        {
            var actionName = Enumeration.FromValue<UserLevel>(searchAccountRoleId).IsMemberLevel() ? "MemberStatement" : "SMAStatement";
            return $"{MicroSites.ReportsUrl()}Statement/{actionName}?CustID={searchAccountId}&CustName={searchAccountName}&CustLevelId={searchAccountRoleId}";
        }

        public string BuildLoginIPUrl(string ip)
            => $"{MicroSites.OldMainUrl()}_IPInfo/IPInfo.aspx?ip={ip}";

        public string PageTitle { get; }
    }
}