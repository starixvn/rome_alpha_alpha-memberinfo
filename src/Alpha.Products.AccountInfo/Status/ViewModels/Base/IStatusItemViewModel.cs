namespace Alpha.Products.AccountInfo.Status.ViewModels.Base
{
    using Alpha.Products.Core.ViewModels;

    public interface IStatusItemViewModel : IViewModel
    {
        string Text { get; }

        string Url { get; }

        bool Disabled { get; }

        bool Checked { get; }

        bool ShowInCustomerList { get; }
    }
}