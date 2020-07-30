namespace Alpha.Products.AccountInfo.Tests.Utility
{
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Models;
    using System;

    /// <summary>
    /// should implementation be located in consumer sites.
    /// </summary>
    public class SearchAccountRequestStub : ExtendedRequest, ISearchAccountInfoRequest
    {
        public SearchAccountRequestStub(
            ICustomer actor,
            ICustomer searchAccount,
            Func<string, string> GetResource = null,
            Func<int, string> GetProductName = null,
            Func<string, string, string> GetMicroSiteUrl = null)
        {
            Actor = actor;
            SearchAccount = searchAccount;
            this.GetResource = GetResource ?? ((key) => key);
            this.GetProductName = GetProductName ?? ((productId) => $"Product Name of {productId}");
            this.GetMicroSiteUrl = GetMicroSiteUrl ?? ((microSiteName, virtualPath) => $"microSiteName/");
        }

        public SearchAccountRequestStub(ICustomer actor, ICustomer searchAccount)
            : this(actor, searchAccount, null, null)
        {
        }

        public ICustomer Actor { get; }
        public ICustomer SearchAccount { get; }

        public virtual bool IsValid =>
            Actor != null
            && SearchAccount != null
            && Actor.IsUplineOf(SearchAccount);

        public bool ActorIsEdittingDirectDownline => Actor.IsDirectUplineOf(SearchAccount);

        public bool ActorIsNotEdittingDirectDownline => !ActorIsEdittingDirectDownline;

        public Func<string, string> GetResource { get; }

        public Func<int, string> GetProductName { get; }

        public Func<IConfigurationItem, bool> Predicate => _ => true;

        public Func<string, string, string> GetMicroSiteUrl { get; }
    }
}