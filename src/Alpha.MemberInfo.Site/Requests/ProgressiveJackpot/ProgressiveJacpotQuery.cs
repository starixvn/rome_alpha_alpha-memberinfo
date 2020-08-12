namespace Alpha.MemberInfo.Site.Requests.ProgressiveJackpot
{
    using System.Collections.Generic;
    using Alpha.MemberInfo.Constants;
    using MediatR;
    using MemberInfo.Models;
    using ViewModels.ProgressiveJackpot;
    using System.Linq;

    public class ProgressiveJacpotQuery : MemberInfoBaseQuery, IRequest<ProgressiveJackpotViewModel>
    {
        public ProgressiveJacpotQuery(IUser viewer) : base(viewer)
        {
            EnableJackpotProductIds = viewer.EnableJackpotProductIds.Where(i => i != ProductId.Pragmatic).ToList();
        }

        public string UserSelectedProductIds { get; set; }

        public IList<int> EnableJackpotProductIds { get; }
    }
}