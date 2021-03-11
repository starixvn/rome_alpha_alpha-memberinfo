namespace Alpha.Products.AccountInfo.Setting.ViewModels.Base
{
    using Alpha.Products.Core.ViewModels;

    public interface ISettingItemViewModel : IViewModel
    {
        string Text { get; }

        string Url { get; }
    }
}