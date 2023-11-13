namespace Alpha.Products.AccountInfo.Requests
{
    using Alpha.Products.Core.Models;
    using AlphaPlus.User.Models;

    public interface ISearchAccountInfoRequest : IRequest, IExtendedRequest
    {
        ICustomer SearchAccount { get; }

        bool ActorIsEdittingDirectDownline { get; }

        bool ActorIsNotEdittingDirectDownline { get; }

        bool IsSubAcc { get; }

        PageAccess SubPageAccess { get; }

        bool SupportAutoTransfer { get; }

        bool AutoTransferAffect { get; }

        string Language { get; }

        bool IsDarkMode { get; }
    }
}