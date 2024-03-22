namespace Alpha.Products.AccountInfo.Tests.Utility
{
    using System;
    using Alpha.Products.AccountInfo.Requests;
    using Core.Models;

    public static class RequestHelper
    {
        private static readonly Func<string, string> GetString = (key) => key;
        private static readonly Func<int, string> GetProductGroupName = (productId) => $"ProductGroupName-{productId}";
        private static readonly Func<int, string> GetProductName = (productId) => $"ProductName-{productId}";
        private static readonly Func<string, string, string> GetMicroSiteUrl = (microSiteName, virtualPath) => $"MicroSiteName/";

        public static ISearchAccountInfoRequest CreateRequestFromCustomers(ICustomer actor, ICustomer searchAccount)
            => new SearchAccountRequestStub(actor, searchAccount, GetProductGroupName, GetString, GetProductName);

        public static ISearchAccountInfoRequest CreateRequest(
            byte actorLevel,
            byte searchAccountLevel,
            Func<string, string> getString,
            Func<int, string> getProductName,
            Func<string, string, string> getMicroSiteUrl,
            bool uplineLevel = false)
        {
            var actor = CustomerHelper.CreateCustomer(1, actorLevel);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);

            return new SearchAccountRequestStub(
                actor,
                searchAccount,
                getProductName ?? GetProductGroupName,
                getString ?? GetString,
                getProductName ?? GetProductName,
                getMicroSiteUrl ?? GetMicroSiteUrl);
        }

        public static ISearchAccountInfoRequest CreateRequest(byte actorLevel, byte searchAccountLevel)
            => CreateRequest(actorLevel, searchAccountLevel, null, null, null);

        public static ISearchAccountInfoRequest CreateRequest(byte actorLevel, bool actorIsMainAccount, byte searchAccountLevel)
        {
            var actor = CustomerHelper.CreateCustomer(1, actorLevel, actorIsMainAccount ? 0 : 100);
            var searchAccount = CustomerHelper.CreateCustomer(2, searchAccountLevel);

            return CreateRequestFromCustomers(actor, searchAccount);
        }

        public static ISearchAccountInfoRequest CreateActorEditDirectDownlineRequest(byte actorLevel)
        {
            return CreateRequest(actorLevel, actorLevel--);
        }

        public static ISearchAccountInfoRequest CreateActorEditDirectDownlineRequest(byte actorLevel, bool actorIsMainAccount)
        {
            var actor = CustomerHelper.CreateCustomer(1, actorLevel, actorIsMainAccount ? 0 : 100);
            var searchAccount = CustomerHelper.CreateCustomer(2, actorLevel--);

            return CreateRequestFromCustomers(actor, searchAccount);
        }
    }
}