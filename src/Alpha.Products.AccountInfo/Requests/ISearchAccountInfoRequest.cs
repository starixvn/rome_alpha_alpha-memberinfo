namespace Alpha.Products.AccountInfo.Requests
{
    using Alpha.Products.Core.Models;

    public interface ISearchAccountInfoRequest : IRequest, IExtendedRequest
    {
        ICustomer SearchAccount { get; }

        bool ActorIsEdittingDirectDownline { get; }

        bool ActorIsNotEdittingDirectDownline { get; }
    }
}