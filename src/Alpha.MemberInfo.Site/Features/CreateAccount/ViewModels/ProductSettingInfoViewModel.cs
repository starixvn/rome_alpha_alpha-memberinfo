namespace Alpha.MemberInfo.Site.Features.CreateAccount.ViewModels
{
    public class ProductSettingInfoViewModel
    {
        public ProductSettingInfoViewModel(string theme, string language)
        {
            this.ProductSettingInfoUrl = "~/ProductSettingInfo";
            this.Theme = theme;
            this.Language = language;
        }

        public string ProductSettingInfoUrl { get; }
        public string Theme { get; }
        public string Language { get; }
    }
}