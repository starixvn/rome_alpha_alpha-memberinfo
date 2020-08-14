namespace Alpha.Products.AccountInfo.Setting.ViewModels.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Models;
    using Alpha.Products.Core.Shared;

    public abstract class SettingItemViewModel : ISettingItemViewModel
    {
        protected readonly ISearchAccountInfoRequest Request;

        protected SettingItemViewModel(
            ISearchAccountInfoRequest request,
            int displayOrder = 0)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Request = request;
            DisplayOrder = displayOrder;
        }

        protected ICustomer Actor => Request.Actor;

        protected ICustomer SearchAccount => Request.SearchAccount;

        public virtual string Text => Request.GetResource($"{GetType().Name}Text");

        public int DisplayOrder { get; }

        public virtual string Url
        {
            get
            {
                var queryString =
                     string.Join("&", QueryStringParamaters.Select(query => $"{query.Key}={query.Value}"));

                return $"{BaseAddress}?{queryString}";
            }
        }

        public virtual string Icon => "icon-edit Enable";

        public abstract Dictionary<string, string> QueryStringParamaters { get; }

        protected abstract string BaseAddress { get; }

        protected virtual string OldMainUrl => Request.GetMicroSiteUrl(MicroSiteConstants.OldMain,"~/");

        protected virtual string MainUrl => Request.GetMicroSiteUrl(MicroSiteConstants.Main, "~/");

        protected virtual string AccountInfoUrl => Request.GetMicroSiteUrl(MicroSiteConstants.AccountInfo, "~/");

        public virtual bool Visible => Request.ActorIsEdittingDirectDownline;
    }
}