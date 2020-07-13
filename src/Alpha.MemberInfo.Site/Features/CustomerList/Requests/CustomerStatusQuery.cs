namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using MediatR;

    public class CustomerStatusQuery : IRequest<CustomerStatusViewModel>
    {
        private readonly IUser viewer;

        public CustomerStatusQuery(IUser viewer)
        {
            this.viewer = viewer;
        }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public int UplineId { get; set; }

        public string UplineName { get; set; }

        public byte UplineRoleId { get; set; }

        public int ActorId => viewer.Id;

        public byte ActorRoleId => viewer.LevelId;

        public string ActorName => viewer.Name;

        public int SubAccountId => viewer.SubAccId;

        public bool GetDisabledProduct(int productId) => viewer.GetDisabledProduct(productId);

        public bool GetHideProduct(int productId) => viewer.GetHideProduct(productId);

        public bool GetSyncedProduct(int productId) => viewer.GetSyncedProduct(productId);

        public bool IsHideKeno => viewer.IsHideKeno;

        public bool IsCasinoSync => viewer.IsCasinoSync;

        public bool IsCasinoDisabled => viewer.IsCasinoDisabled;

        public bool IsDisabledESports => viewer.IsDisabledESports;

        public bool IsNumberGameDisabled => viewer.IsNumberGameDisabled;

        public bool IsHideGoldDeluxe => viewer.IsHideGoldDeluxe;

        public bool DisableGoldDeluxe => viewer.DisableGoldDeluxe;

        public bool IsValid() => IsValidSearchAccount && IsValidUpline;

        public bool CanUpdateAccount => viewer.CanUpdateAccount;

        internal bool IsValidSearchAccount =>
            IsValidName(SearchAccountName) &&
            IsValidSearchAccountRoleId(SearchAccountRoleId);

        internal bool IsValidUpline =>
            IsValidName(UplineName) &&
            UplineRoleId > SearchAccountRoleId;

        internal bool IsValidName(string name) => !string.IsNullOrEmpty(name);

        internal bool IsValidSearchAccountRoleId(byte searchAccountRoleId)
            => searchAccountRoleId < viewer.LevelId;

        public bool AllowedAllDayBalTransfer => viewer.AllowedAllDayBalTransfer;
    }
}