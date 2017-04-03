using System.Web.Mvc;

namespace Alpha.MemberInfo.Site.ViewModels
{
    public class ProductViewModel : SelectListItem
    {
        public ProductViewModel(
            string text,
            string value,
            bool selected,
            bool disabled,
            bool hasSetting)
        {
            Text = text;
            Value = value;
            Selected = selected;
            Disabled = disabled;
            HasSetting = hasSetting;
        }

        public bool HasSetting { get; }
    }
}