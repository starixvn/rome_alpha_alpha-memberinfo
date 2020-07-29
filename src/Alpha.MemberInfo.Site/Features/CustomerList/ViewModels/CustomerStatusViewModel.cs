namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using System.Collections.Generic;

    public class CustomerStatusViewModel
    {
        public CustomerStatusViewModel(
            IEnumerable<IStatusItemViewModel> status,
            IEnumerable<ISettingItemViewModel> settings,
            bool isMultiple = false)
        {
            Status = status;
            Settings = settings;
            IsMultiple = isMultiple;
        }

        public bool IsMultiple { get; }

        public IEnumerable<IStatusItemViewModel> Status { get; }

        public IEnumerable<ISettingItemViewModel> Settings { get; }
    }
}