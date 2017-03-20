namespace Alpha.MemberInfo.Site.Requests.MemberRequest
{
    using System.Collections.Generic;
    using MediatR;
    using MemberInfo.Models;

    public class EnableCasinoCommand :
        MemberInfoBaseQuery,
        IRequest<IEnumerable<Result>>
    {
        public EnableCasinoCommand()
        {
        }

        public EnableCasinoCommand(IUser viewer) : base(viewer)
        {
        }

        public string CustIds { get; set; }

        public int UserSelectedProductId { get; set; }
    }
}