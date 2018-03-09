namespace Alpha.MemberInfo.Site.Requests.ProgressiveJackpot
{
    using System.Collections.Generic;
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.ProgressiveJackpot;

    public class ProgressiveJacpotQuery : MemberInfoBaseQuery, IRequest<ProgressiveJackpotViewModel>
    {
        public ProgressiveJacpotQuery(IUser viewer) : base(viewer)
        {
            EnableJackpotProductIds = viewer.EnableJackpotProductIds;
        }

        public string UserSelectedProductIds { get; set; }

        public IList<int> EnableJackpotProductIds { get; }
    }
}