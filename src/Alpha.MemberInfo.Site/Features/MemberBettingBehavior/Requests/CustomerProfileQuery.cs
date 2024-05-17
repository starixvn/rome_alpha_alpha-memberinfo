
namespace Alpha.MemberInfo.Site.Features.MemberBettingBehavior.Requests
{
    using Alpha.MemberInfo.Features.MemberBettingBehavior.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class CustomerProfileQuery : MemberInfoBaseQuery, IAsyncRequest<CustomerProfile>
    {
        public CustomerProfileQuery(IUser viewer) : base(viewer)
        {

        }
    }
}