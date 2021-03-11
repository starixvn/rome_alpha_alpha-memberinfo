namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using System.Collections.Generic;

    public class CustomerStatusViewModel
    {
        public CustomerStatusViewModel(
            IEnumerable<IStatusItemViewModel> status,
            IEnumerable<ISettingItemViewModel> settings)
        {
            Status = status;
            Settings = settings;
        }

        public IEnumerable<IStatusItemViewModel> Status { get; }

        public IEnumerable<ISettingItemViewModel> Settings { get; }
    }
}