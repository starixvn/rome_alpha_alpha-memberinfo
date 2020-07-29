namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using System.Collections.Generic;

    public class CustomerStatusMultipleViewModel
    {
        public CustomerStatusMultipleViewModel(
            IEnumerable<IStatusItemViewModel> status,
            IEnumerable<ISettingItemMultipleViewModel> settings)
        {
            Status = status;
            Settings = settings;
        }

        public IEnumerable<IStatusItemViewModel> Status { get; }

        public IEnumerable<ISettingItemMultipleViewModel> Settings { get; }
    }
}