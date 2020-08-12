namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using Alpha.MemberInfo.Site.Helpers;
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
            IEnumerable<CustInfo> breadcrumbLevel,
            bool isMobile)
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

            PageIndex = request.PageIndex;
            PageSize = request.PageSize;
            Status = request.Status;
            DoubleComm = request.DoubleComm;
            UnconfirmSetting = request.UnconfirmSetting;
            IsMobileView = isMobile;

            DownlineLevelName = Enumeration.FromValue<UserLevel>(request.CustLevelId).GetDirectDownLineLevel().DisplayName;
        }
        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public bool HideEditMultiple { get; }

        public int SearchAccountId { get; }

        public string SearchAccountName { get; }

        public byte SearchAccountRoleId { get; }

        public byte Status { get; }

        public byte DoubleComm { get; }

        public byte UnconfirmSetting { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public string DownlineLevelName { get; }

        public string PageTitle { get; }

        public bool IsMobileView { get; }

        public IEnumerable<CustInfo> BreadcrumbLevel { get; }

        public string BreadcrumbLevelJson { get; }

        public IEnumerable<CustomerListOption> StatusOptions
             => new List<CustomerListOption>
                {
                    new CustomerListOption(0 , "All"),
                    new CustomerListOption(1 , "Open"),
                    new CustomerListOption(2 , "Suspended"),
                    new CustomerListOption(3 , "Closed"),
                    new CustomerListOption(4 , "Disabled")
                };

        public IEnumerable<CustomerListOption> DoubleCommOptions
             => new List<CustomerListOption>
                {
                    new CustomerListOption(2 , "All"),
                    new CustomerListOption(0 , "Allowed"),
                    new CustomerListOption(1 , "Disallowed")
                };

        public IEnumerable<CustomerListOption> UnconfirmedSettingOptions
             => new List<CustomerListOption>
                {
                    new CustomerListOption(0 , "Not Filte"),
                    new CustomerListOption(6 , "Casino")
                };
    }

    public class CustomerListOption
    {
        public CustomerListOption(int value, string text)
        {
            Value = value;
            Text = text;
        }

        public int Value { get; }

        public string Text { get; }
    }
}