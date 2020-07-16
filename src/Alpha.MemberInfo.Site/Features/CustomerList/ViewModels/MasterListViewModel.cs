namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.Web.Helpers;
    using System.Collections.Generic;

    public class MasterListViewModel
    {
        public MasterListViewModel(
            IEnumerable<MasterListModel> downlinesList,
            int totalDownlines,
            int totalPages,
            MasterListRequest request)
        {
            DownlinesList = downlinesList;
            TotalDownlines = totalDownlines;
            TotalPages = totalPages;
            ActorId = request.ActorId;
            ActorName = request.ActorName;
            ActorRoleId = request.ActorRoleId;
        }

        public IEnumerable<MasterListModel> DownlinesList { get; }

        public int TotalDownlines { get; }

        public int TotalPages { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

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

    }
}