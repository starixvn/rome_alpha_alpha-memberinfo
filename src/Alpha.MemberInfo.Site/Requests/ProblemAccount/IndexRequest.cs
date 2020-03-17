namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using System;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.ViewModels.ProblemAccount;
    using MediatR;

    public class IndexRequest : ProblemAccountRequestBase, IRequest<ProblemAccountReportViewModel>
    {
        public IndexRequest(IUser viewer) : base(viewer)
        {
        }
    }
}