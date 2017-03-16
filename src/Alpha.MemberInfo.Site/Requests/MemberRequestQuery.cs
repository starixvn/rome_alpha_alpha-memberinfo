namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.MemberRequest;

    public class MemberRequestQuery : IRequest<MemberRequestViewModel>
    {
        public MemberRequestQuery()
        {
        }

        public MemberRequestQuery(IUser viewer)
        {
            CustId = viewer.Id;
            CustName = viewer.Name;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public string UserSelectedProductIds { get; set; }
    }
}